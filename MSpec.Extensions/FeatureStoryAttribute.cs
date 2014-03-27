using System;
using Machine.Specifications.Annotations;

namespace MSpec.Extensions
{
    public class FeatureStoryAttribute : StoryAttribute
    {
        public FeatureStoryAttribute(string narration, string storyType) : base(narration, storyType)
        {
        }

        public FeatureStoryAttribute(string narration) : base(narration)
        {
        }
    }
}