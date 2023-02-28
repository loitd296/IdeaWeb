using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class Rating
    {
        public string? Id { get; set; }
        public string? IDidea { get; set; } //Link

        public int Dislike { get; set; }
        public int like { get; set; }

        public Idea? ideaId { get; set; }

        public User? userId { get; set; }

    }
}