using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdeaWeb.Models;
public class User
{

    public int id { get; set; }

    public string? name { get; set; }

    public string? phone { get; set; }

    [DataType(DataType.Date)]
    public DateTime? dob { get; set; }

    public string? email { get; set; }

    public string? password { get; set; }

    public int flag {get; set;} = 0;

    public int DepartmentId { get; set; }

    public Department? Department { get; set; }

    public ICollection<UserRole>? userRoles { get; set; }

    public ICollection<Rating>? ratings { get; set; }

    public ICollection<Comment>? comments { get; set; }

    public ICollection<Idea>? Ideas { get; set; }

}