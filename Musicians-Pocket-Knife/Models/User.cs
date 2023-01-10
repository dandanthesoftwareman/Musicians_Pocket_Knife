using System;
using System.Collections.Generic;

namespace Musicians_Pocket_Knife.Models;

public partial class User
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string GoogleId { get; set; } = null!;
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Playlist> Playlists { get; } = new List<Playlist>();
}
