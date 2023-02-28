using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class Comment
    {
        public string? Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Upload { get; set; }
        public string? Content { get; set; }

        public int Status { get; set; }

        public Idea? ideaId { get; set; }

        public User? userId { get; set; }
    }
}