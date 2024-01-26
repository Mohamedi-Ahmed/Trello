using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrelloMVC.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateCreation { get; set; }

    public int? IdCard { get; set; }

    public int? UserId { get; set; }

    [JsonIgnore]
    public virtual Card? IdCardNavigation { get; set; }

    [JsonIgnore]
    public virtual User? User { get; set; }
}
