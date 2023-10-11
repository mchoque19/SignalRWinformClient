using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftwareVers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MadiCustNo = table.Column<int>(type: "int", nullable: false),
                    CompNo = table.Column<int>(type: "int", nullable: false),
                    StoreNo = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermNo = table.Column<int>(type: "int", nullable: true),
                    OperNo = table.Column<int>(type: "int", nullable: true),
                    OperName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TbNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pax = table.Column<int>(type: "int", nullable: false),
                    TableType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrintOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marchando = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Family_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Family",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMonitor",
                columns: table => new
                {
                    FamiliesId = table.Column<int>(type: "int", nullable: false),
                    MonitorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMonitor", x => new { x.FamiliesId, x.MonitorsId });
                    table.ForeignKey(
                        name: "FK_FamilyMonitor_Family_FamiliesId",
                        column: x => x.FamiliesId,
                        principalTable: "Family",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FamilyMonitor_Monitor_MonitorsId",
                        column: x => x.MonitorsId,
                        principalTable: "Monitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitorState",
                columns: table => new
                {
                    MonitorsId = table.Column<int>(type: "int", nullable: false),
                    StatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorState", x => new { x.MonitorsId, x.StatesId });
                    table.ForeignKey(
                        name: "FK_MonitorState_Monitor_MonitorsId",
                        column: x => x.MonitorsId,
                        principalTable: "Monitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonitorState_State_StatesId",
                        column: x => x.StatesId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentMonitor",
                columns: table => new
                {
                    DepartmentsId = table.Column<int>(type: "int", nullable: false),
                    MonitorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentMonitor", x => new { x.DepartmentsId, x.MonitorsId });
                    table.ForeignKey(
                        name: "FK_DepartmentMonitor_Department_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentMonitor_Monitor_MonitorsId",
                        column: x => x.MonitorsId,
                        principalTable: "Monitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleMonitor",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    MonitorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleMonitor", x => new { x.ArticlesId, x.MonitorsId });
                    table.ForeignKey(
                        name: "FK_ArticleMonitor_Article_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleMonitor_Monitor_MonitorsId",
                        column: x => x.MonitorsId,
                        principalTable: "Monitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    OrderLineNo = table.Column<int>(type: "int", nullable: false),
                    PrintOrderGroupId = table.Column<int>(type: "int", nullable: true),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    Units = table.Column<int>(type: "int", nullable: false),
                    ModifList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuOrderLineNo = table.Column<int>(type: "int", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => new { x.OrderId, x.OrderLineNo });
                    table.ForeignKey(
                        name: "FK_OrderItem_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_PrintOrder_PrintOrderGroupId",
                        column: x => x.PrintOrderGroupId,
                        principalTable: "PrintOrder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItem_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_DepartmentId",
                table: "Article",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleMonitor_MonitorsId",
                table: "ArticleMonitor",
                column: "MonitorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_FamilyId",
                table: "Department",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentMonitor_MonitorsId",
                table: "DepartmentMonitor",
                column: "MonitorsId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMonitor_MonitorsId",
                table: "FamilyMonitor",
                column: "MonitorsId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitorState_StatesId",
                table: "MonitorState",
                column: "StatesId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ArticleId",
                table: "OrderItem",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_PrintOrderGroupId",
                table: "OrderItem",
                column: "PrintOrderGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_StateId",
                table: "OrderItem",
                column: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleMonitor");

            migrationBuilder.DropTable(
                name: "DepartmentMonitor");

            migrationBuilder.DropTable(
                name: "FamilyMonitor");

            migrationBuilder.DropTable(
                name: "MonitorState");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Monitor");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "PrintOrder");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Family");
        }
    }
}
