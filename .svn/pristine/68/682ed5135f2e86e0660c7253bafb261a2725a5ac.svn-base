using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PettiInn.SOA.DTO.Shared
{
    public class TreeNodeModel<TNodeEntity> : ITreeNode
    {
        public string text { get; set; }

        public string iconCls { get; set; }

        public bool leaf { get; set; }

        public int id { get; set; }

        public int sortIndex { get; set; }

        public int? ParentId { get; set; }

        public virtual bool expanded
        {
            get
            {
                return true;
            }
        }

        private IEnumerable<TNodeEntity> _children = new List<TNodeEntity>();

        public IEnumerable<TNodeEntity> children
        {
            get { return _children; }
            set { _children = value; }
        }
    }
}
