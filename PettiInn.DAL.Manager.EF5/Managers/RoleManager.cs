using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;
using PettiInn.DAL.Manager.Core.Managers;
using PettiInn.SOA.DTO;

namespace PettiInn.DAL.Manager.EF5.Managers
{
    internal class RoleManager : ManagerBase<Role>, IRoleManager
    {
        public NHResult<Role> Create(RoleDTO dto)
        {
            var role = new Role
            {
                Name = dto.Name
            };

            var mManager = new ModuleManager();
            var modules = mManager.GetByIds(dto.Modules.Select(m => m.Id));
            role.Modules = modules.ToList();

            var result = base.SaveOrUpdate(role);

            return result;
        }

        public NHResult<Role> Update(SOA.DTO.RoleDTO dto)
        {
            var role = base.GetById(dto.Id);

            var mManager = new ModuleManager();
            var modules = mManager.GetByIds(dto.Modules.Select(m => m.Id));
            role.Modules.Clear();

            foreach (var r in modules)
            {
                role.Modules.Add(r);
            }

            var result = base.SaveOrUpdate(role);

            return result;
        }

        //public override NHResult<Role> Delete(int id)
        //{
        //    var result = new NHResult<Role>();
        //    var obj = this.GetById(id);

        //    if (obj != null)
        //    {
        //        obj.Modules.Clear();
        //        obj.Administrators.Clear();
        //        this.ObjectContext.DeleteObject(obj);
        //        //this.ObjectContext.ObjectStateManager.ChangeObjectState(obj, EntityState.Deleted);//将附加的实体状态更改为删除
        //        var count = this.ObjectContext.SaveChanges();
        //        result.Extra.Add("count", count);
        //    }

        //    return result;
        //}
    }
}
