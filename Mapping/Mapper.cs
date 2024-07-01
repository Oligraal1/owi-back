using System;
using System.Collections.Generic;
using owi_back.DTO;
using owi_back.Models;

namespace owi_back.Mapping;

public class Mapper
{
    public TaskDTO TaskToDTO(owi_back.Models.Task task)
    {
        return new TaskDTO
        {
            Id = task.Id,
            Name = task.Name,
            Tag = task.Tag,
            Comments = task
                .Comments?.Select(c => new CommentDTO
                {
                    Id = c.Id,
                    Content = c.Content,
                    TaskId = c.TaskId
                })
                .ToList()
        };
    }

    public CommentDTO CommentToDTO(Comment comment)
    {
        return new CommentDTO
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskId = comment.TaskId
        };
    }
}
