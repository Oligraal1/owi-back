using System;
using System.Collections.Generic;

namespace owi_back.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string? Tag { get; set; }

    public int? ListingId { get; set; }

    public DateTime? Deadline { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Listing? Listing { get; set; }
}
