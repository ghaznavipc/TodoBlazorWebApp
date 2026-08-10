using System.ComponentModel.DataAnnotations;

namespace TodoWebApp.Models;

public class TodoItem
{
    public int Id { get; set; }
    [MaxLength(255)]
    public string? Title { get; set; } = string.Empty;
    public bool IsDone { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime? CompleteDate { get; set; }
}
