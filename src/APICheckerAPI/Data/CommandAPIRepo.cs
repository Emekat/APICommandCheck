using System.Collections.Generic;
using System.Linq;
using APICheckerAPI.Models;

namespace APICheckerAPI.Data
{
    public class CommandAPIRepo:ICommandAPIRepo
    {
        private readonly CommandContext _context;
        public CommandAPIRepo(CommandContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.CommandItems.ToList();
        }
        public Command GetCommandById(int id)
        {
            return _context.CommandItems.FirstOrDefault(p => p.Id == id);
        }

        public void CreateCommand(Command cmd)
        {
            if(cmd == null)
                throw new System.NotImplementedException();
            _context.CommandItems.Add(cmd);
        }

        public void UpdateCommand(Command cmd)
        {
           
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}