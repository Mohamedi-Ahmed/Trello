using System;
using System.Collections.Generic;

namespace TrelloMVC.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public DateOnly? CreationDate { get; set; }

    public int IdCard { get; set; }

    public virtual Card IdCardNavigation { get; set; } = null!;
}
