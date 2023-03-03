namespace IdeaWeb.Models;
public class UserRole
{
    public int id { get; set; }
    public User? userId { get; set; }
    public Role? roleId { get; set; }

}