using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaWeb.Models;
public class User
{
    [Display(Name = "ID")]
    public int id { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string? name { get; set; }

    [Required]
    [Display(Name = "Phone")]
    public string? phone { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "DOB")]
    public DateTime? dob { get; set; }
    [Required]
    [Display(Name = "Email")]
    public string? email { get; set; }
    [Required]
    [Display(Name = "Password")]
    public string? password { get; set; }
    [Required]
    [Display(Name = "Flag")]
    public int flag { get; set; } = 0;
    [Required]
    [Display(Name = "Department ID")]
    public int DepartmentId { get; set; }
    [Display(Name = "Department")]
    public Department? Department { get; set; }
    [Display(Name = "User Role")]
    public ICollection<UserRole>? userRoles { get; set; }

    public ICollection<Rating>? ratings { get; set; }

    public ICollection<Comment>? comments { get; set; }
    public ICollection<View>? View { get; set; }

    public ICollection<Idea>? Ideas { get; set; }

}