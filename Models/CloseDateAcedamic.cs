using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaWeb.Models
{
    public class CloseDateAcedamic
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? CloseDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? CloseDatePostIdea { get; set; }
        public ICollection<Idea>? Ideas { get; set; }
    }
}