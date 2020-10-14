using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Context
{
    public interface IContactManagerContext
    {
       
        DbSet<TEntity> Set<TEntity>() where TEntity : class;


        #region.:: ENTITIES ::.
         DbSet<User> Users { get; set; }
         DbSet<Contact> Contacts { get; set; }
        
        #endregion


        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken());

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
