using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmsHelpdeskApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedUserToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedToUserID",
                table: "Tickets",
                newName: "AssignedToUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedToUserId",
                table: "Tickets",
                newName: "AssignedToUserID");
        }
    }
}
