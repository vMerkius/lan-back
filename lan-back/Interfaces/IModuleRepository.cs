using lan_back.Models;

namespace lan_back.Interfaces
{
    public interface IModuleRepository
    {
        Module GetModule(int id);
        ICollection<Module> GetModules();
        bool ModuleExists(int id);
        ICollection<Lesson> GetLessonsFromModule(int id);
        ICollection<Flashcard> GetFlashCardsFromModule(int id);
        bool CreateModule(Module module);
        bool UpdateModule(Module module);
        bool DeleteModule(Module module);
        bool Save();
    }
}
