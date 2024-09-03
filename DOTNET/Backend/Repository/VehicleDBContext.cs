using Microsoft.EntityFrameworkCore;
using v_conf_dn.Models;

namespace v_conf_dn.Repository
{
    public class VehicleDBContext : DbContext
    {
        public VehicleDBContext(DbContextOptions<VehicleDBContext> options)
            : base(options)
        {
        }

        public DbSet<AlternateComponent> AlternateComponents { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        // Add other DbSet properties here


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=v_conf;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure AlternateComponent to Model relationship
            modelBuilder.Entity<AlternateComponent>()
                .HasOne(ac => ac.Model)
                .WithMany() // Adjust if there is a collection in Model
                .HasForeignKey(ac => ac.ModId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading delete

            // Configure AlternateComponent to Component relationship
            modelBuilder.Entity<AlternateComponent>()
                .HasOne(ac => ac.Component)
                .WithMany() // Adjust if there is a collection in Component
                .HasForeignKey(ac => ac.CompId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading delete

            // Configure AlternateComponent to AlternateComponent relationship (if applicable)
            modelBuilder.Entity<AlternateComponent>()
                .HasOne(ac => ac.AltComp)
                .WithMany() // Adjust if there is a collection in Component
                .HasForeignKey(ac => ac.AltCompId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading delete

            modelBuilder.Entity<Component>(entity =>
            {
                // Table name
                entity.ToTable("components");

                // Primary key
                entity.HasKey(e => e.Id);

                // Column configurations
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.CompName)
                    .IsRequired()
                    .HasColumnName("comp_name")
                    .HasMaxLength(255);
            });
            modelBuilder.Entity<Invoice>(entity =>
            {
                // Table name
                entity.ToTable("invoices");

                // Primary key
                entity.HasKey(e => e.Id);

                // Column configurations
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.ModelId)
                    .HasColumnName("model_id");

                entity.Property(e => e.OrderedQty)
                    .HasColumnName("ordered_qty");

                entity.Property(e => e.ModelPrice)
                    .HasColumnName("model_price");

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("total_price");

                // Handle AltCompId as a separate table or serialization
                // For simplicity, assuming you use a separate table to store AltCompIds
                entity.Ignore(e => e.AltCompId);

                // You would need to handle the list of AltCompId with a separate entity or a different approach
            });
            modelBuilder.Entity<Manufacturer>(entity =>
            {
                // Table name
                entity.ToTable("manufacturers");

                // Primary key
                entity.HasKey(e => e.Id);

                // Column configurations
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ManuName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("manu_name");

                // Configure foreign key
                entity.HasOne(e => e.Segment)
                    .WithMany() // Assuming Segment does not have a collection of Manufacturers
                    .HasForeignKey("SegId")
                    .OnDelete(DeleteBehavior.Restrict); // Adjust the delete behavior as needed
            });
            modelBuilder.Entity<Segment>(entity =>
            {
                entity.ToTable("segments");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.SegName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("seg_name");
            });
            modelBuilder.Entity<Model>(entity =>
            {
                // Table name
                entity.ToTable("models");

                // Primary key
                entity.HasKey(e => e.Id);

                // Columns
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ModName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("mod_name");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnName("price");

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("image_path");

                entity.Property(e => e.MinQty)
                    .IsRequired()
                    .HasColumnName("min_qty");

                entity.Property(e => e.SafetyRating)
                    .HasDefaultValue(5)
                    .HasColumnName("safety_rating");

                // Relationships
                entity.HasOne(e => e.Segment)
                    .WithMany() // Adjust if there is a navigation property in Segment
                    .HasForeignKey("SegId")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Manufacturer)
                    .WithMany() // Adjust if there is a navigation property in Manufacturer
                    .HasForeignKey("ManuId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<User>(entity =>
            {
                // Table name
                entity.ToTable("user");

                // Primary key
                entity.HasKey(e => e.UserId);

                // Columns
                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("user_id");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasColumnName("address_line1");

                entity.Property(e => e.AddressLine2)
                    .HasColumnName("address_line2");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("company_name");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.GstNumber)
                    .IsRequired()
                    .HasColumnName("gst_number");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.PinCode)
                    .IsRequired()
                    .HasColumnName("pin_code");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state");

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasColumnName("telephone");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username");
            });
            modelBuilder.Entity<Vehicle>(entity =>
            {
                // Table name
                entity.ToTable("vehicles");

                // Primary key
                entity.HasKey(e => e.Id);

                // Columns
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CompType)
                    .IsRequired()
                    .HasColumnName("comp_type")
                    .HasConversion(
                        v => v.ToString(),
                        v => (CompType)Enum.Parse(typeof(CompType), v));

                entity.Property(e => e.IsConfigurable)
                    .IsRequired()
                    .HasColumnName("is_configurable")
                    .HasConversion(
                        v => v.ToString(),
                        v => (IsConfigurable)Enum.Parse(typeof(IsConfigurable), v));

                // Foreign Key Configuration
                entity.HasOne(e => e.Mod)
                    .WithMany() // Configure the inverse navigation if needed
                    .HasForeignKey(e => e.ModelId)
                    .OnDelete(DeleteBehavior.Cascade); // Adjust as needed

                entity.HasOne(e => e.Component)
                    .WithMany() // Configure the inverse navigation if needed
                    .HasForeignKey(e => e.ComponentId)
                    .OnDelete(DeleteBehavior.Cascade); // Adjust as needed
            });
        }
    }
}
