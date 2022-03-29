using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace datageneratorGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ATL", "33.640411,-84.419853");
            dic.Add("DFW", "32.897480,-97.040443");
            dic.Add("DEN", "39.849312,-104.673828");
            dic.Add("ORD", "41.978611,-87.904724");
            dic.Add("LAX", "33.942791,-118.410042");
            dic.Add("CLT", "35.213890,-80.943054");
            dic.Add("LAS", "36.086010,-115.153969");
            dic.Add("PHX", "33.437269,-112.007788");
            dic.Add("MCO", "28.431881,-81.308304");
            dic.Add("MHK", "39.141222,-96.671806");
            Random r = new Random();
            foreach (KeyValuePair<string, string> airport in dic)
            {
                int rInt = r.Next(100, 999);
                for (int i = 0; i < dic.Count; i++)
                {
                    if (dic.ElementAt(i).Key != airport.Key)
                    {
                        string[] sLatLong = airport.Value.Split(',');
                        string[] eLatLong = dic.ElementAt(i).Value.Split(',');
                        var sCoord = new GeoCoordinate(Convert.ToDouble(sLatLong[0]), Convert.ToDouble(sLatLong[1]));
                        var eCoord = new GeoCoordinate(Convert.ToDouble(eLatLong[0]), Convert.ToDouble(eLatLong[1]));
                        textBox1.AppendText("('" + RandomString(3) + rInt + "','" + airport.Key + "','" + dic.ElementAt(i).Key + "'," + Math.Round(sCoord.GetDistanceTo(eCoord) / 1000, 2) + "),");
                        textBox1.AppendText(Environment.NewLine);
                    }
                }
            }
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
