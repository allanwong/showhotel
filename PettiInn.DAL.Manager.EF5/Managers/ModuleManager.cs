using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;
using PettiInn.DAL.Manager.Core.Managers;

namespace PettiInn.DAL.Manager.EF5.Managers
{
    internal class ModuleManager : ManagerBase<Module>, IModuleManager
    {
        public IEnumerable<Module> GetRoots()
        {
            return base.Query().Where(m => m.ParentId == null).OrderBy(m => m.Sort);
        }

        public IEnumerable<Module> GetByRootsAdmin(int adminId)
        {
            var aManager = new AdministratorManager();
            var admin = aManager.GetById(adminId);

            if (admin.IsSuper)
            {
                return this.GetRoots();
            }
            else
            {
                var all = (from m in this.GetAll(admin.IsSuper) from r in m.Roles from a in r.Administrators where m.ParentId == null && a.Id == adminId select m).Distinct();
                return all;
            }
        }

        public IEnumerable<Module> GetAll(bool? isSuper)
        {
            var q = this.GetAll();

            if (isSuper != null)
            {
                q = q.Where(m => m.IsSuper == isSuper.Value);
            }

            return q;
        }
    }
}
