using Microsoft.Extensions.DependencyInjection;

namespace csharp_notepad_crud_3layer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var startup = new Startup();

            var serviceProvider = startup.ConfigureServices();

            var noteApp = serviceProvider.GetService<NoteApp>();

            noteApp.Start();

            //replaces below method with dependency injection (IoC) above

            /*            var fileClient = new FileClient();

            var notesRepository = new NotesRepository(fileClient);

            var notesService = new NotesService(notesRepository);

            var noteApp = new NoteApp(notesService);*/
        }
    }
}