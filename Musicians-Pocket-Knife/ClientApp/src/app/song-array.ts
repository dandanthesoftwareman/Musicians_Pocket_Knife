export interface SongArray {
    search: Search[];
}

export interface Search {
    id:     string;
    title:  string;
    uri:    string;
    artist: Artist;
    display:boolean;
}

export interface Artist {
    id:     string;
    name:   string;
    uri:    string;
    img:    null | string;
    genres: string[] | null;
    from:   null | string;
    mbid:   string;
}



