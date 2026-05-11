using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassStore.Domain.Entities;

namespace ClassStore.Domain.Abstract
{
    public interface IClassRepository
    {

        IEnumerable<Class> Classes { get; }

        void SaveClass(Class cls);

        Class DeleteClass (int classID);
    }
}
