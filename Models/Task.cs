using System;
using System.Collections.Generic;
using owi_back.DTO;

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

    public Task()
		{
			CreatedAt = DateTime.Now;
		}

    /*public TaskDTO TaskToDTO(Task task)
    {
        return new TaskDTO
        {
            Id = task.Id,
            Name = task.Name,
            Tag = task.Tag
        };
    }*/
}
