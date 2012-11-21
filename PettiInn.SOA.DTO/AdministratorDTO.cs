using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class AdministratorDTO : DTOBase<Administrator>
    {
        public AdministratorDTO() { }

        public AdministratorDTO(Administrator entity)
            : base(entity)
        {

        }

        public AdministratorDTO(NHResult<Administrator> result)
            : base(result)
        {

        }

        public AdministratorDTO(IEnumerable<Query> query, Administrator entity)
            : base(query, entity)
        {
            
        }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }

        public DateTime CreatedOn { get; protected set; }

        public DateTime? LastLoggedIn { get; set; }

        public bool IsSuper { get; set; }

        private IList<RoleDTO> _Roles = new List<RoleDTO>();

        public IList<RoleDTO> Roles
        {
            get { return _Roles; }
            set { _Roles = value; }
        }
    }
}
