using DesWebProyectoFinal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesWebProyectoFinal.Data
{
    public class EntityConfigs
    {
		public class CategoriaConfig : IEntityTypeConfiguration<Categoria>
		{
			public void Configure(EntityTypeBuilder<Categoria> builder)
			{
				builder.HasKey(c => c.Id);
				builder.Property(c => c.Nombre).HasColumnType("varchar(60)");
				builder.Property(c => c.Descripcion).HasColumnType("varchar(300)");
			}
		}

		public class ProveedorConfig : IEntityTypeConfiguration<Proveedor>
		{
			public void Configure(EntityTypeBuilder<Proveedor> builder)
			{
				builder.HasKey(p=> p.Id);
				builder.Property(p => p.Nombre).HasColumnType("varchar(120)");
				builder.Property(p => p.Telefono).HasColumnType("varchar(9)");
				builder.Property(p => p.Email).HasColumnType("varchar(255)");
				builder.Property(p => p.Celular).HasColumnType("varchar(9)");
				builder.Property(p => p.Direccion).HasColumnType("varchar(600)");
			}
		}

        public class ProductoConfig : IEntityTypeConfiguration<Producto>
        {
            public void Configure(EntityTypeBuilder<Producto> builder)
            {
                builder.HasKey(p=> p.Id);
				builder.Property(p => p.Nombre).HasColumnType("varchar(120)");
				builder.Property(p => p.Descripcion).HasColumnType("varchar(600)");
				builder.Property(p => p.PrecioVenta).HasColumnType("decimal(12,2)");
				builder.Property(p => p.Stock).HasColumnType("numeric(10,0)");
				//builder.HasOne(p => p.Categoria).WithOne().HasForeignKey(typeof (Producto)).OnDelete(DeleteBehavior.Restrict);
				//builder.HasOne(p => p.Proveedor).WithOne().HasForeignKey(typeof (Producto)).OnDelete(DeleteBehavior.Restrict);
            }
        }

        public class CompraConfig : IEntityTypeConfiguration<Compra>
        {
            public void Configure(EntityTypeBuilder<Compra> builder)
            {
                builder.HasKey(c => c.Id);
				builder.Property(p => p.TotalCompra).HasColumnType("decimal(12,2)");
				builder.HasMany(p => p.Detalle).WithOne(d => d.Compra);				
            }
        }

        public class DetalleCompraConfig : IEntityTypeConfiguration<DetalleCompra>
        {
            public void Configure(EntityTypeBuilder<DetalleCompra> builder)
            {
				builder.HasKey(k => new { k.CompraId, k.Linea });
				builder.Property(d => d.PrecioCompra).HasColumnType("decimal(12,2)");
				builder.Property(d => d.TotalLinea).HasColumnType("decimal(12,2)");
				builder.Property(d => d.Linea).HasColumnType("int");
				builder.Property(d => d.Cantidad).HasColumnType("int");
				builder.HasOne(d => d.Compra).WithMany(d=>d.Detalle).OnDelete(DeleteBehavior.Restrict);
				builder.HasOne(d => d.Producto).WithMany(d => d.DetallesCompra).OnDelete(DeleteBehavior.Restrict);
			}
		}

        public class ConfigSARConfig : IEntityTypeConfiguration<ConfigSAR>
        {
            public void Configure(EntityTypeBuilder<ConfigSAR> builder)
            {
				builder.HasKey(k => k.RTN);
				builder.Property(c => c.Nombre).HasColumnType("varchar(120)");
				builder.Property(c => c.DireccionFacturacion).HasColumnType("varchar(600)");
				builder.Property(c => c.TelefonoFacturacion).HasColumnType("char(9)");
				builder.Property(c => c.CorreoFacturacion).HasColumnType("varchar(255)");
				builder.Property(c => c.CodigoEstablecimientoOnline).HasColumnType("char(3)");
                builder.Property(c => c.PuntoEmisionOnline).HasColumnType("char(3)");
				builder.Property(c => c.CAIVigente).HasColumnType("char(37)");
				builder.Property(c => c.RangoDesde).HasColumnType("int");
				builder.Property(c => c.RangoHasta).HasColumnType("int");
                builder.Property(c => c.CorrelativoFacturas).HasColumnType("int");
				builder.HasMany(c => c.CAIS).WithOne(c=>c.Configuracion).OnDelete(DeleteBehavior.Restrict);
            }
        }

        public class ConfigHistoricoCAI : IEntityTypeConfiguration<HistoricoCAI>
        {
            public void Configure(EntityTypeBuilder<HistoricoCAI> builder)
            {
				builder.HasKey(h => h.Id);
				builder.Property(h => h.CAI).HasColumnType("char(37)");
				builder.Property(h => h.RangoDesde).HasColumnType("int");
                builder.Property(h => h.RangoHasta).HasColumnType("int");
            }
        }

        public class DetalleCarroCompraConfig : IEntityTypeConfiguration<DetalleCarroCompra>
        {
            public void Configure(EntityTypeBuilder<DetalleCarroCompra> builder)
            {
				builder.HasKey(d => new { d.UsuarioId, d.ProductoId });
				builder.Property(d=> d.Cantidad).HasColumnType("int");
            }
        }

        public class OrdenConfig : IEntityTypeConfiguration<Orden>
        {
            public void Configure(EntityTypeBuilder<Orden> builder)
            {
				builder.HasKey(o => o.Id);
				builder.Property(o => o.Total).HasColumnType("decimal(12,2)");
				builder.HasMany(o => o.Detalle).WithOne(d => d.Orden).OnDelete(DeleteBehavior.Restrict);
				builder.Property(o => o.DireccionEntrega).HasColumnType("varchar(600)");
            }
        }

        public class DetalleOrdenConfig : IEntityTypeConfiguration<DetalleOrden>
        {
            public void Configure(EntityTypeBuilder<DetalleOrden> builder)
            {
				builder.HasKey(d => new { d.OrdenId, d.Linea });
				builder.Property(d => d.PrecioVenta).HasColumnType("decimal(12,2)");
				builder.Property(d => d.Cantidad).HasColumnType("int");
            }
        }

        public class FacturaConfig : IEntityTypeConfiguration<Factura>
        {
            public void Configure(EntityTypeBuilder<Factura> builder)
            {
                builder.HasKey(f => f.Id);
                builder.HasIndex(f => f.Numero).IsUnique(true);
                builder.Property(f => f.CAI).HasColumnType("char(37)");
                builder.Property(f => f.DireccionFacturacion).HasColumnType("varchar(600)");
                builder.Property(f => f.RTNCliente).HasColumnType("char(14)");
                builder.Property(f => f.RTNFacturacion).HasColumnType("char(14)");
                builder.Property(f => f.RangoDesde).HasColumnType("int");
                builder.Property(f => f.RangoHasta).HasColumnType("int");
                builder.Property(f => f.NombreCliente).HasColumnType("varchar(200)");
                builder.HasMany(f => f.Detalle).WithOne(d => d.Factura);
            }
        }

        public class DetalleFacturaConfig : IEntityTypeConfiguration<DetalleFactura>
        {
            public void Configure(EntityTypeBuilder<DetalleFactura> builder)
            {
                builder.HasKey(d => new { d.FacturaId, d.Linea });
                builder.Property(d => d.PrecioBruto).HasColumnType("decimal(12,2)");
                builder.Property(d => d.PrecioVenta).HasColumnType("decimal(12,2)");
                builder.Property(d => d.ISV).HasColumnType("decimal(12,2)");
                builder.Property(d => d.TotalLinea).HasColumnType("decimal(12,2)");
                builder.Property(d => d.Cantidad).HasColumnType("int");
            }
        }
    }
}
