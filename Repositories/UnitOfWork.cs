using Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContactManagerContext _contactManagerContext;
        public UnitOfWork(IContactManagerContext contactManagerContext)
        {
            _contactManagerContext = contactManagerContext;
        }

        private readonly UserRepository _userRepository;
        public UserRepository UserRepository
            => _userRepository ?? new UserRepository(_contactManagerContext);

        private readonly ContactRepository _contactRepository;
        public ContactRepository ContactRepository
            => _contactRepository ?? new ContactRepository(_contactManagerContext);


        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
