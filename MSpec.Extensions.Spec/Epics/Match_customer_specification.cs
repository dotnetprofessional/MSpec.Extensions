namespace MSpec.Extensions.Spec.Epics
{
    [EpicStory("Ensure code matches customer expectations",
      @"As a Software Developer
        I want to ensure the code matches the customers specification
        So that the correct customer experience is achieved")]
    public class Match_customer_specification
    {
        [FeatureStory("Include user stories in code",
          @"As a Software Developer
            I want to include the customer user stories in my code
            So that I can clearly match the code to the customer requirements")]
        public class Include_stories_in_code
        {
            [Story("Have access existing MSpec XML report output",
          @"As a Software Developer
            I want to have access to existing MSpec Xml report output
            So that I can create custom reports from the data")]
            public class Access_existing_MSpec_Xml_report_output { }
        }

        [FeatureStory("Have a BDD style Html report for Specs",
          @"As a Software Developer
            I want to have a BDD style Html report for my Spec tests
            So that its easier to match the buiness stories to my tests")]
        public class Bdd_style_reporting { }
    }
}
