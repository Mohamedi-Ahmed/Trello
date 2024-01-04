using System;
using System.Collections.Generic;

namespace TrelloMVC.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateOnly DateCreation { get; set; }

    public int? IdCard { get; set; }

    public string UserName { get; set; } = null!;

    public virtual Card? IdCardNavigation { get; set; }
}
