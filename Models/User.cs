namespace IdeaWeb.Models;
public class User{

    public string? id {get;set;}

    public string? name {get;set;}

    public string? phone {get;set;}

    public DateTime? dob {get;set;}

    public string? gmail {get;set;}

    public string? password {get;set;}

    public Department? departmentId {get;set;}

    public UserRole? userRoles {get;set;}

    public ICollection<Rating>? ratings {get;set;}

    public ICollection<Comment>? comments {get;set;}
    
}