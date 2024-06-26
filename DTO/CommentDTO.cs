using System;
using System.Collections.Generic;
using owi_back.Models;

namespace owi_back.DTO;


public class CommentDTO{

    public int Id { get; set; }

    public string Content { get; set; } = null!;
     public int? TaskId { get; set; }       //?


   /*public Comment ToComment()
    {
        return new Comment
        {
            Id = Id,
            Content = Content,
            
        };
    }*/

}