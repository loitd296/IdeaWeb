using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
        public int Deleted_Status { get; set; }
        public ICollection<Idea>? Ideas { get; set; }
    }
}