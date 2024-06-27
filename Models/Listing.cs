using System;
using System.Collections.Generic;

namespace owi_back.Models;

public partial class Listing
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
