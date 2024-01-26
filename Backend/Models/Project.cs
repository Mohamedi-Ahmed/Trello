using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrelloMVC.Models;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime DateCreation { get; set; }

    public virtual ICollection<List> Lists { get; set; } = new List<List>();

    [JsonIgnore]
    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}
