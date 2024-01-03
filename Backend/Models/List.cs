using System;
using System.Collections.Generic;

namespace TrelloMVC.Models;

public partial class List
{
    public int IdList { get; set; }

    public string? NameList { get; set; }

    public int? IdProject { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual Project? IdProjectNavigation { get; set; }
}
