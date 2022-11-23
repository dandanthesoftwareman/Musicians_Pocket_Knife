export interface Song {
    song: SongClass;
}

export interface SongClass {
    id:       string;
    title:    string;
    uri:      string;
    artist:   Artist;
    tempo:    string;
    time_sig: string;
    key_of:   string;
    open_key: null;
    display?:boolean;
}

export interface Artist {
    id:     string;
    name:   string;
    uri:    string;
    img:    string;
    genres: string[];
    from:   string;
    mbid:   string;
}
