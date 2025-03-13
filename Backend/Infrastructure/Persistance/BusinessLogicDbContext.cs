using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Persistance
{
    public class BusinessLogicDbContext : DbContext
    {
        public DbSet<TaskItemEntity> Tasks { get; set; }
        public BusinessLogicDbContext(DbContextOptions<BusinessLogicDbContext> options) : base(options)
        {
        }
    }
}
