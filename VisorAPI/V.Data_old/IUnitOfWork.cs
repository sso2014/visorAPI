using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace V.Data
{
    public interface IUnitOfWork
    {
        void MarkDirty(object entity);
        void MarkNew(object entity);
        void MarkDeleted(object entity);
        void MarkUpdate(object entity);
        void Commit();
        void Rollback();
    }
}
