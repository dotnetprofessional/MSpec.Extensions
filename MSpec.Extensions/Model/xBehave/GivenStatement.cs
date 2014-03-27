using System.Collections.Generic;
using System.Xml.Serialization;

namespace MSpec.Extensions.Model.xBehave
{
    public class GivenStatement
    {
        public GivenStatement()
        {
            this.Whens = new List<WhenStatement>();
        }

        public string Narration { get; set; }

        public List<WhenStatement> Whens { get; set; }

        [XmlAttribute]
        public string TypeName { get; set; }
    }
}