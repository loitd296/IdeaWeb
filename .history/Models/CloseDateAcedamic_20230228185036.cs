using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class CloseDateAcedamic
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CloseDate { get; set; }
        [DataType(DataType.Date)]

        public DataType? CloseDatePostIdea { get; set; }

        public ICollection<Ideas>? ideas {get;set;}
    }
}