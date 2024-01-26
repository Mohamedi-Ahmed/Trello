using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrelloMVC.Models;

public partial class UserProject
{
    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public string UserRole { get; set; } = null!;

    [JsonIgnore]
    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
