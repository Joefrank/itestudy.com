
using CMCommon.Enums;

namespace CMCommon.Model
{
    public class ReturnStructure
    {
        public object ReturnObject { get; set; }

        public ReturnStatus Status { get; set; }

        public string ErrorMessage { get; set; }
    }
}
