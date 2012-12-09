using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class AgentDTO : DTOBase<Agent>
    {
        public AgentDTO() { }

        public AgentDTO(Agent entity)
            : base(entity)
        {

        }

        public AgentDTO(NHResult<Agent> result)
            : base(result)
        {

        }

        public AgentDTO(IEnumerable<Query> query, Agent entity)
            : base(query, entity)
        {
            
        }

        public string Name { get; set; }

        public int Priority { get; set; }

        public int TypeId { get; set; }

        public string Address { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedOn { get; set; }

        public AgentTypeDTO AgentType { get; set; }
    }
}
