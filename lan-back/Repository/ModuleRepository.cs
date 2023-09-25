using AutoMapper;
using lan_back.Controllers;
using lan_back.Data;
using lan_back.Interfaces;
using lan_back.Models;

namespace lan_back.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly DataContext _context;

        public ModuleRepository(DataContext context)
        {
            _context = context;
        } 
        public Module GetModule(int id)
        {
            return _context.Modules.Where(m => m.Id == id).FirstOrDefault();
        }

        public ICollection<Module> GetModules()
        {
            return _context.Modules.ToList();
        }

        public bool ModuleExists(int id)
        {
            return _context.Modules.Any(m=>m.Id == id);
        }
        public ICollection<Flashcard> GetFlashCardsFromModule(int id)
        {
            return _context.Flashcards.Where(f=>f.Module.Id==id).ToList();
        }

        public ICollection<Lesson> GetLessonsFromModule(int id)
        {
            return _context.Lessons.Where(l => l.Module.Id == id).ToList();

        }

        public bool CreateModule(Module module)
        {
            _context.Add(module);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateModule(Module module)
        {
            _context.Update(module);
            return Save();
        }

        public bool DeleteModule(Module module)
        {
            _context.Remove(module);
            return Save();
        }
    }
}
