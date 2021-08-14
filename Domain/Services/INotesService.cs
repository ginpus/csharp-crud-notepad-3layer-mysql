using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface INotesService // according to functions/commands in interface
    {
        public IEnumerable<Note> GetAll();

        public void Create(Note note);

        public void Edit(int id, string title, string text);

        public void DeleteById(int id);

        public void ClearAll();
    }
}