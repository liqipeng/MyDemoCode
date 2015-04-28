using ASPNETPatterns.QueryObject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.QueryObject.Repository
{
    public static class NamedQueryFactory
    {
        public static Query CreateRetrieveOrdersUsingAComplexQuery(Guid CustomerId) 
        {
            IList<Criterion> criteria = new List<Criterion>();
            criteria.Add(new Criterion("CustomerId", CustomerId, CriteriaOperator.NotApplicable));
            Query query = new Query(QueryName.RetrieveOrdersUsingAComplexQuery, criteria);

            return query;
        }
    }
}
