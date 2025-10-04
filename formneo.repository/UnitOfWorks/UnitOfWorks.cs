using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.UnitOfWorks;

namespace formneo.repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext _context;

        private IDbContextTransaction _transaction;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();

            _transaction.Commit();


        }

        public async Task CommitAsync()
        {

            var result = await _context.SaveChangesAsync();


            if (_transaction != null)
                _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }
    }
}
