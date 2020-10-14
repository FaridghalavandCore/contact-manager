using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        ContactRepository ContactRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken());
    }
}
