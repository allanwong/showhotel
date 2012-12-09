using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class RoleDTO : DTOBase<Role>
    {
        public RoleDTO() { }

        public RoleDTO(Role entity)
            : base(entity)
        {

        }

        public RoleDTO(NHResult<Role> result)
            : base(result)
        {

        }

        public RoleDTO(IEnumerable<Query> query, Role entity)
            : base(query, entity)
        {
            
        }

        public string Name { get; set; }

        private List<AdministratorDTO> _Administrators = new List<AdministratorDTO>();

        public List<AdministratorDTO> Administrators
        {
            get { return _Administrators; }
            set { _Administrators = value; }
        }

        private List<ModuleDTO> _Modules = new List<ModuleDTO>();

        public List<ModuleDTO> Modules
        {
            get { return _Modules; }
            set { _Modules = value; }
        }
    }
}
