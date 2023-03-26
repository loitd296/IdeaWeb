using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}