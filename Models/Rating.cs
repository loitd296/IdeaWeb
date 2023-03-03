using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdeaWeb.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public int Dislike { get; set; }
        public int like { get; set; }

        public int IdeaId { get; set; }

        public Idea? Idea { get; set; }

        public int userId { get; set; }

        public User? user { get; set; }


    }
}