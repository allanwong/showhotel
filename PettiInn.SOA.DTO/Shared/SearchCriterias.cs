using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PettiInn.SOA.DTO.Shared
{
    public class SearchCriterias
    {
        public class Sort
        {
            public string dir { get; set; }

            public string sort { get; set; }
        }

        public int sEcho { get; set; }

        /// <summary>
        /// 页码
        /// 服务端需要计算结果集页数时必须指定
        /// </summary>
        public int? pageNumber { get; set; }

        /// <summary>
        /// 从第几条记录开始
        /// 客户端需要计算结果集页数时必须指定
        /// </summary>
        public int? iDisplayStart { get; set; }

        private int _iDisplayLength = 10;

        /// <summary>
        /// 每页行数
        /// </summary>
        public int iDisplayLength
        {
            get { return _iDisplayLength; }
            set { _iDisplayLength = value; }
        }

        private IEnumerable<Sort> _sortings = new List<Sort>();

        public IEnumerable<Sort> sortings
        {
            get { return _sortings; }
            set { _sortings = value; }
        }
    }
}
