using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.xBehave
{
    public class Scenario
    {
        public Scenario()
        {
            this.Givens = new List<GivenStatement>();
        }

        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public long ExecutionTime { get; set; }

        public string CapturedOutput { get; set; }

        public List<GivenStatement> Givens { get; set; }

        [XmlAttribute]
        public string TypeName { get; set; }
    }
}