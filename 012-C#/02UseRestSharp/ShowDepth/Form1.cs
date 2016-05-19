using Newtonsoft.Json;
using RestSharp;
using RestSharp.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ShowDepth
{
    public partial class Form1 : Form
    {
        //private static readonly string publicKeyEncrypt = ConfigurationManager.AppSettings["PublicKey"];
        //private static readonly string privateKeyEncrypt = ConfigurationManager.AppSettings["PrivateKey"];

        public Form1()
        {
            InitializeComponent();
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            string[] arr = this.txtCoinType.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            int count = 0;
            for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
            {
                for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                {
                    Chart chart = this.tableLayoutPanel1.GetControlFromPosition(j, i) as Chart;
                    if (chart != null)
                    {
                        chart.Series.Clear();

                        if (count <= arr.Length - 1)
                        {
                            string coin = arr[count];
                            try
                            {
                                var dictData = GetDepth(coin, null, null);
                                chart.Titles.Clear();
                                chart.Titles.Add(coin);
                                chart.Series.Clear();
                                foreach (var key in dictData.Keys)
                                {
                                    chart.Series.Add(key);
                                    chart.Series[key].ChartType = SeriesChartType.Area;
                                    chart.Series[key].ToolTip = "valx:#VALX\nvaly:#VALY\navg:#AVG\nmax#MAX";
                                    chart.Series[key].Points.DataBind(dictData[key], "X", "Y", null);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            count++;
                        }
                    }
                }
            }

            //string pubKey = EncryptUtil.DecryptByAES(publicKeyEncrypt, this.txtPwd.Text);
            //string priKey = EncryptUtil.DecryptByAES(privateKeyEncrypt, this.txtPwd.Text);
            //string coin = this.txtCoinType.Text.Trim();

            //GetDepth(coin, pubKey, priKey);

            //try
            //{
            //    //ChartArea chartArea = new ChartArea("Default");
            //    //chartArea.AxisX.Interval = 0.01;
            //    //chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            //    //chartArea.AxisX.IntervalOffset = 0.01;
            //    //this.chart1.ChartAreas.Add(chartArea);

            //    var dictData = GetDepth(coin, null, null);
            //    this.chart1.Titles.Add(this.txtCoinType.Text);
            //    this.chart1.Series.Clear();
            //    foreach (var key in dictData.Keys)
            //    {
            //        this.chart1.Series.Add(key);
            //        this.chart1.Series[key].ChartType = SeriesChartType.Area;
            //        this.chart1.Series[key].ToolTip = "valx:#VALX\nvaly:#VALY\navg:#AVG\nmax#MAX";
            //        this.chart1.Series[key].Points.DataBind(dictData[key], "X", "Y", null);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private static Dictionary<string, IEnumerable<Data>> GetDepth(string coin, string pubKey, string privateKey)
        {
            var client = new RestClient("http://www.jubi.com/");
            var request = new RestSharp.Newtonsoft.Json.RestRequest("api/v1/depth/", Method.GET);
            request.JsonSerializer = new NewtonsoftJsonSerializer();
            request.AddParameter("coin", coin);
            //request.AddParameter("key", pubKey);
            //request.AddParameter("signature", EncryptUtil.GetHMACSHA256(GetParametersString(request.Parameters), EncryptUtil.GetMd5(privateKey)));
            Console.WriteLine(GetParametersString(request.Parameters));

            IRestResponse<DepthData> response = client.Execute<DepthData>(request);
            var content = response.Content;
            //var dept = response.Data; //反序列化失败！
            var dept = JsonConvert.DeserializeObject<DepthData>(content);

            Dictionary<string, IEnumerable<Data>> dict = new Dictionary<string, IEnumerable<Data>>();
            dict["卖-asks"] = dept.asks.Select(arr => new Data() { X = arr[0], Y = arr[1] });
            dict["买-bids"] = dept.bids.Select(arr => new Data() { X = arr[0], Y = arr[1] });

            return dict;
        }

        private static string GetParametersString(List<Parameter> lstParams)
        {
            StringBuilder sBuiler = new StringBuilder();
            lstParams.ForEach(p =>
            {
                sBuiler.AppendFormat("{0}={1}&", p.Name, p.Value);
            });

            return sBuiler.ToString().Trim('&');
        }
    }
}
