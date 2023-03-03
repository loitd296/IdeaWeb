using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class CloseDateAcedamic
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CloseDate { get; set; }
        [DataType(DataType.Date)]
        public DataType? CloseDatePostIdea { get; set; }
        public ICollection<Idea>? ideas { get; set; }
    }
}