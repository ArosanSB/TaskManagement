using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Data.Entity;
namespace Infrastructure.Persistance
{
    public class BusinessLogicDbContext : DbContext
    {
        public BusinessLogicDbContext(DbContextOptions<BusinessLogicDbContext> options) : base(options)
        {
        }
        public DbSet<TaskItemEntity> Tasks { get; set; }
    }
}
