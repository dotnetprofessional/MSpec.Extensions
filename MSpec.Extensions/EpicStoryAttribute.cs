namespace MSpec.Extensions
{
    public class EpicStoryAttribute : StoryAttribute
    {
        public EpicStoryAttribute(string narration, string storyType) : base(narration, storyType)
        {
        }

        public EpicStoryAttribute(string narration) : base(narration)
        {
        }
    }
}