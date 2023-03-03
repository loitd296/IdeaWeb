using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaWeb.Models
{
    public class CloseDateAcedamic
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CloseDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CloseDatePostIdea { get; set; }
        public ICollection<Idea>? Ideas { get; set; }
    }
}