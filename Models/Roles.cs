namespace IdeaWeb.Models;
public class Roles{

    public string? id {get;set;}

    public string? name {get;set;}

    public ICollection<UserRole>? userRoles {get;set;}


    
}