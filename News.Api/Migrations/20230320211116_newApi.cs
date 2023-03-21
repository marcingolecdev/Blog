using Microsoft.EntityFrameworkCore.Migrations;

namespace News.Api.Migrations
{
    public partial class newApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDodania = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "DataDodania", "Description", "Title" },
                values: new object[,]
                {
                    { "20c3cbdc-3565-46a7-80d2-1dc30e5b9b36", "20.03.2023", "This is a list of online newspaper archives and some magazines and journals, including both free and pay wall blocked digital archives. Most are scanned from microfilm into pdf, gif or similar graphic formats and many of the graphic archives have been indexed into searchable text dders.", "Wiadomości – program informacyjny" },
                    { "6a3d9001-2181-4da0-8876-e65677d59a09", "20.03.2023", "Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings f  in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field.", "This is a list of online newspaper archives" },
                    { "5b6d993a-538e-42c5-9e19-a7c4f40ebf6e", "20.03.2023", " s interested in the teachings from the field. Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field. Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field. Wayne State University in Detroit is now adding a bachelor’s in law for undergraduate students interested in the teachings from the field. ", "Archives and some magazines and journals" },
                    { "7c5b95e3-8bf4-432b-8d2c-c62ae3ee43ce", "20.03.2023", "Provost committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.", "Including both free and pay wall blocked digital" },
                    { "880b07f6-96af-4b34-b318-751c8b6f1141", "20.03.2023", "Provost  m that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.Provost and Senior Vice President for Academic Affairs Mark Kornbluh told the committee that the B.A. in law is designed to be an intradisciplinary program that will provide students with a broad-based liberal arts degree.", " Most are scanned from microfilm into pdf" },
                    { "61f3467b-1edb-4d0e-b3b8-709379e7da95", "20.03.2023", "Though  ture.Though the degree will not enable students to take the state bar exam and practice law, it will pave the way to law-adjoining careers or prepare students for a juris doctor in the future.Though the degree will not enable students to take the state bar exam and practice law, it will pave the way to law-adjoining careers or prepare students for a juris doctor in the future.", "Searchable text databases utilizing" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
