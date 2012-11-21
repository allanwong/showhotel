using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.DAL.Manager.Core
{
    public interface IManagerBase<T> : IDisposable where T : EntityBase
    {
        #region 事务

        /// <summary>
        /// 开始事务，事务结束后必须Commit,否则更改不会提交或锁死数据库
        /// </summary>
        DbTransaction BeginTransaction();

        /// <summary>
        /// 提交事务并关闭当前会话，但不会关闭数据库连接
        /// </summary>
        DbTransaction CommitTransaction(DbTransaction trans);

        /// <summary>
        /// 回滚事务
        /// </summary>
        DbTransaction RollbackTransaction(DbTransaction trans);

        #endregion

        #region 查询
        IQueryable<T> Query();

        IQueryable<T> GetAll();

        T GetById(int Id);

        IQueryable<T> GetByIds(IEnumerable<int> Ids);

        NHResult<T> Delete(int id);
        #endregion
    }
}
