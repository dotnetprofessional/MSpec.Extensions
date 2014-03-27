using System;

namespace MSpec.Extensions
{
    public class StoryAttribute : Attribute
    {
        public string Narration { get; set; }

        public string StoryType { get; set; }

        public string Reference { get; set; }

        public StoryAttribute(string narration, string storyType)
        {
            Narration = narration;
            StoryType = storyType;
        }

        public StoryAttribute(string narration)
        {
            Narration = narration;
            StoryType = Extensions.StoryType.Business;
        }

        public StoryAttribute(string narration, string storyType, string reference)
        {
            Narration = narration;
            StoryType = storyType;
            Reference = reference;
        }
    }
}