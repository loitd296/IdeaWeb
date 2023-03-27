using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace IdeaWeb.Models
{
    public class Idea
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Content {get; set; }
        public int? Like_Count { get; set; }
        public int? Dislike_Count { get; set; }
        public string? File { get; set; }
        public string? Image { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Date_Upload { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [Required]
        public int CloseDateAcedamicId { get; set; }
        public CloseDateAcedamic? CloseDateAcedamic { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<View>? View { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
       
    }
}