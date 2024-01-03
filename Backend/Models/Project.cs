using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? CreationDate { get; set; }

    public virtual ICollection<List> Lists { get; set; } = new List<List>();
}
