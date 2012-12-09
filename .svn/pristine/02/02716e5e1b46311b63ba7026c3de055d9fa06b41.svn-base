using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;
using PettiInn.DAL.Manager.Core.Managers;

namespace PettiInn.DAL.Manager.EF5.Managers
{
    internal class AgentManager : ManagerBase<Agent>, IAgentManager
    {
        public override IQueryable<Agent> GetAll()
        {
            return base.Query().OrderBy(a => a.Priority);
        }

        public override NHResult<Agent> Delete(int id)
        {
            var result = new NHResult<Agent>();
            var obj = this.GetById(id);

            if (obj.RoomBookings.Count > 0)
            {
                result.Errors.Add("该中介已经存在房间预订，无法删除");
            }

            if (result.IsValid)
            {
                result = base.Delete(id);
            }

            return result;
        }
    }
}
