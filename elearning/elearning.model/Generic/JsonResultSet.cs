
namespace elearning.model.Generic
{
    public class JsonResultSet
    {
        public bool IsSuccess { get; set; }

        public object ResultObject { get; set; }

        public string ErrorMessage { get; set; }
    }
}
