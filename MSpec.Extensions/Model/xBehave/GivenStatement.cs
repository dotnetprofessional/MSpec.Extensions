using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.xBehave
{
    public class GivenStatement
    {
        public GivenStatement()
        {
            this.Whens = new List<WhenStatement>();
            this.Statistics = new Statistics();
        }

        public string Narration { get; set; }

        public List<WhenStatement> Whens { get; set; }

        [XmlAttribute]
        public string TypeName { get; set; }

        public Statistics Statistics { get; set; }

        public string Key { get { return this.TypeName.GetHashCode().ToString(CultureInfo.InvariantCulture); } }

    }
}