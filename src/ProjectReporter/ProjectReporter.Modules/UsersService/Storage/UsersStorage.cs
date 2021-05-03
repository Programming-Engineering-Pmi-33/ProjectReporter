using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectReporter.Modules.UsersService.Storage
{
    public class UsersStorage : IdentityDbContext<User>
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AcademicGroup> AcademicGroups { get; set; }

        public IQueryable<Faculty> FindFaculties() =>
            Faculties.Include(f => f.AcademicGroups).Include(f => f.Departments);

        public IQueryable<Department> FindDepartments() => 
            Departments.Include(d => d.Teachers).Include(d => d.Faculty);

        public IQueryable<AcademicGroup> FindAcademicGroups() => AcademicGroups.Include(a => a.Faculty);

        public UsersStorage(DbContextOptions<UsersStorage> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(u => u.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.DateTimeModified).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Faculty>().HasIndex(f => f.Name).IsUnique();
            modelBuilder.Entity<Faculty>().Property(f => f.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<Faculty>().Property(f => f.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Faculty>().HasMany(f => f.Departments).WithOne(d => d.Faculty);
            modelBuilder.Entity<Faculty>().HasMany(d => d.AcademicGroups).WithOne(ac => ac.Faculty);
            modelBuilder.Entity<Faculty>().ToTable("Faculties");

            modelBuilder.Entity<Department>().HasIndex(d => d.Name).IsUnique();
            modelBuilder.Entity<Department>().Property(d => d.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<Department>().Property(d => d.DateTimeModified).ValueGeneratedOnAddOrUpdate();
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