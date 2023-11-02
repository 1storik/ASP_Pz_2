using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_Pz_2.Migrations
{
    public partial class AddClientMembershipEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientMemberships",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false),
                    MembershipId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientMemberships", x => new { x.ClientId, x.MembershipId });
                    table.ForeignKey(
                        name: "FK_ClientMemberships_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientMemberships_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "MembershipId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientMemberships_MembershipId",
                table: "ClientMemberships",
                column: "MembershipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientMemberships");
        }
    }
}
