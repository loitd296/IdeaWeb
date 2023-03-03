namespace IdeaWeb.Models;
public class Idea
{

    public int id { get; set; }

    public string? name { get; set; }

    public int? like_Count { get; set; }

    public int? dislike_Count { get; set; }

    public DateTime? date_Upload { get; set; }

    public Category? categoryId { get; set; }

    public ICollection<Document>? documents { get; set; }

    public ICollection<Rating>? ratings { get; set; }

    public ICollection<Comment>? comments { get; set; }

    public User? userId { get; set; }

}