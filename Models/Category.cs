using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int Deleted_Status { get; set; }
        public ICollection<Idea>? Ideas { get; set; }
    }
}