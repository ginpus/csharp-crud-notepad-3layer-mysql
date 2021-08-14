using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IFileClient // T willbe any class that we will create
    {
        IEnumerable<T> ReadAll<T>(string fileName); // common method to read any type of objects and return into some list (Enumerable)

        void Append<T>(string fileName, T item);

        void WriteAll<T>(string fileName, IEnumerable<T> items);

        void DeleteFileContents(string fileName);
    }
}