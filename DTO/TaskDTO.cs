using System;
using System.Collections.Generic;

using owi_back.Models;

namespace owi_back.DTO;


public class TaskDTO{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Tag { get; set; }
    public List<CommentDTO> Comments { get; set; }
    
}