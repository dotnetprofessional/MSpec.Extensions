using Machine.Specifications;

namespace MSpec.Extensions.xBehave.Spec.SystemHealth
{
    public class _95th_Percentile
    {
        [Scenario("Calculate status based on targets in green yellow red order",
            typeof (Overall_system_health.Jobs_moving_at_expected_speed._95th_Percentile))]
        [Given(@"
            Given the green range is between zero and 39
            And the yellow range is between 40 and 89
            And the red range is 90 or more")]
        public class Given_green_yellow_red
        {

            Establish given = () => { };

            public class When_the_value_is_0
            {
                Because when = () => { };

                It the_status_should_be_green;
            }

            public class When_the_value_is_20
            {
                Because when = () => { };

                It the_status_should_be_green;
            }

            public class When_the_value_is_40
            {
                Because when = () => { };

                It the_status_should_be_yellow;
            }
        }

        [Scenario("Calculate status based on targets in red yellow green order",
            typeof (Overall_system_health.Jobs_moving_at_expected_speed._95th_Percentile))]
        [Given(@"
            Given the red range is between zero and 39
            And the yellow range is between 40 and 89
            And the green range is 90 or more")]
        public class Given_red_yellow_green
        {

            Establish given = () => { };

            public class When_the_value_is_0
            {
                Because when = () => { };

                It should_have_a_status_of_red;

                It should_have_a_red_target_of_39;

                It should_have_a_yellow_target_of_40;

                It should_have_a_green_target_of_90;

            }

            public class When_the_value_is_20
            {
                Because when = () => { };

                It the_status_should_be_red;
            }

            public class When_the_value_is_40
            {
                Because when = () => { };

                It the_status_should_be_yellow;
            }
        }

    }
}

