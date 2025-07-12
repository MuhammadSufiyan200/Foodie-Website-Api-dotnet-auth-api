using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication_System_API.Migrations
{
    /// <inheritdoc />
    public partial class forInventorymanegament_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_ItemCategories",
                columns: table => new
                {
                    ItemCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ItemCategories", x => x.ItemCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_PurchaseReturnRoots",
                columns: table => new
                {
                    PurchaseReturnRootId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_PurchaseReturnRoots", x => x.PurchaseReturnRootId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_PurchaseRoots",
                columns: table => new
                {
                    PurchaseRootId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_PurchaseRoots", x => x.PurchaseRootId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SaleReturnRoots",
                columns: table => new
                {
                    SaleReturnRootId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_SaleReturnRoots", x => x.SaleReturnRootId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SaleRoots",
                columns: table => new
                {
                    SaleRootId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_SaleRoots", x => x.SaleRootId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Units",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Units", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_InventoryItems",
                columns: table => new
                {
                    InventoryItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_InventoryItems", x => x.InventoryItemId);
                    table.ForeignKey(
                        name: "FK_Tbl_InventoryItems_Tbl_ItemCategories_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "Tbl_ItemCategories",
                        principalColumn: "ItemCategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_InventoryItems_Tbl_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Tbl_Units",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_PurchaseDetails",
                columns: table => new
                {
                    PurchaseDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseRootId = table.Column<int>(type: "int", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_PurchaseDetails", x => x.PurchaseDetailId);
                    table.ForeignKey(
                        name: "FK_Tbl_PurchaseDetails_Tbl_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "Tbl_InventoryItems",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_PurchaseDetails_Tbl_PurchaseRoots_PurchaseRootId",
                        column: x => x.PurchaseRootId,
                        principalTable: "Tbl_PurchaseRoots",
                        principalColumn: "PurchaseRootId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_PurchaseReturnDetails",
                columns: table => new
                {
                    PurchaseReturnDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseReturnRootId = table.Column<int>(type: "int", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_PurchaseReturnDetails", x => x.PurchaseReturnDetailId);
                    table.ForeignKey(
                        name: "FK_Tbl_PurchaseReturnDetails_Tbl_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "Tbl_InventoryItems",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_PurchaseReturnDetails_Tbl_PurchaseReturnRoots_PurchaseReturnRootId",
                        column: x => x.PurchaseReturnRootId,
                        principalTable: "Tbl_PurchaseReturnRoots",
                        principalColumn: "PurchaseReturnRootId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SaleDetails",
                columns: table => new
                {
                    SaleDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleRootId = table.Column<int>(type: "int", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_SaleDetails", x => x.SaleDetailId);
                    table.ForeignKey(
                        name: "FK_Tbl_SaleDetails_Tbl_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "Tbl_InventoryItems",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_SaleDetails_Tbl_SaleRoots_SaleRootId",
                        column: x => x.SaleRootId,
                        principalTable: "Tbl_SaleRoots",
                        principalColumn: "SaleRootId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_SaleReturnDetails",
                columns: table => new
                {
                    SaleReturnDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleReturnRootId = table.Column<int>(type: "int", nullable: false),
                    InventoryItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_SaleReturnDetails", x => x.SaleReturnDetailId);
                    table.ForeignKey(
                        name: "FK_Tbl_SaleReturnDetails_Tbl_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "Tbl_InventoryItems",
                        principalColumn: "InventoryItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_SaleReturnDetails_Tbl_SaleReturnRoots_SaleReturnRootId",
                        column: x => x.SaleReturnRootId,
                        principalTable: "Tbl_SaleReturnRoots",
                        principalColumn: "SaleReturnRootId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_InventoryItems_ItemCategoryId",
                table: "Tbl_InventoryItems",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_InventoryItems_UnitId",
                table: "Tbl_InventoryItems",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PurchaseDetails_InventoryItemId",
                table: "Tbl_PurchaseDetails",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PurchaseDetails_PurchaseRootId",
                table: "Tbl_PurchaseDetails",
                column: "PurchaseRootId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PurchaseReturnDetails_InventoryItemId",
                table: "Tbl_PurchaseReturnDetails",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PurchaseReturnDetails_PurchaseReturnRootId",
                table: "Tbl_PurchaseReturnDetails",
                column: "PurchaseReturnRootId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_SaleDetails_InventoryItemId",
                table: "Tbl_SaleDetails",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_SaleDetails_SaleRootId",
                table: "Tbl_SaleDetails",
                column: "SaleRootId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_SaleReturnDetails_InventoryItemId",
                table: "Tbl_SaleReturnDetails",
                column: "InventoryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_SaleReturnDetails_SaleReturnRootId",
                table: "Tbl_SaleReturnDetails",
                column: "SaleReturnRootId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_PurchaseDetails");

            migrationBuilder.DropTable(
                name: "Tbl_PurchaseReturnDetails");

            migrationBuilder.DropTable(
                name: "Tbl_SaleDetails");

            migrationBuilder.DropTable(
                name: "Tbl_SaleReturnDetails");

            migrationBuilder.DropTable(
                name: "Tbl_PurchaseRoots");

            migrationBuilder.DropTable(
                name: "Tbl_PurchaseReturnRoots");

            migrationBuilder.DropTable(
                name: "Tbl_SaleRoots");

            migrationBuilder.DropTable(
                name: "Tbl_InventoryItems");

            migrationBuilder.DropTable(
                name: "Tbl_SaleReturnRoots");

            migrationBuilder.DropTable(
                name: "Tbl_ItemCategories");

            migrationBuilder.DropTable(
                name: "Tbl_Units");
        }
    }
}
