using System;
using System.Collections.Generic;

namespace TrelloMVC.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
