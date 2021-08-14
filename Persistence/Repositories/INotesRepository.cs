using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Models;

namespace Persistence.Repositories
{
    public interface INotesRepository
    {
        IEnumerable<Note> GetAll();

        void Save(Note note);

        void Edit(int id, string title, string text);

        void Delete(int id);

        void DeleteAll();
    }
}