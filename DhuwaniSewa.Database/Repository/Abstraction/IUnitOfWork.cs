using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DhuwaniSewa.Database.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        Task CommitAsync();
        Task<IDbContextTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
    }
}
