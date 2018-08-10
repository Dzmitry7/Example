using Models.DTO;
using System;
using System.Data.Entity;

namespace DataProvider.Context
{
    public class ProposalContext : DbContext
    {
        public static Object obj = new Object();
        private static ProposalContext _context;
        private ProposalContext()
        {
        }

        public static ProposalContext Default
        {
            get
            {
                if (_context == null)
                {
                    lock (obj)
                    {
                        _context = new ProposalContext();
                    }
                }

                return _context;
            }
        }
        public DbSet<Proposal> Proposals { get; set; }
    }
}
