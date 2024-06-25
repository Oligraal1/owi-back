using System;
using System.Collections.Generic;

namespace owi_back.Models;

public class TaskDTO{
    public int Id { get; set; }

    public string Name { get; set; } = null!;



    public Task ToTask()
    {
        return new Task
        {
            Id = Id,
            Name = Name    

        };
    }

}