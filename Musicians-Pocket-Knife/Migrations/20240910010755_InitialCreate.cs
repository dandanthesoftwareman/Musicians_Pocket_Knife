using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Musicians_Pocket_Knife.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GoogleID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3213E83F7264FFE3", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastDateViewed = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Playlist__3213E83F93567F1A", x => x.id);
                    table.ForeignKey(
                        name: "FK__Playlists__UserI__160F4887",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "DBSongs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongIndex = table.Column<int>(type: "int", nullable: true),
                    PlaylistId = table.Column<int>(type: "int", nullable: true),
                    APIid = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Artist = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Tempo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TimeSignature = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OriginalKey = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TransposedKey = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DBSongs__3213E83FB0A1AA73", x => x.id);
                    table.ForeignKey(
                        name: "FK__DBSongs__Playlis__18EBB532",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DBSongs_PlaylistId",
                table: "DBSongs",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UserId",
                table: "Playlists",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DBSongs");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
