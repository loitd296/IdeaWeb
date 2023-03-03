using System.ComponentModel.DataAnnotations;
namespace IdeaWeb.Models;
public class UserRole
{
    public int id { get; set; }
    public int userId { get; set; }

    public User? user { get; set; }
    public int roleId { get; set; }
    public Role? roles {get; set;}

}