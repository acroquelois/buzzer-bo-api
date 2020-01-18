using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buzzerApi.Dto
{
    public class PropositionDto
    {
        public Guid Id { get; set; }
        public string proposition { get; set; }
    }

    public static class PropositionExtensions
    {
        public static PropositionDto ToDto(this Models.Propositions entity)
        {
            return new PropositionDto
            {
                Id = entity.Id,
                proposition = entity.proposition
            };
        }
    }
}
