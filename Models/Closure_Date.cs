using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class Closure_Date
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Upload { get; set; }
        public string? Content { get; set; }

        public int Status { get; set; }
    }
}