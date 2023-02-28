namespace IdeaWeb.Models;
public class Role
{

    public string? id { get; set; }

    public string? name { get; set; }

    public ICollection<UserRole>? userRoles { get; set; }



}