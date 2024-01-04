﻿using System;
using System.Collections.Generic;

namespace TrelloMVC.Models;

public partial class Card
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly DateCreation { get; set; }

    public int? IdList { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual List? IdListNavigation { get; set; }
}
