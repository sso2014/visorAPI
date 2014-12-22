using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisorAPI.Core
{
    public interface IUnitOfWork {
        void registerNew(object obj);
        void registerDirty(object obj);
        void registerClean(object obj);
        void registerDeleted(object obj);
        void commit();
        void rollback();
    }
}
