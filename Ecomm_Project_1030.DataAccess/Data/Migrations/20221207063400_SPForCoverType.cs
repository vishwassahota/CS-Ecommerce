using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomm_Project_1030.DataAccess.Migrations
{
    public partial class SPForCoverType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE CreateCoverType
                                @name varchar(50)
                                AS
                                insert CoverTypes values(@name)");

            migrationBuilder.Sql(@"CREATE PROCEDURE UpdateCoverType
                                @id int,
                                @name varchar(50)
                                AS
                                update CoverTypes set Name=@name where Id=@id");

            migrationBuilder.Sql(@"CREATE PROCEDURE DeleteCoverType
                                @id int
                                AS
                                delete CoverTypes where Id=@id");

            migrationBuilder.Sql(@"CREATE PROCEDURE GetCoverTypes
                                AS
                                select * from CoverTypes");

            migrationBuilder.Sql(@"CREATE PROCEDURE GetCoverType
                                @id int
                                AS
                                select * from CoverTypes where Id=@id");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
