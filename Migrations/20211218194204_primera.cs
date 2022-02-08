using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Final.Migrations
{
    public partial class primera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dni = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CUITCUIL = table.Column<long>(type: "bigint", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    attemps = table.Column<int>(type: "int", nullable: false),
                    locked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ammount = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.cartId);
                    table.ForeignKey(
                        name: "FK_Cart_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    logId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eventType = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.logId);
                    table.ForeignKey(
                        name: "FK_Logs_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    purchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    total = table.Column<double>(type: "float", nullable: false),
                    buyeruserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.purchaseId);
                    table.ForeignKey(
                        name: "FK_Purchases_Users_buyeruserId",
                        column: x => x.buyeruserId,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    ammount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => new { x.cartId, x.productId });
                    table.ForeignKey(
                        name: "FK_CartProduct_Cart_cartId",
                        column: x => x.cartId,
                        principalTable: "Cart",
                        principalColumn: "cartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProduct_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartPurchase",
                columns: table => new
                {
                    purchaseId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartPurchase", x => new { x.purchaseId, x.productId });
                    table.ForeignKey(
                        name: "FK_CartPurchase_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartPurchase_Purchases_purchaseId",
                        column: x => x.purchaseId,
                        principalTable: "Purchases",
                        principalColumn: "purchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    percentage = table.Column<int>(type: "int", nullable: false),
                    CartPurchaseproductId = table.Column<int>(type: "int", nullable: true),
                    CartPurchasepurchaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.id);
                    table.ForeignKey(
                        name: "FK_Coupons_CartPurchase_CartPurchasepurchaseId_CartPurchaseproductId",
                        columns: x => new { x.CartPurchasepurchaseId, x.CartPurchaseproductId },
                        principalTable: "CartPurchase",
                        principalColumns: new[] { "purchaseId", "productId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseCoupon",
                columns: table => new
                {
                    purchaseId = table.Column<int>(type: "int", nullable: false),
                    couponId = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseCoupon", x => new { x.purchaseId, x.couponId });
                    table.ForeignKey(
                        name: "FK_PurchaseCoupon_Coupons_couponId",
                        column: x => x.couponId,
                        principalTable: "Coupons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseCoupon_Purchases_purchaseId",
                        column: x => x.purchaseId,
                        principalTable: "Purchases",
                        principalColumn: "purchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_userId",
                table: "Cart",
                column: "userId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartProduct_productId",
                table: "CartProduct",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_CartPurchase_productId",
                table: "CartPurchase",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CartPurchasepurchaseId_CartPurchaseproductId",
                table: "Coupons",
                columns: new[] { "CartPurchasepurchaseId", "CartPurchaseproductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_userId",
                table: "Logs",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_categoryId",
                table: "Products",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseCoupon_couponId",
                table: "PurchaseCoupon",
                column: "couponId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_buyeruserId",
                table: "Purchases",
                column: "buyeruserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "PurchaseCoupon");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "CartPurchase");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
