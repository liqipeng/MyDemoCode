using ASPNETPatterns.QueryObject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.QueryObject.Repository
{
    public static class OrderQueryTranslator
    {
        private static string baseSelectQuery = "SELECT * FROM [Order]";
        private static Dictionary<string, string> _dictFields = new Dictionary<string, string>() 
        {
            { "Id", "[Id]" },
            { "OrderDate", "[OrderDate]" },
            { "CustomId", "[CustomId]" }
        };

        public static void TranslateInfo(this Query query, SqlCommand command)
        {
            if (query.IsNameQuery())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = query.Name.ToString();

                foreach (Criterion criterion in query.Criteria)
                {
                    command.Parameters.Add(new SqlParameter("@" + criterion.PropertyName, criterion.PropertyName));
                }
            }
            else
            {
                StringBuilder sqlQuery = new StringBuilder(baseSelectQuery);
                bool isNotFirstFilterClause = false;

                // where
                if (query.Criteria.Count() > 0)
                {
                    sqlQuery.Append(" WHERE ");
                }

                foreach (Criterion criterion in query.Criteria)
                {
                    if (isNotFirstFilterClause)
                    {
                        sqlQuery.Append(GetQueryOperator(query));
                    }

                    sqlQuery.Append(AddFilterClauseFrom(criterion));

                    command.Parameters.Add(new SqlParameter("@" + criterion.PropertyName, criterion.Value));

                    isNotFirstFilterClause = true;
                }

                // order by
                sqlQuery.Append(GenerateOrderByClauseFrom(query.OrderByProperty));

                command.CommandType = CommandType.Text;
                command.CommandText = sqlQuery.ToString();
            }
        }

        private static string GenerateOrderByClauseFrom(OrderByClause orderByClause)
        {
            string orderByClip = string.Format(" ORDER BY {0} {1}", FindTableColumnFor(orderByClause.PropertyName), GetSelectOrder(orderByClause.Desc));

            return orderByClip;
        }

        private static string FindTableColumnFor(string propertyName)
        {
            if (!_dictFields.ContainsKey(propertyName))
            {
                throw new ApplicationException("Property is not exists: " + propertyName);
            }

            return _dictFields[propertyName];
        }

        private static string AddFilterClauseFrom(Criterion criterion)
        {
            string clip = string.Format("{0} {1} @{2}", FindTableColumnFor(criterion.PropertyName), FindSQLOperatorFor(criterion.CriteriaOperator));

            return clip;
        }

        private static string FindSQLOperatorFor(CriteriaOperator criteriaOperator)
        {
            switch (criteriaOperator)
            {
                case CriteriaOperator.Equal:
                    return "=";
                case CriteriaOperator.LessThanOrEqual:
                    return "<=";
                default:
                    throw new ApplicationException("No operator defined.");
            }
        }

        private static string GetQueryOperator(Query query)
        {
            if (query.QueryOperator == QueryOperator.And)
            {
                return "AND";
            }
            else
            {
                return "OR";
            }
        }

        private static string GetSelectOrder(bool desc)
        {
            return desc ? "DESC" : "ASC";
        }
    }
}
