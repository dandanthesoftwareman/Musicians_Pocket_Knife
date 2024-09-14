using System;
using System.Collections.Generic;

namespace Musicians_Pocket_Knife.Models;

public class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; }

    public string? LastName { get; set; }

    public string GoogleId { get; set; }
}
