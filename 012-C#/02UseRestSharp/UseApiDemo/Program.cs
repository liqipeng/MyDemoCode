using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UseApiDemo
{
    class Program
    {
        private static string publicKey = ConfigurationManager.AppSettings["PublicKey"];
        private static string privateKey = ConfigurationManager.AppSettings["PrivateKey"];

        static void Main(string[] args)
        {
            Console.WriteLine("请输入密码：");
            string pwd = Console.ReadLine();
            privateKey = Encrypter.DecryptByAES(privateKey, GetMd5(pwd));
            GetBalance();
            //GetTicker();

            Console.ReadKey();
        }

        private static void GetBalance()
        {
            var client = new RestClient("http://www.jubi.com/");
            var request = new RestRequest("api/v1/balance/", Method.POST);
            request.AddParameter("nonce", "6");
            request.AddParameter("key", publicKey);
            request.AddParameter("signature", GetHMACSHA256(GetParametersString(request.Parameters), GetMd5(privateKey)));
            Console.WriteLine(GetParametersString(request.Parameters));
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            Console.WriteLine(content);
        }

        private static void GetTicker()
        {
            var client = new RestClient("http://www.jubi.com/");
            var request = new RestRequest("api/v1/ticker", Method.GET);
            request.AddParameter("nonce", "3");
            request.AddParameter("key", publicKey);
            request.AddParameter("coin", "vtc");
            request.AddParameter("signature", GetHMACSHA256(GetParametersString(request.Parameters), GetMd5(privateKey)));
            Console.WriteLine(GetParametersString(request.Parameters));
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            Console.WriteLine(content);
        }

        private static string GetParametersString(List<Parameter> lstParams) 
        {
            StringBuilder sBuiler = new StringBuilder();
            lstParams.ForEach(p => {
                sBuiler.AppendFormat("{0}={1}&", p.Name, p.Value);
            });

            return sBuiler.ToString().Trim('&');
        }

        static string GetHMACSHA256(string input, string key)
        {
            using (HMACSHA256 hmac = new HMACSHA256())
            {
                hmac.Key = Encoding.UTF8.GetBytes(key);

                byte[] data = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        static string GetMd5(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
    }
}
