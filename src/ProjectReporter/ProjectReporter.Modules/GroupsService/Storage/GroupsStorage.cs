using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectReporter.Modules.GroupsService.Storage
{
    public class GroupsStorage : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Report> Reports { get; set; }

        public IQueryable<Group> GetGroups() =>
            Groups.Include(g => g.Members).Include(g => g.Projects).ThenInclude(g => g.Reports)
                .Include(g => g.Tasks)
                .ThenInclude(g => g.Reports);

        public GroupsStorage(DbContextOptions<GroupsStorage> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().Property(g => g.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<Group>().Property(g => g.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Group>().HasIndex(g => new { g.Name, g.OwnerId }).IsUnique();
            modelBuilder.Entity<Group>().HasMany(g => g.Members).WithOne(gm => gm.Group);
            modelBuilder.Entity<Group>().HasMany(g => g.Projects).WithOne(p => p.Group);
            modelBuilder.Entity<Group>().HasMany(g => g.Tasks).WithOne(t => t.Group);

            modelBuilder.Entity<GroupMember>().Property(gm => gm.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<GroupMember>().Property(gm => gm.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<GroupMember>().HasIndex(gm => new { gm.UserId, gm.GroupId }).IsUnique();
            modelBuilder.Entity<GroupMember>().HasIndex(gm => gm.Guid).IsUnique();
            modelBuilder.Entity<GroupMember>().ToTable("GroupMembers");

            modelBuilder.Entity<Project>().Property(p => p.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<Project>().Property(p => p.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Project>().HasIndex(p => new { p.Name, p.GroupId }).IsUnique();
            modelBuilder.Entity<Project>().HasMany(p => p.Members).WithOne(pm => pm.Project);
            modelBuilder.Entity<Project>().HasMany(p => p.Reports).WithOne(r => r.Project);

            modelBuilder.Entity<ProjectMember>().Property(pm => pm.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<ProjectMember>().Property(pm => pm.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<ProjectMember>().HasIndex(p => new { p.UserId, p.ProjectId }).IsUnique();
            modelBuilder.Entity<ProjectMember>().ToTable("ProjectMembers");

            modelBuilder.Entity<Report>().Property(r => r.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<Report>().Property(r => r.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Report>().HasIndex(r => new { r.UserId, r.ProjectId, r.TaskId }).IsUnique();
            modelBuilder.Entity<Report>().ToTable("Reports");

            modelBuilder.Entity<Task>().Property(t => t.DateTimeCreated).ValueGeneratedOnAdd();
            modelBuilder.Entity<Task>().Property(t => t.DateTimeModified).ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Task>().HasMany(t => t.Reports).WithOne(r => r.Task);
            modelBuilder.Entity<Task>().HasIndex(t => new { t.Name, t.GroupId }).IsUnique();
        }
    }
}