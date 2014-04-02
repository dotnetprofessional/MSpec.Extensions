using System;
using FluentAssertions;
using Machine.Specifications;
using MSpec.Extensions.Model;
using MSpec.Extensions.Model.xBehave;
using xpf.IO;

namespace MSpec.Extensions.Spec.Epics.Feature.Include_stories_in_code.Story.Access_existing_MSpec_Xml_report_output
{
    [Scenario(@"Read MSpec xml output into a Bdd model instance", 
        typeof(Match_customer_specification.Include_stories_in_code.Access_existing_MSpec_Xml_report_output))]
    public class Merge_MSpec_model_with_Bdd_Model
    {
        static xBehaveModeler Subject;
        static Exception Exception;

        [Given(@"
            Given an MSpec Xml output exists
            And no xBehave Attributes have been defined in the originating Spec/Test code
            And ProcessModel called
        ")]
        public class When_merging_the_MSpec_model_the_Bdd_model
        {
            Establish read_file = () =>
            {
                Subject = new xBehaveModeler();
                Exception =
                    Catch.Exception(
                        () => Subject.ProcessModel(EmbeddedResources.GetResourceStream("vanilla.xml", typeof(Merge_MSpec_model_with_Bdd_Model))));

                DefaultEpic = Subject.XBehaveModel.Epics[0];
                ActiveStory = DefaultEpic.Features[0].Stories[0];
                //var x = Subject.xBehaveModel.ToXml();
                //var y = x;
            };

            static Model.xBehave.Story ActiveStory;
            static Epic DefaultEpic { get; set; }

            It should_have_a_default_epic = () => DefaultEpic.Narration.Should().Be("");

            It should_have_a_default_feature = () => DefaultEpic.Features[0].Narration.Should().Be("");

            It should_have_a_default_story = () => DefaultEpic.Features[0].Stories[0].Narration.Should().Be("");

            It should_have_1_scenario = () => ActiveStory.Scenarios.Count.Should().Be(1);

        }

        [Given(@"
            Given an MSpec Xml output exists
            And xBehave Attributes have been defined in the originating Spec/Test code
            And ProcessModel called
        ")]
        public class When_merging_the_MSpec_model_the_Bdd_model_
        {
            Establish read_file = () =>
            {
                Subject = new xBehaveModeler();
                Exception =
                    Catch.Exception(
                        () => Subject.ProcessModel(EmbeddedResources.GetResourceStream("xBehave.xml", typeof(Merge_MSpec_model_with_Bdd_Model))));

                DefaultEpic = Subject.XBehaveModel.Epics[0];
                ActiveStory = DefaultEpic.Features[0].Stories[0];
                
                var x = Subject.XBehaveModel.ToXml();
                var y = x;
            };

            static Model.xBehave.Story ActiveStory;
            static Epic DefaultEpic { get; set; }

            It should_have_a_default_epic = () => DefaultEpic.Narration.Should().NotBe("");

            It should_have_a_default_feature = () => DefaultEpic.Features[0].Narration.Should().NotBe("");

            It should_have_a_default_story = () => DefaultEpic.Features[0].Stories[0].Narration.Should().NotBe("xx");

            It should_have_1_scenario = () => ActiveStory.Scenarios.Count.Should().Be(1);

        }
    }
}