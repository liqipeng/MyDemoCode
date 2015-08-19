using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientAndHostingInOne.Interface
{
    [ServiceContract(Name = "CalculatorService", Namespace="http://www.aaa.com")]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double x, double y);
        [OperationContract]
        double Substract(double x, double y);
        [OperationContract]
        double Multiply(double x, double y);
        [OperationContract]
        double Divide(double x, double y);
    }
}
