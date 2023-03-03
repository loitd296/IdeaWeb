using System.ComponentModel.DataAnnotations;
namespace IdeaWeb.Models;
public class Role
{

    public int id { get; set; }

    public string? name { get; set; }

    public ICollection<UserRole>? userRoles { get; set; }



}