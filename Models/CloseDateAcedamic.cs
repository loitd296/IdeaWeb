using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class CloseDateAcedamic
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Upload { get; set; }
        public string? Content { get; set; }

        public int Status { get; set; }

        public ICollection<Ideas>? ideas {get;set;}
    }
}