using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO.Shared
{
    public class PagingResult<TEntity, TDTO> where TEntity: EntityBase where TDTO : DTOBase<TEntity>
    {
        public int PageCount { get; set; }

        public int PageNumber { get; set; }

        /// <summary>
        /// 结果过滤前的记录总数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 结果过滤后的记录总数
        /// </summary>
        public long TotalDisplay { get; set; }

        public IEnumerable<TEntity> PageOfResults { get; set; }

        public IEnumerable<TDTO> PageOfDTOs { get; set; }
    }
}
