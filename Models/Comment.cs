using System;
using System.Collections.Generic;
using owi_back.DTO;

namespace owi_back.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? User { get; set; }

    public int TaskId { get; set; }

    public virtual Task? Task { get; set; }

    public Comment()
		{
			CreatedAt = DateTime.Now;
		}

    /*public CommentDTO ToCommentDTO(Comment comment)
    {
        return new CommentDTO
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskId = comment.TaskId
        };
    }*/
}
