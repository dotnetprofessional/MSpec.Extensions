using System;
using System.IO;
using MSpec.Extensions.Model;
using MSpec.Extensions.Model.xBehave;
using MSpec.Report.Template;
using xpf.IO;

namespace MSpec.Report
{
    class Program
    {
        static void Main(string[] args)
        {
            // Process command line arguments
            if (args.Length == 0)
            {
                OutputHelp();
                return;
            }

            // Process the command line
            var config = ProcessCommandLine(args);
            config.OutputPath += @"bs\";
            if (config == null)
                return;

            try
            {

                var xBehaveModelerler = new xBehaveModeler();
                xBehaveModelerler.ProcessModel(config);

                xBehaveModel model = xBehaveModelerler.XBehaveModel;
                if (!Directory.Exists(config.OutputPath))
                    Directory.CreateDirectory(config.OutputPath);

                //BasicHtmlTemplate(model);
                BootstrapTemplate(model);
            }
            catch (FileNotFoundException fileNotFound)
            {
                Console.WriteLine(string.Format("The file {0} was not found", fileNotFound.FileName));
                Console.ReadLine();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        static void BootstrapTemplate(xBehaveModel model)
        {
            ExecuteTemplate("BootstrapTemplate.Summary.cshtml", model.OutputPath, "summary.html", model);
            ExecuteTemplate("BootstrapTemplate.Default.cshtml", model.OutputPath, "default.html", model);

            foreach (var e in model.Epics)
            {
                var compositeModel = new CompositeModel<Epic> {Full = model, Selected = e};
                ExecuteTemplate("BootstrapTemplate.Epic.cshtml", model.OutputPath, string.Format("epic-{0}.html", e.Key), compositeModel);
                foreach (var f in e.Features)
                {
                    var featureModel = new CompositeModel<Feature> { Full = model, Selected = f };
                    // Create a page for each feature
                    ExecuteTemplate("BootstrapTemplate.Feature.cshtml", model.OutputPath, string.Format("feature-{0}.html", f.Key), featureModel);
                    foreach (var s in f.Stories)
                    {
                        var storyModel = new CompositeModel<Story> { Full = model, Selected = s };
                        // Create a page for each feature
                        ExecuteTemplate("BootstrapTemplate.Story.cshtml", model.OutputPath, string.Format("story-{0}.html", s.Key), storyModel);
                    }
                }
            }
        }
        static void BasicHtmlTemplate(xBehaveModel model)
        {
            CopyTemplateFiles(model);

            ExecuteTemplate("Index.cshtml", model.OutputPath, "index.html", model);
            ExecuteTemplate("examples.cshtml", model.OutputPath, "examples.html", model);

            // create sub pages
            foreach (var e in model.Epics)
                foreach (var f in e.Features)
                {
                    var featureModel = new CompositeModel<Feature> {Full = model, Selected = f};
                    // Create a page for each feature
                    ExecuteTemplate("Feature.cshtml", model.OutputPath, string.Format("{0}.html", f.TypeName), featureModel);
                    foreach (var s in f.Stories)
                    {
                        var storyModel = new CompositeModel<Story> {Full = model, Selected = s};
                        // Create a page for each feature
                        ExecuteTemplate("Story.cshtml", model.OutputPath, string.Format("{0}.html", s.TypeName), storyModel);
                    }
                }
        }

        static ReportConfig ProcessCommandLine(string[] args)
        {
            var config = new ReportConfig();

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-title":
                        config.Title = args[i + 1];
                        i++;
                        break;
                    case "-mspecoutput":
                        config.MSpecXmlFile = args[i + 1];
                        i++;
                        break;
                    case "-output":
                        config.OutputPath = args[i + 1];
                        i++;
                        break;
                    case "-subtitle":
                        config.SubTitle = args[i + 1];
                        i++;
                        break;
                    default:
                        Console.WriteLine("Unknown switch " + args[i]);
                        Console.ReadLine();
                        return null;
                        break;
                }
            }

            return config;
        }

        static void OutputHelp()
        {
            Console.WriteLine("HELP");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Use the following command line parameters to output a report");
            Console.WriteLine("-help\t\tshows this help menu");
            Console.WriteLine("-title\t\tdefines the report title");
            Console.WriteLine("-subtitle\t\tdefines the report sub title");
            Console.WriteLine("-mspecOutput\tthe filename of the mspec runner xml output file");
            Console.WriteLine("-output\t\tthe path to output the report");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
        }


        static void ExecuteTemplate(string templateName, string outputPath, string outputName, object model)
        {
            var template = EmbeddedResources.GetResourceString(typeof(Program).Assembly, templateName);
            var output = RazorEngine.Razor.Parse(template, model);
            File.WriteAllText(string.Format(@"{0}\{1}", outputPath, outputName), output);
            
        }

        private static void CopyTemplateFiles(xBehaveModel model)
        {
            var cssFile = EmbeddedResources.GetResourceString(typeof(Program).Assembly, "styles.css");
            File.WriteAllText(string.Format(@"{0}\styles.css", model.OutputPath), cssFile);
        }

    }

}
