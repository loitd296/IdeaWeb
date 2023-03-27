using System.ComponentModel.DataAnnotations;
namespace IdeaWeb.Models;
public class View
{

    public int id { get; set; }

    public int userId { get; set; }

    public User user { get; set; }

    public int ideaId { get; set; }

    public Idea idea { get; set; }



}