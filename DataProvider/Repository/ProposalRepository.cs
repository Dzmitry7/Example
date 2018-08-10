using DataProvider.Context;
using DataProvider.Interfaces;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataProvider.Repository
{
    public class ProposalRepository : IProposalRepository
    {
        private ProposalContext db;

        public ProposalRepository(ProposalContext db)
        {
            this.db = ProposalContext.Default;
        }

        public void Add(Proposal obj)
        {
            db.Proposals.Add(obj);
        }

        public void Delete(Proposal obj)
        {
            var proposals = db.Proposals.Find(obj.Id);
            if (proposals != null)
                db.Proposals.Remove(proposals);
        }

        public IEnumerable<Proposal> Read(Expression<Func<Proposal, bool>> filter)
        {
            return db.Proposals.Where(filter);
        }

        public void Update(Proposal obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }
    }
}
