namespace MSpec.Extensions.xBehave.Spec.SystemHealth
{
    [EpicStory(
      @"As a Service Engineer  
        I want to be able to see the overall health of the service 
        So that I can gain an understanding of the service health")]
    public partial class Overall_system_health
    {
        [FeatureStory(
          @"As a Service Engineer  
            I want to understand/know whether jobs are moving through the system at the expected speed 
            So that I can have confidence that the services I monitor are meeting my customer's needs.")]
        public class Jobs_moving_at_expected_speed
        {
            [Story(
              @"As a Service Engineer  
                I want to see the 95th Percentile of job execution time to completion (E2E) over a defined time period 
                So that I can have an increased level of confidence that jobs are executing to completion within SLA (this generates a number)")]
            public class _95th_Percentile {}
        }
    }
}
