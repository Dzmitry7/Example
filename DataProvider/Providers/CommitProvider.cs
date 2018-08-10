using DataProvider.Context;
using DataProvider.Interfaces;
using System; 

namespace DataProvider.Providers
{
    internal class CommitProvider : ICommitProvider
    {
        private readonly ProposalContext _context;
        public CommitProvider()
        {
            _context = ProposalContext.Default;
        }

        public void Commit(string comment = "")
        {
            if (_context.Database.CurrentTransaction != null)
            {
                _context.SaveChanges();
            }
            else
            {
                using (var transaction = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
