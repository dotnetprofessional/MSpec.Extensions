using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.xBehave
{
    public class Story
    {
        public Story()
        {
            this.Scenarios = new List<Scenario>();
        }


        public string Name { get; set; }

        public string Narration { get; set; }

        public List<Scenario> Scenarios { get; set; }

        [XmlAttribute]
        public long ExecutionTime { get; set; }

        [XmlAttribute]
        public string TypeName { get; set; }
    }
}