using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrelloMVC.Models;

public partial class List
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? IdProject { get; set; }

    [JsonIgnore]
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
    [JsonIgnore]
    public virtual Project? IdProjectNavigation { get; set; }
}
