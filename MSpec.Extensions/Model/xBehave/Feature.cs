using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.xBehave
{
    public class Feature
    {
        public Feature()
        {
            this.Stories = new List<Story>();
            this.Statistics = new Statistics();
        }

        public string Name { get; set; }

        public string Narration { get; set; }

        public List<Story> Stories { get; set; }

        [XmlAttribute]
        public long ExecutionTime { get; set; }

        [XmlAttribute]
        public string TypeName { get; set; }

        public Statistics Statistics { get; set; }

        public string Key { get { return this.TypeName.GetHashCode().ToString(CultureInfo.InvariantCulture); } }

    }
}