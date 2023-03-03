using System.ComponentModel.DataAnnotations;

namespace IdeaWeb.Models;
 public class RegisterViewModel
     {
         [Required]
         public string Email { get; set; }
         [Required]
         [DataType(DataType.Password)]
         public string Password { get; set; }
         [Required]
         public string Role { get; set; }
     } 