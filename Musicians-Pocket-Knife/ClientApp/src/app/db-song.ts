export interface DbSong {
    id:             number;
    songIndex:      number;
    playlistId:     number;
    apiid:          string;
    title:          string;
    artist:         string;
    tempo:          string;
    timeSignature:  string;
    originalKey:    string;
    transposedKey:  string;
}
