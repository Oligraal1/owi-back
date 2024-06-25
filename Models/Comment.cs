using System;
using System.Collections.Generic;

namespace owi_back.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? User { get; set; }

    public int? TaskId { get; set; }

    public virtual Task? Task { get; set; }
}
