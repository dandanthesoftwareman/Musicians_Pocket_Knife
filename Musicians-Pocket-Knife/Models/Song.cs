using System;
using System.Collections.Generic;

namespace Musicians_Pocket_Knife.Models;

public partial class Song
{
    public int Id { get; set; }

    public int? PlaylistId { get; set; }

    public string? Title { get; set; }

    public string? Artist { get; set; }

    public string? Tempo { get; set; }

    public string? TimeSignature { get; set; }

    public string? OriginalKey { get; set; }

    public string? TransposedKey { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public virtual Playlist? Playlist { get; set; }
}
