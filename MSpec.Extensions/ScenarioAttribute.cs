using System;
using Machine.Specifications;

namespace MSpec.Extensions
{
    public class ScenarioAttribute : SubjectAttribute
    {
        public Type Story { get; set; }

        public string Name{ get; set; }

        public ScenarioAttribute(string subject)
            : base(subject)
        {
            this.Name = subject;
        }

        public ScenarioAttribute(string subject, Type associatedStory)
            : base(subject)
        {
            this.Story = associatedStory;
            this.Name = subject;
        }
    }
}
