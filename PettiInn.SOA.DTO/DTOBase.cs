using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fasterflect;
using PettiInn.Utilities;
using System.Collections;
using System.Reflection;
using PettiInn.DAL.Entities.EF5;

namespace PettiInn.SOA.DTO
{
    public class ResultInfo
    {
        private List<string> _errors = new List<string>();
        public List<string> Errors
        {
            get
            {
                return this._errors;
            }

            set
            {
                this._errors = value;
            }
        }

        private IDictionary<object, object> _extra = new Dictionary<object, object>();
        public IDictionary<object, object> Extra
        {
            get
            {
                return this._extra;
            }

            set
            {
                this._extra = value;
            }
        }


        public bool IsValid { get; set; }
    }

    public abstract class DTOBase<TEntity> where TEntity: EntityBase
    {
        private readonly IEnumerable<Query> _Query = new List<Query>();
        protected IEnumerable<Query> Query
        {
            get
            {
                return this._Query;
            }
        }

        private ResultInfo _Info = new ResultInfo();
        public ResultInfo Info
        {
            get
            {
                return this._Info;
            }

            set
            {
                this._Info = value;
            }
        }

        public DTOBase() { }

        public DTOBase(TEntity entity)
        {
            this.Copy(entity);
        }

        public DTOBase(NHResult<TEntity> result)
        {
            this.Copy(result);
        }

        public DTOBase(IEnumerable<Query> query, TEntity entity)
        {
            this._Query = query ?? new List<Query>();
            this.Copy(entity);
        }

        public int Id { get; set; }

        internal virtual void Copy(NHResult<TEntity> result)
        {
            if (result.Entity != null)
            {
                this.Copy(result.Entity);
            }
            
            this._Info = new ResultInfo
            {
                Errors = result.Errors,
                Extra = result.Extra,
                IsValid = result.IsValid
            };
        }

        internal virtual void Copy(TEntity entity)
        {
            foreach (var sourceProperty in typeof(TEntity).Properties(Flags.InstancePublic))
            {
                if (!sourceProperty.IsReadable())
                {
                    continue;
                }

                var targetProperty = this.GetType().Property(sourceProperty.Name, Flags.InstancePublic);
                if (targetProperty == null)
                {
                    //throw new ArgumentException("Property " + sourceProperty.Name + " is not present and accessible in " + typeof(TTarget).FullName);
                    continue;
                }

                var targetType = targetProperty.Type();
                var sourceType = sourceProperty.Type();
                if (!targetType.IsAssignableFrom(sourceType))
                {
                    if (this.Query != null)
                    {
                        var query = this.Query.FirstOrDefault(s => s.Name.Equals(sourceProperty.Name, StringComparison.OrdinalIgnoreCase));

                        if (query != null)
                        {
                            if (sourceType.Implements<IEnumerable>() && sourceType.IsGenericType && targetType.Implements<IEnumerable>() && targetType.IsGenericType)
                            {
                                var listSourceType = sourceType.GetGenericArguments()[0];
                                var allDTOTypes = Assembly.GetExecutingAssembly().Types();
                                var targetListType = allDTOTypes.FirstOrDefault(t => t.Name.Equals(string.Concat(listSourceType.Name, "DTO"), StringComparison.OrdinalIgnoreCase));

                                if (targetListType != null)
                                {
                                    var v = (IEnumerable)sourceProperty.Get(entity);

                                    foreach (var item in v)
                                    {
                                        var targetListItem = targetListType.TryCreateInstance(new Dictionary<string, object>
                                        {
                                            { "query", query.Queries },
                                            { "entity", item }
                                        });

                                        this.GetPropertyValue(targetProperty.Name).TryCallMethodWithValues("Add", targetListItem);
                                    }
                                }
                            }
                            else if (targetType.Name.Equals(string.Concat(sourceType.Name, "DTO"), StringComparison.OrdinalIgnoreCase))
                            {
                                var v = sourceProperty.Get(entity);

                                if (v != null)
                                {
                                    var targetInstance = targetType.TryCreateInstance(new Dictionary<string, object>
                                {
                                    { "query", query.Queries },
                                    { "entity", v }
                                });

                                    this.TrySetValue(targetProperty.Name, targetInstance);
                                }
                            }
                        }
                    }

                    continue;
                }
                if (!targetProperty.IsWritable())
                {
                    throw new ArgumentException("Property " + sourceProperty.Name + " is not writable in " + typeof(TEntity).FullName);
                }

                var value = sourceProperty.Get(entity);
                this.TrySetValue(targetProperty.Name, value);
            }
        }
    }
}
