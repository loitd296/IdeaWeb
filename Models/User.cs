using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaWeb.Models;
public class User
{

    public int id { get; set; }
    [Required]
    public string? name { get; set; }
    [Required]
    public string? phone { get; set; }

    [DataType(DataType.Date)]
    public DateTime? dob { get; set; }
    [Required]
    public string? email { get; set; }
    [Required]
    public string? password { get; set; }
    [Required]
    public int flag { get; set; } = 0;
    [Required]
    public int DepartmentId { get; set; }

    public Department? Department { get; set; }

    public ICollection<UserRole>? userRoles { get; set; }

    public ICollection<Rating>? ratings { get; set; }

    public ICollection<Comment>? comments { get; set; }
    public ICollection<View>? View { get; set; }

    public ICollection<Idea>? Ideas { get; set; }

}