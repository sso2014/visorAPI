using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorRemoting.V6.Respository
{
    public interface IUnitOfWork:IDisposable
    {
        void SaveChanges();
    }
}
