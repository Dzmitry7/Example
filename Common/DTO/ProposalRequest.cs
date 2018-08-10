using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Extentions;
using Models.Interfaces;

namespace Models.DTO
{
    [NotMapped]
    public class ProposalRequest : IBaseFilterModel
    {
        public Guid Id { get; set; }
         
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, 1000)]
        public int Take { get; set; }
        public int Skip { get; set; }

        public string Token { get; set; } 

        public override int GetHashCode()
        {
            return Hash.StringToSHA512(ToString());
        }
    }
}
