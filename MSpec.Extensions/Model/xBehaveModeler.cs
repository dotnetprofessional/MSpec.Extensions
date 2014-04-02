using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Machine.Specifications;
using MSpec.Extensions.Model.MSpec;
using MSpec.Extensions.Model.xBehave;
using xpf.IO;

namespace MSpec.Extensions.Model
{
    /// <summary>
    /// This class takes the MSpec Model and MSpec.Extensions attribues
    /// and creates a BDD friendly view, which can then be used in reporting
    /// </summary>
    public class xBehaveModeler
    {

        Dictionary<string, Story> Stories = new Dictionary<string, Story>();
        List<GivenStatement> Givens = new List<GivenStatement>();

        public MSpec.MSpec MSpecModel;
        public xBehaveModel XBehaveModel { get; set; }

        void LoadMSpecModel(Stream stream)
        {
            this.MSpecModel = stream.FromXmlStreamToInstance<MSpec.MSpec>();
        }


        public void ProcessModel(ReportConfig config)
        {
            this.ProcessModel(config.MSpecXmlFile);

            // Add config details to model
            this.XBehaveModel.Title = config.Title;
            this.XBehaveModel.OutputPath = config.OutputPath;
            this.XBehaveModel.SubTitle = config.SubTitle;
        }

        public void ProcessModel(Stream stream)
        {
            this.LoadMSpecModel(stream);

            this.XBehaveModel = new xBehaveModel();

            foreach (var a in this.MSpecModel.Assemblies)
            {
                this.CreateBddModelFromAssembly(a);
            }

            this.CalculateStatistics();

            //this.CreateDefaultBddModel();

            //this.MergeMSpecModelWithBddModel();
        }

        void CalculateStatistics()
        {
            foreach(var e in this.XBehaveModel.Epics)
                foreach (var f in e.Features)
                {
                    foreach (var s in f.Stories)
                    {
                        foreach (var s1 in s.Scenarios)
                        {
                            foreach (var g in s1.Givens)
                            {
                                foreach (var w in g.Whens)
                                {
                                    g.Statistics.Update(w.Statistics);
                                }
                                s1.Statistics.Update(g.Statistics);
                            }
                            s.Statistics.Update(s1.Statistics);
                        }
                        f.Statistics.Update(s.Statistics);
                    }
                    e.Statistics.Update(f.Statistics);
                }
        }

        public void ProcessModel(string mspecXmlOutput)
        {
            //if(!File.Exists(mspecXmlOutput))
            //    throw new FileNotFoundException("Unable to find file: " + mspecXmlOutput);

            using (var fs = File.OpenRead(mspecXmlOutput))
            {
                this.ProcessModel(fs);
            }
        }

        void CreateBddModelFromAssembly(Assembly mspecAssembly)
        {
            var assemblyInstance = System.Reflection.Assembly.LoadFrom(mspecAssembly.Location);

            this.DefineEpics(assemblyInstance);
            this.LocateAndAddScenariosToStories(assemblyInstance);
            this.LocateWhensAndAssociateWithGivens(mspecAssembly);
        }

        private void DefineEpics(System.Reflection.Assembly assemblyInstance)
        {
            var epicStories = GetCustomTypes<EpicStoryAttribute>(assemblyInstance);
            if (epicStories.Count == 0)
                this.CreateDefaultBddModel();
            else
            {

                foreach (var e in epicStories)
                {
                    var epic = new Epic
                    {
                        Narration = e.Attribute.Narration,
                        TypeName = e.Type.FullName,
                        Name = this.FormatTypeName(e.Type),
                    };
                    // Add the Epic
                    this.XBehaveModel.Epics.Add(epic);

                    // Now locate any nested classes that have been defined as features
                    foreach (var nestedFeature in e.Type.GetNestedTypes())
                    {
                        if (nestedFeature.IsDefined(typeof (FeatureStoryAttribute), false))
                        {
                            var featureAttribute =
                                nestedFeature.GetCustomAttributes(typeof (FeatureStoryAttribute), false).FirstOrDefault() as FeatureStoryAttribute;

                            var feature = new Feature { Name = this.FormatTypeName(nestedFeature), Narration = featureAttribute.Narration, TypeName = nestedFeature.FullName };
                            epic.Features.Add(feature);

                            // Now look for stories
                            foreach (var nestedStory in nestedFeature.GetNestedTypes())
                            {
                                if (nestedStory.IsDefined(typeof (StoryAttribute), false))
                                {
                                    var storyAttribute = nestedStory.GetCustomAttributes(typeof (StoryAttribute), false).FirstOrDefault() as StoryAttribute;
                                    var story = new Story {Name = this.FormatTypeName(nestedStory), Narration = storyAttribute.Narration, TypeName = nestedStory.FullName};
                                    feature.Stories.Add(story);
                                    this.Stories.Add(story.TypeName, story);
                                }
                            }
                        }
                    }
                }
            }
        }

        //void DefineStories(System.Reflection.Assembly assemblyInstance)
        //{
        //    var stories = GetCustomTypes<StoryAttribute>(assemblyInstance);
        //    if (stories.Count != 0)
        //    {
        //        foreach (var s in stories)
        //        {
        //            // Lookup the feature for this story if there is one
        //            Feature feature = null;
        //            foreach(var e in this.xBehaveModel.Epics)
        //                foreach(var f in e.Features)
        //                    if (f.TypeName == s.Attribute..FullName)
        //                    {
        //                        feature = f;
        //                        break;
        //                    }

        //            if (feature == null)
        //            {
        //                feature = new Feature {Narration = ""};

        //                // Now find a default epic to put it in or create one!
        //                var defaultEpic = this.xBehaveModel.Epics.SingleOrDefault(e => string.IsNullOrEmpty(e.Narration));
        //                if (defaultEpic == null)
        //                    defaultEpic = new Epic {Narration = ""};
        //                defaultEpic.Features.Add(feature);
        //            }

        //            feature.Stories.Add(new Story {Narration = s.Attribute.Narration});
        //        }
        //    }

        //     Add a default feature and/or story to any epics that have no features defined
        //    foreach (var e in this.xBehaveModel.Epics)
        //        if (e.Features.Count == 0)
        //        {
        //            var f = new Feature {Narration = ""};
        //            e.Features.Add(f);
        //            f.Stories.Add(new Story{Narration = ""});
        //        }
        //}
        List<TypeWithAttribute<T>> GetCustomTypes<T>(System.Reflection.Assembly assemblyInstance)
            where T : class
        {
            var customTypes =
                from t in assemblyInstance.GetTypes()
                let attributes = t.GetCustomAttributes(typeof (T), true)
                where attributes != null && attributes.Length > 0
                select new TypeWithAttribute<T> {Type = t, Attribute = attributes.SingleOrDefault() as T};
            return customTypes.ToList();
        }

        List<TypeWithAttribute<T>> GetNestedTypes<T>(Type classType)
            where T : class
        {
             var nestedTypes = classType.GetNestedTypes();
            var foundTypes = new List<TypeWithAttribute<T>>();
 
            foreach (var type in nestedTypes)
            {
                if (type.IsDefined(typeof (T), false))
                {
                    var attribute = type.GetCustomAttributes(typeof (T), false).FirstOrDefault() as T;
                    foundTypes.Add(new TypeWithAttribute<T>{Attribute = attribute, Type = type});
                }
            }
            return foundTypes;
        }

        void LocateAndAddScenariosToStories(System.Reflection.Assembly assemblyInstance)
        {
            var scenarios = GetCustomTypes<ScenarioAttribute>(assemblyInstance);
            if(scenarios.Count != 0)
            {
                foreach(var s in scenarios)
                {
                    // Find associated Story
                    var story = this.Stories[s.Attribute.Story.FullName];
                    if (story == null)
                        throw new ArgumentException("Unable to locate story " + s.Attribute.Story.FullName);

                    var scenario = new Scenario { Name = s.Attribute.Name, TypeName = s.Type.FullName };
                    story.Scenarios.Add(scenario);

                    // Find all Givens nested with this story
                    //var givens = this.GetNestedTypes<GivenAttribute>(s.Type);
                    //foreach (var g in givens)
                    //{
                    //    var given = new GivenStatement {Narration = g.Attribute.Narration, TypeName = g.Type.FullName};
                    //    scenario.Givens.Add(given);
                    //    this.Givens.Add(given);
                    //}

                    var givenAttribute = s.Type.GetCustomAttributes(typeof (GivenAttribute),false).SingleOrDefault() as GivenAttribute;
                    if(givenAttribute != null)
                    {
                        var given = new GivenStatement {Narration = givenAttribute.Narration, TypeName = s.Type.FullName};
                        scenario.Givens.Add(given);
                        this.Givens.Add(given);
                    }
                }
            }
        }

        void LocateWhensAndAssociateWithGivens(Assembly mspecAssembly)
        {
            foreach(var concern in mspecAssembly.Concerns)
                foreach (var c in concern.Contexts)
                {
                    try
                    {
                        // Locate the Given to which the conext is associated
                        var given = this.MatchContextToGiven(c);
                        var when = new WhenStatement { Narration = c.Name };
                        // Now associate all Thens (its) for the when
                        foreach (var s in c.Specifications)
                            when.AddThen(new ThenStatement { Name = s.Name, CapturedOutput = s.CapturedOutput, Status = this.MapStatus(s.Status), ExecutionTime = s.ExecutionTime });

                        // Associate When to given
                        given.Whens.Add(when);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
        }

        ThenStatus MapStatus(string status)
        {
            switch (status)
            {
                case "passed":
                    return ThenStatus.Pass;
                case "not-implemented":
                    return ThenStatus.NotImplemented;
                case "failed":
                    return ThenStatus.Failed;
                case "ignored":
                    return ThenStatus.Ignored;
                default:
                    throw new ArgumentException("Unknown status type: " + status);
            }
        }

        GivenStatement MatchContextToGiven(Context c)
        {
            var contextType = c.Type;
            foreach(var g in this.Givens)
                if (contextType.StartsWith(g.TypeName))
                    return g;

            throw new ArgumentException("Unable to locate a given for context type: " + contextType);
        }

        
        void MergeMSpecModelWithBddModel(System.Reflection.Assembly assemblyInstance)
        {
           var scenarios = GetCustomTypes<ScenarioAttribute>(assemblyInstance);

            foreach (var c in this.MSpecModel.Assemblies[0].Concerns)
            {
                var s = new Scenario {Name = c.Name};
                var given = new GivenStatement {Narration = ""};
                foreach (var context in c.Contexts)
                {
                    var when = new WhenStatement {Narration = context.Name};
                    given.Whens.Add(when);

                    foreach (var spec in context.Specifications)
                        when.Thens.Add(new ThenStatement
                        {
                            Name = spec.Name,
                            ExecutionTime = spec.ExecutionTime,
                            Status = this.MapStatus(spec.Status),
                            CapturedOutput = spec.CapturedOutput
                        });
                }
                s.Givens.Add(given);
            }
        }


        void CreateDefaultBddModel()
        {
            var epic = new Epic {Narration = ""};
            var feature = new Feature {Narration = ""};
            var story = new Story {Narration = ""};

            feature.Stories.Add(story);

            epic.Features.Add(feature);

            this.XBehaveModel.Epics.Add(epic);
        }

        string FormatTypeName(Type t)
        {
            var name = t.Name;
            // Replace all double underscores with quotes
            name = name.Replace("__", "\"");

            // Replace underscore s with 's
            name = name.Replace("_s_", "'s ");

            // Replace underscores with spaces
            name = name.Replace("_", " ");

            return name;
        }
    }

    public class ReportConfig
    {
        public string OutputPath { get; set; }
        public string Title { get; set; }
        public string MSpecXmlFile { get; set; }

        public string SubTitle { get; set; }

    }

    public class TypeWithAttribute<T>
    {
        public Type Type { get; set; }

        public T Attribute { get; set; }
    }

    
}

