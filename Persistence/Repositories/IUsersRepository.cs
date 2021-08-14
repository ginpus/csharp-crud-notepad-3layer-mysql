using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAll();

        void Save(User user);

        void Edit(int id, string username, string password);

        void Delete(int id);

        void DeleteAll();
    }
}