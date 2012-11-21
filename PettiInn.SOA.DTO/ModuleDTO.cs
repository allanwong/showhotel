using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class ModuleDTO : DTOBase<Module>
    {
        public ModuleDTO() { }

        public ModuleDTO(Module entity) : base(entity)
        {

        }

        public ModuleDTO(IEnumerable<Query> query, Module entity)
            : base(query, entity)
        {
            
        }

        public string Name { get; set; }

        public string CSS { get; set; }

        public string Class { get; set; }

        public int? ParentId { get; set; }

        public ModuleDTO Parent { get; set; }

        public bool IsSuper { get; set; }

        public int Sort { get; set; }

        private List<ModuleDTO> _Children = new List<ModuleDTO>();

        public List<ModuleDTO> Children
        {
            get { return _Children; }
            set { _Children = value; }
        }

        private List<RoleDTO> _Roles = new List<RoleDTO>();

        public List<RoleDTO> Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }
    }
}
