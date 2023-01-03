using System;
using System.Collections.Generic;

namespace Musicians_Pocket_Knife.Models;

public partial class Playlist
{
    public int Id { get; set; }

    public string? ListTitle { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Song> Songs { get; } = new List<Song>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual User? User { get; set; }
}
