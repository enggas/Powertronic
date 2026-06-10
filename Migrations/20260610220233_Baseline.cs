using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Powertronic.Migrations
{
    /// <inheritdoc />
    public partial class Baseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProducto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreProveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tipos_Pago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipos_Pago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venta_Prod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta_Prod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo_Id = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleado_Cargo_Cargo_Id",
                        column: x => x.Cargo_Id,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaProducto_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Proveedores_Id = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaProductoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_CategoriaProducto_CategoriaProductoId",
                        column: x => x.CategoriaProductoId,
                        principalTable: "CategoriaProducto",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Adquisicion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num_Documento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empleado_Id = table.Column<int>(type: "int", nullable: false),
                    Proveedor_Id = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adquisicion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adquisicion_Empleado_Empleado_Id",
                        column: x => x.Empleado_Id,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adquisicion_Proveedores_Proveedor_Id",
                        column: x => x.Proveedor_Id,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Despacho",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num_Factura = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cliente_Id = table.Column<int>(type: "int", nullable: false),
                    Empleado_Id = table.Column<int>(type: "int", nullable: false),
                    Venta_Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoPago_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Despacho", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Despacho_Clientes_Cliente_Id",
                        column: x => x.Cliente_Id,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despacho_Empleado_Empleado_Id",
                        column: x => x.Empleado_Id,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Despacho_Venta_Prod_Venta_Prod_Id",
                        column: x => x.Venta_Prod_Id,
                        principalTable: "Venta_Prod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Det_Venta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Venta_Prod_Id = table.Column<int>(type: "int", nullable: false),
                    Producto_Id = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Det_Venta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Det_Venta_Producto_Producto_Id",
                        column: x => x.Producto_Id,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Det_Venta_Venta_Prod_Venta_Prod_Id",
                        column: x => x.Venta_Prod_Id,
                        principalTable: "Venta_Prod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Adquisicion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adquisicion_Id = table.Column<int>(type: "int", nullable: false),
                    Producto_Id = table.Column<int>(type: "int", nullable: false),
                    PrecioAdquisicion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_Adquisicion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Detalle_Adquisicion_Adquisicion_Adquisicion_Id",
                        column: x => x.Adquisicion_Id,
                        principalTable: "Adquisicion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Detalle_Adquisicion_Producto_Producto_Id",
                        column: x => x.Producto_Id,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orden_Reparacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Factura_Id = table.Column<int>(type: "int", nullable: false),
                    Empleado_Id = table.Column<int>(type: "int", nullable: false),
                    Det_Reparacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha_Orden = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fecha_Entrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoEntrega = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orden_Reparacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orden_Reparacion_Despacho_Factura_Id",
                        column: x => x.Factura_Id,
                        principalTable: "Despacho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orden_Reparacion_Empleado_Empleado_Id",
                        column: x => x.Empleado_Id,
                        principalTable: "Empleado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pago_Tarjeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Despacho_Id = table.Column<int>(type: "int", nullable: false),
                    MarcaTarjeta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ultimos4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoAutorizacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago_Tarjeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pago_Tarjeta_Despacho_Despacho_Id",
                        column: x => x.Despacho_Id,
                        principalTable: "Despacho",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adquisicion_Empleado_Id",
                table: "Adquisicion",
                column: "Empleado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisicion_Proveedor_Id",
                table: "Adquisicion",
                column: "Proveedor_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Despacho_Cliente_Id",
                table: "Despacho",
                column: "Cliente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Despacho_Empleado_Id",
                table: "Despacho",
                column: "Empleado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Despacho_Venta_Prod_Id",
                table: "Despacho",
                column: "Venta_Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Det_Venta_Producto_Id",
                table: "Det_Venta",
                column: "Producto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Det_Venta_Venta_Prod_Id",
                table: "Det_Venta",
                column: "Venta_Prod_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Adquisicion_Adquisicion_Id",
                table: "Detalle_Adquisicion",
                column: "Adquisicion_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Adquisicion_Producto_Id",
                table: "Detalle_Adquisicion",
                column: "Producto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_Cargo_Id",
                table: "Empleado",
                column: "Cargo_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Reparacion_Empleado_Id",
                table: "Orden_Reparacion",
                column: "Empleado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_Reparacion_Factura_Id",
                table: "Orden_Reparacion",
                column: "Factura_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Tarjeta_Despacho_Id",
                table: "Pago_Tarjeta",
                column: "Despacho_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_CategoriaProductoId",
                table: "Producto",
                column: "CategoriaProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Det_Venta");

            migrationBuilder.DropTable(
                name: "Detalle_Adquisicion");

            migrationBuilder.DropTable(
                name: "Orden_Reparacion");

            migrationBuilder.DropTable(
                name: "Pago_Tarjeta");

            migrationBuilder.DropTable(
                name: "Tipos_Pago");

            migrationBuilder.DropTable(
                name: "Adquisicion");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Despacho");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "CategoriaProducto");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Venta_Prod");

            migrationBuilder.DropTable(
                name: "Cargo");
        }
    }
}
