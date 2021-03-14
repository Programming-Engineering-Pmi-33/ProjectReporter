using Microsoft.EntityFrameworkCore;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class UsersStorage : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public UsersStorage(DbContextOptions<UsersStorage> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().Property(u => u.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.DateTimeModified).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Faculty>().HasIndex(f => f.Name).IsUnique();
            modelBuilder.Entity<Faculty>().Property(f => f.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<Faculty>().Property(f => f.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Faculty>().HasMany(f => f.Departments).WithOne(d => d.Faculty);
            modelBuilder.Entity<Faculty>().ToTable("Faculties");

            modelBuilder.Entity<Department>().HasIndex(d => d.Name).IsUnique();
            modelBuilder.Entity<Department>().Property(d => d.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<Department>().Property(d => d.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Department>().HasMany(d => d.AcademicGroups).WithOne(ac => ac.Department);
            modelBuilder.Entity<Department>().HasMany(d => d.Teachers).WithOne(t => t.Department);
            modelBuilder.Entity<Department>().ToTable("Departments");

            modelBuilder.Entity<AcademicGroup>().HasIndex(ac => ac.Name).IsUnique();
            modelBuilder.Entity<AcademicGroup>().Property(ac => ac.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<AcademicGroup>().Property(ac => ac.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<AcademicGroup>().HasMany(ac => ac.Students).WithOne(s => s.Group);
            modelBuilder.Entity<AcademicGroup>().ToTable("AcademicGroups");
        }
    }
}