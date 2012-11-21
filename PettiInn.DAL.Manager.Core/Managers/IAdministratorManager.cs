using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities;
using PettiInn.SOA.DTO;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.DAL.Manager.Core.Managers
{
    public interface IAdministratorManager : IManagerBase<Administrator>
    {
        NHResult<Administrator> Create(AdministratorDTO dto);

        NHResult<Administrator> Authenticate(string userName, string password);

        NHResult<Administrator> MarkLogin(int adminId);

        NHResult<Administrator> Update(AdministratorDTO dto);

        NHResult<Administrator> UpdatePassword(AdministratorDTO dto);

        NHResult<Administrator> UpdateMyPassword(AdministratorDTO dto, string oldPassword);

        Administrator GetByUserName(string userName);

        bool IsUserNameUnique(string userName, int? editingId = null);
    }
}
