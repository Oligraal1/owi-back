using System;
using System.Collections.Generic;

namespace owi_back.Models;


public class CommentDTO{

    public int Id { get; set; }

    public string Content { get; set; } = null!;


   public Comment ToComment()
    {
        return new Comment
        {
            Id = Id,
            Content = Content,
            
        };
    }

}