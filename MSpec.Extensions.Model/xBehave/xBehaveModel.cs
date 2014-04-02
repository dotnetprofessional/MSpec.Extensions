using System.Collections.Generic;

namespace MSpec.Extensions.Model.xBehave
{
    public class xBehaveModel
    {
        public xBehaveModel()
        {
            this.Epics = new List<Epic>();
        }

        public List<Epic> Epics { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string OutputPath { get; set; }
    }
}