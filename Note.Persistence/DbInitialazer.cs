using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistence
{
    public class DbInitialazer
    {
        public static void Inialize(NotesDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
