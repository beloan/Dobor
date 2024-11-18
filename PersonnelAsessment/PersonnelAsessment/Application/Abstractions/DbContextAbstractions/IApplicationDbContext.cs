using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.DbContexts
{
    public interface IApplicationDbContext 
    {
        public DbSet<User> Users { get; }
        public DbSet<Organisation> Organisations { get; }
        public DbSet<Lead> Leads { get; }
        public DbSet<Worker> Workers{ get; }
        public DbSet<Assigment> Assigments { get; }
        public DbSet<Grade> Grades { get; }
        public DbSet<Form> Forms { get; }
        public DbSet<UserImage> UserImages { get; }
    }
}
