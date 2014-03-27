using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.xBehave
{
    public class Epic
    {
        public Epic()
        {
            Features = new List<Feature>();
        }

        public string Name { get; set; }
        public string Narration { get; set; }

        public List<Feature> Features { get; set; }
        
        [XmlAttribute]
        public long ExecutionTime { get; set; }

        [XmlAttribute]
        public string TypeName { get; set; }
    }
}
