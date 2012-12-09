using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class AgentTypeDTO : DTOBase<AgentType>
    {
        public AgentTypeDTO() { }

        public AgentTypeDTO(AgentType entity)
            : base(entity)
        {

        }

        public AgentTypeDTO(NHResult<AgentType> result)
            : base(result)
        {

        }

        public AgentTypeDTO(IEnumerable<Query> query, AgentType entity)
            : base(query, entity)
        {
            
        }

        public string Name { get; set; }

        public int Sort { get; set; }
    }
}
