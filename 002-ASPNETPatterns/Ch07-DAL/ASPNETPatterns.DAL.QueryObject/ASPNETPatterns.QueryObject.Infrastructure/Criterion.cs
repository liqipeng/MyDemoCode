using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.QueryObject.Infrastructure
{
    public class Criterion
    {
        private string _propertyName;
        private object _value;
        private CriteriaOperator _criteriaOperator;

        public Criterion(string propertyName, object value, CriteriaOperator criteriaOperator)
        {
            _propertyName = propertyName;
            _value = value;
            _criteriaOperator = criteriaOperator;
        }

        public string PropertyName 
        {
            get
            {
                return _propertyName;
            }
        }

        public object Value 
        {
            get 
            {
                return _value;
            }
        }

        public CriteriaOperator CriteriaOperator 
        {
            get
            {
                return _criteriaOperator;
            }
        }
    }
}
