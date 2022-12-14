using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csharp_boolflix.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayedHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayedHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsChild = table.Column<bool>(type: "bit", nullable: false),
                    PlayedHistoryId = table.Column<int>(type: "int", nullable: false),
                    PlaylistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_PlayedHistories_PlayedHistoryId",
                        column: x => x.PlayedHistoryId,
                        principalTable: "PlayedHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profiles_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayedHistoryId = table.Column<int>(type: "int", nullable: true),
                    ProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoContents_PlayedHistories_PlayedHistoryId",
                        column: x => x.PlayedHistoryId,
                        principalTable: "PlayedHistories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoContents_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GenreVideoContent",
                columns: table => new
                {
                    GenresListId = table.Column<int>(type: "int", nullable: false),
                    VideoContentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreVideoContent", x => new { x.GenresListId, x.VideoContentsId });
                    table.ForeignKey(
                        name: "FK_GenreVideoContent_Genres_GenresListId",
                        column: x => x.GenresListId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreVideoContent_VideoContents_VideoContentsId",
                        column: x => x.VideoContentsId,
                        principalTable: "VideoContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistVideoContent",
                columns: table => new
                {
                    PlaylistsListId = table.Column<int>(type: "int", nullable: false),
                    VideoContentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistVideoContent", x => new { x.PlaylistsListId, x.VideoContentsId });
                    table.ForeignKey(
                        name: "FK_PlaylistVideoContent_Playlists_PlaylistsListId",
                        column: x => x.PlaylistsListId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistVideoContent_VideoContents_VideoContentsId",
                        column: x => x.VideoContentsId,
                        principalTable: "VideoContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreVideoContent_VideoContentsId",
                table: "GenreVideoContent",
                column: "VideoContentsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistVideoContent_VideoContentsId",
                table: "PlaylistVideoContent",
                column: "VideoContentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PlayedHistoryId",
                table: "Profiles",
                column: "PlayedHistoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PlaylistId",
                table: "Profiles",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoContents_PlayedHistoryId",
                table: "VideoContents",
                column: "PlayedHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoContents_ProfileId",
                table: "VideoContents",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreVideoContent");

            migrationBuilder.DropTable(
                name: "PlaylistVideoContent");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "VideoContents");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "PlayedHistories");

            migrationBuilder.DropTable(
                name: "Playlists");
        }
    }
}
