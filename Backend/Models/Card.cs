using System;
using System.Collections.Generic;

namespace Backend.Models;

public partial class Card
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? CreationDate { get; set; }

    public int IdList { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual List IdListNavigation { get; set; } = null!;
}
