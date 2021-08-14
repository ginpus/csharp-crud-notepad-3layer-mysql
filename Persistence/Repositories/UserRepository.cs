using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private const string FileName = "users.txt";
        private readonly IFileClient _fileClient;

        public UserRepository(IFileClient fileClient)
        {
            _fileClient = fileClient;
        }

        public IEnumerable<User> GetAll()
        {
            return _fileClient.ReadAll<User>(FileName);
        }

        public void Save(User user)
        {
            _fileClient.Append(FileName, user);
        }

        public void Edit(int id, string username, string password)
        {
            var allUsers = _fileClient.ReadAll<User>(FileName).ToList(); // ToList() forces IEnumerable to be a list. Better for performance. It is neither Array nor List - it is IEnumerable by default.
            var userToUpdate = allUsers.First(user => user.Id == id);

            userToUpdate.Username = username;
            userToUpdate.Password = password;

            _fileClient.WriteAll(FileName, allUsers);
        }

        public void Delete(int id)
        {
            var allUsers = _fileClient.ReadAll<User>(FileName).ToList();
            var updatedUsers = allUsers.Where(note => note.Id != id); // all notes are preserved in the list apart the one defined by the user

            _fileClient.WriteAll(FileName, updatedUsers);
        }

        public void DeleteAll()
        {
            _fileClient.DeleteFileContents(FileName);
        }
    }
}