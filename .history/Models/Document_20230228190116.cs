using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class Document
    {
        public string? Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Upload { get; set; } //Link
        public string? DataLink { get; set; }

        public Idea? ideaId { get; set; }
    }
}