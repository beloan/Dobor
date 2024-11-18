using Application.Abstractions.DbContexts;
using Domain.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Organisation> Organisations => Set<Organisation>();
        public DbSet<Lead> Leads => Set<Lead>();
        public DbSet<Worker> Workers => Set<Worker>();
        public DbSet<Form> Forms => Set<Form>();
        public DbSet<Assigment> Assigments => Set<Assigment>();
        public DbSet<Grade> Grades => Set<Grade>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<JobList> JobLists => Set<JobList>();
        public DbSet<UserImage> UserImages => Set<UserImage>();

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurations();

            base.OnModelCreating(modelBuilder);
        }
    }
}
