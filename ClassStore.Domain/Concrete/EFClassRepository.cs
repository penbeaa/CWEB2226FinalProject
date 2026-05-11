using ClassStore.Domain.Abstract;
using ClassStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ClassStore.Domain.Concrete
{
    public class EFClassRepository : IClassRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Class> Classes
        {
            get { return context.Classes; }
        }
        void IClassRepository.SaveClass(Class cls)
        {
            if (cls.ClassID == 0)
            {
                context.Classes.Add(cls);
            }
            else
            {

                Class dbEntry = context.Classes.Find(cls.ClassID);

                if (dbEntry != null)
                {
                    dbEntry.Name = cls.Name;
                    dbEntry.Description = cls.Description;
                    dbEntry.Price = cls.Price;
                    dbEntry.Category = cls.Category;

                    dbEntry.ImageData = cls.ImageData;
                    dbEntry.ImageMimeType = cls.ImageMimeType;
                }
            }

            context.SaveChanges();
        }

        public Class DeleteClass(int classID)
        {
            Class dbEntry = context.Classes.Find(classID);
            if (dbEntry != null)
            {
                context.Classes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;

        }
    }
}