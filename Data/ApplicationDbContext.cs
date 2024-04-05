using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DesWebProyectoFinal.Models;
using DesWebProyectoFinal.Data.Entities;
using static DesWebProyectoFinal.Data.EntityConfigs;

namespace DesWebProyectoFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<DetalleCompra> DetallesCompras { get; set; }

        public DbSet<ConfigSAR> ConfigSAR { get; set; }

        public DbSet<HistoricoCAI> HistoricoCAIS { get; set; }

        public DbSet<DetalleCarroCompra> DetalleCarroCompras { get; set; }

        public DbSet<Orden> Ordenes { get; set; }

        public DbSet<DetalleOrden> DetalleOrdenes { get; set; }

        public DbSet<Factura> Facturas { get; set; }

        public DbSet<DetalleFactura> DetalleFacturas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);            
            builder.Entity<Usuario>(entity =>
            {
                entity.ToTable(name: "Usuario");
                entity.Property(u => u.PrimerNombre).HasColumnType("varchar(30)").HasColumnName("PrimerNombre");
                entity.Property(u => u.SegundoNombre).HasColumnType("varchar(30)").HasColumnName("SegundoNombre");
                entity.Property(u => u.PrimerApellido).HasColumnType("varchar(30)").HasColumnName("PrimerApellido");
                entity.Property(u => u.SegundoApellido).HasColumnType("varchar(30)").HasColumnName("SegundoApellido");
                entity.Property(u => u.DNI).HasColumnType("char(13)").HasColumnName("DNI");
                entity.Property(u => u.RTN).HasColumnType("char(14)").HasColumnName("RTN");
                entity.Property(u => u.Direccion).HasColumnType("varchar(600)");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UsuarioRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UsuarioPermisos");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UsuarioLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RolesPermisos");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UsuarioTokens");
            });
            builder.ApplyConfiguration(new CategoriaConfig());
			builder.ApplyConfiguration(new ProveedorConfig());
            builder.ApplyConfiguration(new ProductoConfig());
            builder.ApplyConfiguration(new CompraConfig());
            builder.ApplyConfiguration(new DetalleCompraConfig());
            builder.ApplyConfiguration(new ConfigSARConfig());
            builder.ApplyConfiguration(new ConfigHistoricoCAI());
            builder.ApplyConfiguration(new DetalleCarroCompraConfig());
            builder.ApplyConfiguration(new OrdenConfig());
            builder.ApplyConfiguration(new DetalleOrdenConfig());
            builder.ApplyConfiguration(new FacturaConfig());
            builder.ApplyConfiguration(new DetalleFacturaConfig());
		}

       
    }
}