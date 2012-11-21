using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PettiInn.DAL.Entities.EF5
{
    public class NHResult<TEntity>
    {
        public TEntity Entity { get; set; }

        private List<string> _errors = new List<string>();
        public List<string> Errors
        {
            get
            {
                return this._errors;
            }

            internal set
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

            internal set
            {
                this._extra = value;
            }
        }


        public bool IsValid
        {
            get { return this.Errors.Count == 0; }
        }

        public NHResult<TEntity> MergeErrors<TAnotherEntity>(NHResult<TAnotherEntity> another)
        {
            this.Errors.AddRange(another.Errors);

            return this;
        }
    }
}
