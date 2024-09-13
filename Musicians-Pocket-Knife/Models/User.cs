using System;
using System.Collections.Generic;

namespace Musicians_Pocket_Knife.Models;

public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string GoogleId { get; set; } = null!;
}
