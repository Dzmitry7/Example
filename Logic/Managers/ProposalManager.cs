using Models.DTO;
using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataProvider.Interfaces;

namespace Logic.Managers
{
    internal class ProposalManager : BaseManager, IProposalManager
    {
        private readonly IProposalRepository _proposalRepository;
        private readonly ICommitProvider _commitProvider;
        public ProposalManager(IProposalRepository proposalRepository, ICommitProvider commitProvider)
        {
            _proposalRepository = proposalRepository;
            _commitProvider = commitProvider;
        }

        public void Add(Proposal obj)
        {
            _proposalRepository.Add(obj);
            _commitProvider.Commit(string.Empty);
        }

        public void Delete(Proposal obj)
        { 
            _proposalRepository.Delete(obj);
            _commitProvider.Commit(string.Empty);
        }

        public IEnumerable<Proposal> Read(Expression<Func<Proposal, bool>> filter)
        {
           return _proposalRepository.Read(filter);
        }

        public void Update(Proposal obj)
        {
            _proposalRepository.Update(obj);
            _commitProvider.Commit(string.Empty);
        }
    }
}
