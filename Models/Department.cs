namespace IdeaWeb.Models;
public class Department
{
    public int id { get; set; }
    public string? name { get; set; }
    public ICollection<User>? users { get; set; }
}