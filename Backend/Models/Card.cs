using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrelloMVC.Models;

public partial class Card
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime DateCreation { get; set; }

    public int? IdList { get; set; }

    public int? CreatorId { get; set; }

    public string Background { get; set; } = "#FFFFFF";
    
    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [JsonIgnore]
    public virtual User? Creator { get; set; }
    
    [JsonIgnore]
    public virtual List? IdListNavigation { get; set; }
}
