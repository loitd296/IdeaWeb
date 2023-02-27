using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class Category
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public int Status { get; set; }
        public int Deleted_Status { get; set; }

        public ICollection<Ideas>? ideas {get;set;}
        
    }
}