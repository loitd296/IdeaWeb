using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models
{
    public class Document
    {
        public string? Id { get; set; }

        [DataType(DataType.Date)]
        public Ideas? IDIdea { get; set; } //Link
        public string? Content { get; set; } 

        public int Status { get; set; }

        public Ideas? ideasId {get;set;}
    }
}