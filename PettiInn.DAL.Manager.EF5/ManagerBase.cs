using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PettiInn.DAL.Entities.EF5;
using PettiInn.DAL.Manager.Core;
using PettiInn.Utilities;

namespace PettiInn.DAL.Manager.EF5
{
    internal abstract class ManagerBase<TEntity> : IManagerBase<TEntity> where TEntity : EntityBase
    {
        private bool _disposed = false;

        public ObjectContext ObjectContext
        {
            get
            {
                return ObjectContextFactory.GetCurrentObjectContext() as ObjectContext;
            }
        }

        ~ManagerBase()
        {
            Dispose(true);
        }

        public DbTransaction BeginTransaction()
        {
            this.ObjectContext.Connection.Open();
            var trans = this.ObjectContext.Connection.BeginTransaction();

            return trans;
        }

        public DbTransaction CommitTransaction(DbTransaction trans)
        {
            if (trans != null)
            {
                try
                {
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    if (this.ObjectContext != null && this.ObjectContext.Connection.State != ConnectionState.Closed)
                    {
                        this.ObjectContext.Connection.Close();
                    }
                }
            }

            return trans;
        }

        public DbTransaction RollbackTransaction(DbTransaction trans)
        {
            if (trans != null)
            {
                trans.Rollback();
            }

            return trans;
        }

        public IQueryable<TEntity> Query()
        {
            return this.ObjectContext.CreateObjectSet<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            var query = "SELECT VALUE N FROM {0} AS N";
 
            if (typeof(ISort).IsAssignableFrom(typeof(TEntity)))
            {
                query = string.Concat(query, " order by N.Sort");
            }
            var q = this.ObjectContext.CreateQuery<TEntity>(string.Format(query, typeof(TEntity).Name));

            return q;
        }

        public TEntity GetById(int Id)
        {
            return this.ObjectContext.CreateObjectSet<TEntity>().FirstOrDefault(t => t.Id == Id);
        }

        public IQueryable<TEntity> GetByIds(IEnumerable<int> Ids)
        {
            var results = new List<TEntity>().AsQueryable();

            if (Ids != null && Ids.Count() > 0)
            {
                var q = string.Format("select value p from {0} as p where p.Id in {{{1}}}", typeof(TEntity).Name, string.Join(",", Ids));
                results = this.ObjectContext.CreateQuery<TEntity>(q);
            }

            return results;
        }

        public virtual NHResult<TEntity> Delete(int id)
        {
            var result = new NHResult<TEntity>();
            var obj = this.GetById(id);

            if (obj != null)
            {
                this.ObjectContext.CreateObjectSet<TEntity>().Attach(obj);
                this.ObjectContext.DeleteObject(obj);
                //this.ObjectContext.ObjectStateManager.ChangeObjectState(obj, EntityState.Deleted);//将附加的实体状态更改为删除
                var count = this.ObjectContext.SaveChanges();
                result.Extra.Add("count", count);
            }

            return result;
        }

        public void Dispose()
        {
            Dispose(false);
        }

        private void Dispose(bool finalizing)
        {
            if (!_disposed)
            {
                if (!finalizing)
                    GC.SuppressFinalize(this);

                _disposed = true;
            }
        }

        #region 事物
        public NHResult<TEntity> SaveOrUpdate(TEntity entity)
        {
            var result = new NHResult<TEntity>();

            result = this.Validate(entity);
            result.Entity = entity;

            if (result.IsValid)
            {
                //SaveChanges会自动Commit Transaction,失败时也会自动回滚
                if (entity.IsNew())
                {
                    if (typeof(ICreatedOn).IsAssignableFrom(typeof(TEntity)))
                    {
                        ((ICreatedOn)entity).CreatedOn = DateTime.Now;
                    }
                    this.ObjectContext.CreateObjectSet<TEntity>().AddObject(entity);
                }
                this.ObjectContext.SaveChanges();
            }

            return result;
        }

        protected TResult Transact<TResult>(Func<DbTransaction, TResult> action)
        {
            var trans = this.BeginTransaction();

            try
            {
                var result = action.Invoke(trans);
                this.CommitTransaction(trans);
                return result;
            }
            catch (Exception ex)
            {
                this.RollbackTransaction(trans);
                throw ex;
            }
        }

        public NHResult<TEntity> Validate(TEntity entity)
        {
            var result = new NHResult<TEntity>();

            //var validator = ValidationFactory.CreateValidatorFromConfiguration<TEntity>(this.Helper.ValidationConfigurationSource);
            //var results = validator.Validate(entity);

            //if (!results.IsValid)
            //{
            //    var rs = from r in results select r.Message;
            //    result.Errors.AddRange(rs);
            //}

            return result;
        }
        #endregion
    }
}
