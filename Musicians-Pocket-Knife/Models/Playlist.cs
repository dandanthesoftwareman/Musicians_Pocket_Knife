using System;
using System.Collections.Generic;

namespace Musicians_Pocket_Knife.Models;

public partial class Playlist
{
    public int Id { get; set; }

    public string? ListTitle { get; set; }

    public int? UserId { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? LastDateViewed { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual ICollection<Dbsong> Dbsongs { get; } = new List<Dbsong>();
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual User? User { get; set; }
}
