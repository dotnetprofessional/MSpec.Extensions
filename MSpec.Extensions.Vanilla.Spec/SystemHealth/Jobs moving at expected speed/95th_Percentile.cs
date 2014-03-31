using Machine.Specifications;

namespace MSpec.Extensions.Vanilla.Spec.SystemHealth
{
    public class _95th_Percentile
    {
        [Subject("Scenario: Calculate status based on targets in green yellow red order")]
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

        [Subject("Scenario: Calculate status based on targets in red yellow green order")]
        public class Given_red_yellow_green
        {

            Establish given = () => { };

            public class When_the_value_is_0
            {
                Because when = () => { };

                It the_status_should_be_red;
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

