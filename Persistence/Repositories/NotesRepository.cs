using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private const string FileName = "notes.txt";
        private readonly IFileClient _fileClient;

        public NotesRepository(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }

        public IEnumerable<Note> GetAll()
        {
            return _fileClient.ReadAll<Note>(FileName);
        }

        public void Save(Note note)
        {
            _fileClient.Append(FileName, note);
        }

        public void Edit(int id, string title, string text)
        {
            var allNotes = _fileClient.ReadAll<Note>(FileName).ToList(); // ToList() forces IEnumerable to be a list. Better for performance. It is neither Array nor List - it is IEnumerable by default.
            var noteToUpdate = allNotes.First(note => note.Id == id);

            noteToUpdate.Title = title;
            noteToUpdate.Text = text;

            _fileClient.WriteAll(FileName, allNotes);
        }

        public void Delete(int id)
        {
            var allNotes = _fileClient.ReadAll<Note>(FileName).ToList();
            var updatedNotes = allNotes.Where(note => note.Id != id); // all notes are preserved in the list apart the one defined by the user

            _fileClient.WriteAll(FileName, updatedNotes);
        }

        public void DeleteAll()
        {
            _fileClient.DeleteFileContents(FileName);
        }
    }
}