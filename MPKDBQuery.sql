--CREATE TABLE Users(
--id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
--FirstName NVARCHAR(100) NOT NULL,
--LastName NVARCHAR(100),
--GoogleID NVARCHAR(255) NOT NULL
--);

--SELECT * FROM Users;
--DELETE FROM Users
--WHERE id = 1;



--CREATE TABLE Playlists(
--id INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
--ListTitle NVARCHAR(255),
--UserId INT FOREIGN KEY REFERENCES Users(id)
--);

--SELECT * FROM Playlists;



--CREATE TABLE Songs(
--id INT NOT NULL IDENTITY (1,1) PRIMARY KEY,
--PlaylistId INT FOREIGN KEY REFERENCES Playlists(id),
--Title NVARCHAR(255),
--Artist NVARCHAR(255),
--Tempo NVARCHAR(15),
--TimeSignature NVARCHAR(15),
--OriginalKey NVARCHAR(15),
--TransposedKey NVARCHAR(15),
--);
