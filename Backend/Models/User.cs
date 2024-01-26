using System;
using System.Collections.Generic;

namespace TrelloMVC.Models;

public partial class User
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}
