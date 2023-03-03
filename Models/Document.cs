using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaWeb.Models
{
    public class Document
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date_Upload { get; set; } //Link
        public string? DataLink { get; set; }

        public int IdeaId { get; set; }

        public Idea? Idea { get; set; }
    }
}