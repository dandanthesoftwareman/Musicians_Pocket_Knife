export interface Playlist {
    id:        number;
    listTitle: string;
    userId:    number;
    dateCreated: Date;
    lastDateViewed: Date;
    songs:     any[];
}
