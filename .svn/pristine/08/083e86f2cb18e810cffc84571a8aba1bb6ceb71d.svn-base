using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PettiInn.SOA.DTO
{
    public class Query
    {
        public string Name { get; set; }

        private IEnumerable<Query> _Queries = new List<Query>();

        public IEnumerable<Query> Queries
        {
            get { return _Queries; }
            set { _Queries = value; }
        }
    }
}
