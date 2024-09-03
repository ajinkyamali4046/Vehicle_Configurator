using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vconfdn.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "components",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    compname = table.Column<string>(name: "comp_name", type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_components", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<long>(name: "user_id", type: "bigint", nullable: true),
                    modelid = table.Column<long>(name: "model_id", type: "bigint", nullable: true),
                    orderedqty = table.Column<int>(name: "ordered_qty", type: "int", nullable: true),
                    modelprice = table.Column<int>(name: "model_price", type: "int", nullable: true),
                    totalprice = table.Column<int>(name: "total_price", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "segments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    segname = table.Column<string>(name: "seg_name", type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_segments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    userid = table.Column<long>(name: "user_id", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addressline1 = table.Column<string>(name: "address_line1", type: "nvarchar(max)", nullable: false),
                    addressline2 = table.Column<string>(name: "address_line2", type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    companyname = table.Column<string>(name: "company_name", type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gstnumber = table.Column<string>(name: "gst_number", type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pincode = table.Column<string>(name: "pin_code", type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "manufacturers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    manuname = table.Column<string>(name: "manu_name", type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SegId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manufacturers", x => x.id);
                    table.ForeignKey(
                        name: "FK_manufacturers_segments_SegId",
                        column: x => x.SegId,
                        principalTable: "segments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "models",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SegId = table.Column<long>(type: "bigint", nullable: true),
                    ManuId = table.Column<long>(type: "bigint", nullable: true),
                    modname = table.Column<string>(name: "mod_name", type: "nvarchar(255)", maxLength: 255, nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    imagepath = table.Column<string>(name: "image_path", type: "nvarchar(255)", maxLength: 255, nullable: false),
                    minqty = table.Column<int>(name: "min_qty", type: "int", nullable: false),
                    safetyrating = table.Column<int>(name: "safety_rating", type: "int", nullable: true, defaultValue: 5)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_models", x => x.id);
                    table.ForeignKey(
                        name: "FK_models_manufacturers_ManuId",
                        column: x => x.ManuId,
                        principalTable: "manufacturers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_models_segments_SegId",
                        column: x => x.SegId,
                        principalTable: "segments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlternateComponents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeltaPrice = table.Column<double>(type: "float", nullable: false),
                    ModId = table.Column<long>(type: "bigint", nullable: false),
                    CompId = table.Column<long>(type: "bigint", nullable: false),
                    AltCompId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternateComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternateComponents_components_AltCompId",
                        column: x => x.AltCompId,
                        principalTable: "components",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlternateComponents_components_CompId",
                        column: x => x.CompId,
                        principalTable: "components",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlternateComponents_models_ModId",
                        column: x => x.ModId,
                        principalTable: "models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    comptype = table.Column<string>(name: "comp_type", type: "nvarchar(max)", nullable: false),
                    isconfigurable = table.Column<string>(name: "is_configurable", type: "nvarchar(max)", nullable: false),
                    ModelId = table.Column<long>(type: "bigint", nullable: false),
                    ComponentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehicles_components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "components",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vehicles_models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlternateComponents_AltCompId",
                table: "AlternateComponents",
                column: "AltCompId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateComponents_CompId",
                table: "AlternateComponents",
                column: "CompId");

            migrationBuilder.CreateIndex(
                name: "IX_AlternateComponents_ModId",
                table: "AlternateComponents",
                column: "ModId");

            migrationBuilder.CreateIndex(
                name: "IX_manufacturers_SegId",
                table: "manufacturers",
                column: "SegId");

            migrationBuilder.CreateIndex(
                name: "IX_models_ManuId",
                table: "models",
                column: "ManuId");

            migrationBuilder.CreateIndex(
                name: "IX_models_SegId",
                table: "models",
                column: "SegId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_ComponentId",
                table: "vehicles",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_ModelId",
                table: "vehicles",
                column: "ModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternateComponents");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "components");

            migrationBuilder.DropTable(
                name: "models");

            migrationBuilder.DropTable(
                name: "manufacturers");

            migrationBuilder.DropTable(
                name: "segments");
        }
    }
}
