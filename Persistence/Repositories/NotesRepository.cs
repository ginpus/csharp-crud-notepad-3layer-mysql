using Dapper;
using MySql.Data.MySqlClient;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence;
using Persistence.Repositories;

namespace Persistence.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private const string TableName = "notes";
        private readonly ISqlClient _sqlClient;

        public NotesRepository(ISqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        public IEnumerable<Note> GetAll()
        {
            //var sqlSelect = $"SELECT note_id, title, text, date_created FROM {TableName}";
            var sqlSelect = $"SELECT * FROM {TableName}";
            return _sqlClient.Query<Note>(sqlSelect);
        }

        public void Save(Note note)
        {
            var sqlInsert = @$"INSERT INTO {TableName} (title, text, date_created) VALUES(@title, @text, @date_created)";

            _sqlClient.Execute(sqlInsert, new
            {
                title = note.Title,
                text = note.Text,
                date_created = note.Date_Created
            });
            //_sqlClient.Execute(sqlInsert, note);
        }

        public void Edit(int id, string title, string text)
        {
            var sqlUpdate = $"UPDATE {TableName} SET title = @new_title, text = @new_text where note_id = {id}";

            _sqlClient.Execute(sqlUpdate, new
            {
                new_title = title,
                new_text = text,
                date_created = DateTime.Now
            });
        }

        public void Delete(int id)
        {
            var sqlDelete = $"DELETE FROM {TableName} WHERE note_id = @note_id";

            _sqlClient.Execute(sqlDelete, new
            {
                note_id = id
            });
        }

        public void DeleteAll()
        {
            var sqlDeleteAll = $"DELETE FROM {TableName}";

            _sqlClient.Execute(sqlDeleteAll);
        }
    }
}