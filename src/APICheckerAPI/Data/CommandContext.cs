using APICheckerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace APICheckerAPI.Data
{
    public class CommandContext:DbContext
    {
        public DbSet<Command> CommandItems {get; set;}
        public CommandContext(DbContextOptions<CommandContext> options):base(options)
        {
            
        }
    }
}