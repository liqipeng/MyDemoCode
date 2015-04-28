using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.QueryObject.Infrastructure
{
    public class Query
    {
        private QueryName _queryName;
        private IList<Criterion> _criteria;

        public Query():this(QueryName.Dynamic, new List<Criterion>())
        {

        }

        public Query(QueryName queryName, IList<Criterion> criteria)
        {
            this._queryName = queryName;
            this._criteria = criteria;
        }

        public QueryName Name 
        {
            get 
            {
                return _queryName;
            }
        }

        public bool IsNameQuery() 
        {
            return Name != QueryName.Dynamic;
        }

        public IEnumerable<Criterion> Criteria 
        {
            get 
            {
                return _criteria;
            }
        }

        public void Add(Criterion criterion) 
        {
            if (!IsNameQuery())
            {

            }
            else 
            {
                throw new ApplicationException("You cannot add additional criteria to named queries");
            }
        }

        public QueryOperator QueryOperator
        {
            get;
            set;
        }

        public OrderByClause OrderByProperty
        {
            get;
            set;
        }
    }
}
