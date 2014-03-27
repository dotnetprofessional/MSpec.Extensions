using System;
using Machine.Specifications.Annotations;

namespace MSpec.Extensions
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
    public class GivenAttribute : Attribute
    {
        public string Narration { get; set; }

        public GivenAttribute(string narration)
        {
            Narration = narration;
        }   
    }
}