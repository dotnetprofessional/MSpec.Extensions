using System;
using FluentAssertions;
using Machine.Specifications;
using MSpec.Extensions.Model;
using xpf.IO;

namespace MSpec.Extensions.Spec.Epics.Feature.Include_stories_in_code.Story.Access_existing_MSpec_Xml_report_output
{
    [Scenario(@"Read MSpec xml output into a Bdd model instance",
        typeof(Match_customer_specification.Include_stories_in_code.Access_existing_MSpec_Xml_report_output))]
    [Given(@"
            Given an MSpec Xml output exists
            And no Bdd Attributes have been defined in the originating Spec/Test code
        ")]
    public class Read_into_MSpec_model_instance
    {

        static xBehaveModeler Subject;
        static Exception Exception;

        Given given = ()
            =>
            {
                Subject = new xBehaveModeler();
            };
        
        Because read_file = () =>
        {
            Exception =
                Catch.Exception(
                    () => Subject.ProcessModel(EmbeddedResources.GetResourceStream("vanilla.xml", typeof(Read_into_MSpec_model_instance))));
        };


        public class When_reading_the_xml_output_the_MSpec_model
        {
            It should_not_fail_to_load = () => Exception.Should().BeNull();

            It should_have_1_assemblies = () => Subject.MSpecModel.Assemblies.Count.Should().Be(1);

            It should_have_2_conerns = () => Subject.MSpecModel.Assemblies[0].Concerns.Count.Should().Be(2);

            It should_have_the_concern_name =
                () => Subject.MSpecModel.Assemblies[0].Concerns[0].Name.Should().Be("Scenario: Calculate status based on targets in green yellow red order");

            It should_have_3_contexts = () => Subject.MSpecModel.Assemblies[0].Concerns[0].Contexts.Count.Should().Be(3);

            It should_have_the_context_name = () => Subject.MSpecModel.Assemblies[0].Concerns[0].Contexts[0].Name.Should().Be("When the value is 0");

            It should_have_1_specification = () => Subject.MSpecModel.Assemblies[0].Concerns[0].Contexts[0].Specifications.Count.Should().Be(1);

            It should_have_the_specification_name =
                () => Subject.MSpecModel.Assemblies[0].Concerns[0].Contexts[0].Specifications[0].Name.Should().Be("the status should be green");
        }

    }
}

