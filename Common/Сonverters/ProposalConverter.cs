using Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Models.Сonverters
{
    public static class ProposalConverter
    {
        public static Proposal Convert(this ProposalRequest request)
        {
            return new Proposal 
            {
                Id = request.Id,
                Name = request.Name
            };
        }

        public static IEnumerable<ProposalResponse> Convert(this List<ProposalRequest> request)
        {
            return request.Select(record => new ProposalResponse
            {
                Id = record.Id,
                Name = record.Name
            });
        }
    }
}
