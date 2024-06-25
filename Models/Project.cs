using System;
using System.Collections.Generic;

namespace owi_back.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? Deadline { get; set; }

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
