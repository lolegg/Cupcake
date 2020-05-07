
namespace Cupcake.Web.Models
{
    public class LogModel : BaseModel
    {
        public string Ip { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}