using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace IdeaWeb.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Upload { get; set; }
        public string? Content { get; set; }

        public int Status { get; set; }

        public int ideaId { get; set; }

        public Idea? idea { get; set; }

        public int userId { get; set; }

       [ForeignKey("userId")]
        public User? user { get; set; }
       
    }
}