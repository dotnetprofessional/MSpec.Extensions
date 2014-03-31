using MSpec.Extensions.Model.xBehave;

namespace MSpec.Report.Template
{
    public class CompositeModel<T>
    {
        public xBehaveModel Full { get; set; }

        public T Selected { get; set; }
    }

}
