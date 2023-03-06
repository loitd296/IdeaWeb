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
        public string? Name { get; set; }
        public int? Like_Count { get; set; }
        public int? Dislike_Count { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date_Upload { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<Document>? Documents { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Comment>? Comments { get; set; }

        public int UserId { get; set; }
       
        public User? User { get; set; }
       
    }
}