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
    public class ApplicationFlight
    {
        public int Id { get; set; }

        public string FlightNumber { get; set; }

        public string DepartingAirportCode { get; set; }

        public string DestinationAirportCode { get; set; }

        public string Airline { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int Capacity { get; set; }

        public int SeatsTaken { get; set; }

        public decimal Price { get; set; }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Dictionary<string, string> dic = new Dictionary<string, string>();
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
                        textBox1.AppendText("dic.Add(\"" + airport.Key + "To" + dic.ElementAt(i).Key + "\", " + (sCoord.GetDistanceTo(eCoord)/1000)/ 900 + ");");
                        textBox1.AppendText(Environment.NewLine);
                    }
                }
            }*/
            Dictionary<string, double> dic = new Dictionary<string, double>
            {
                { "ATLToDFW", 1.30725572451624 },
                { "ATLToDEN", 2.14277369488298 },
                { "ATLToORD", 1.08555689404232 },
                { "ATLToLAX", 3.47703881172972 },
                { "ATLToCLT", 0.404457030679804 },
                { "ATLToLAS", 3.1200264269319 },
                { "ATLToPHX", 2.83505944218935 },
                { "ATLToMCO", 0.723430371758545 },
                { "ATLToMHK", 1.3946579707286 },
                { "DFWToATL", 1.30725572451624 },
                { "DFWToDEN", 1.14648906852173 },
                { "DFWToORD", 1.43537463591358 },
                { "DFWToLAX", 2.20537374697025 },
                { "DFWToCLT", 1.67189997986523 },
                { "DFWToLAS", 1.8847514660962 },
                { "DFWToPHX", 1.54937003452177 },
                { "DFWToMCO", 1.75997783848851 },
                { "DFWToMHK", 0.772957335795331 },
                { "DENToATL", 2.14277369488298 },
                { "DENToDFW", 1.14648906852173 },
                { "DENToORD", 1.58631543509905 },
                { "DENToLAX", 1.53983461791215 },
                { "DENToCLT", 2.38886976857147 },
                { "DENToLAS", 1.1216902167879 },
                { "DENToPHX", 1.07537771608774 },
                { "DENToMCO", 2.7643288235683 },
                { "DENToMHK", 0.768316122264706 },
                { "ORDToATL", 1.08555689404232 },
                { "ORDToDFW", 1.43537463591358 },
                { "ORDToDEN", 1.58631543509905 },
                { "ORDToLAX", 3.11632645413679 },
                { "ORDToCLT", 1.07268950225617 },
                { "ORDToLAS", 2.70410349949855 },
                { "ORDToPHX", 2.57213717738108 },
                { "ORDToMCO", 1.80138179698141 },
                { "ORDToMHK", 0.89468348426939 },
                { "LAXToATL", 3.47703881172972 },
                { "LAXToDFW", 2.20537374697025 },
                { "LAXToDEN", 1.53983461791215 },
                { "LAXToORD", 3.11632645413679 },
                { "LAXToCLT", 3.79518071339672 },
                { "LAXToLAS", 0.423011407621394 },
                { "LAXToPHX", 0.661567966144413 },
                { "LAXToMCO", 3.96172336722519 },
                { "LAXToMHK", 2.2469664778604 },
                { "CLTToATL", 0.404457030679804 },
                { "CLTToDFW", 1.67189997986523 },
                { "CLTToDEN", 2.38886976857147 },
                { "CLTToORD", 1.07268950225617 },
                { "CLTToLAX", 3.79518071339672 },
                { "CLTToLAS", 3.42147670118808 },
                { "CLTToPHX", 3.16710251920464 },
                { "CLTToMCO", 0.83951540869202 },
                { "CLTToMHK", 1.62150185160082 },
                { "LASToATL", 3.1200264269319 },
                { "LASToDFW", 1.8847514660962 },
                { "LASToDEN", 1.1216902167879 },
                { "LASToORD", 2.70410349949855 },
                { "LASToLAX", 0.423011407621394 },
                { "LASToCLT", 3.42147670118808 },
                { "LASToPHX", 0.457579020373236 },
                { "LASToMCO", 3.64345441439641 },
                { "LASToMHK", 1.84593454014099 },
                { "PHXToATL", 2.83505944218935 },
                { "PHXToDFW", 1.54937003452177 },
                { "PHXToDEN", 1.07537771608774 },
                { "PHXToORD", 2.57213717738108 },
                { "PHXToLAX", 0.661567966144413 },
                { "PHXToCLT", 3.16710251920464 },
                { "PHXToLAS", 0.457579020373236 },
                { "PHXToMCO", 3.3020571975155 },
                { "PHXToMHK", 1.68049203295766 },
                { "MCOToATL", 0.723430371758545 },
                { "MCOToDFW", 1.75997783848851 },
                { "MCOToDEN", 2.7643288235683 },
                { "MCOToORD", 1.80138179698141 },
                { "MCOToLAX", 3.96172336722519 },
                { "MCOToCLT", 0.83951540869202 },
                { "MCOToLAS", 3.64345441439641 },
                { "MCOToPHX", 3.3020571975155 },
                { "MCOToMHK", 2.05550742495893 },
                { "MHKToATL", 1.3946579707286 },
                { "MHKToDFW", 0.772957335795331 },
                { "MHKToDEN", 0.768316122264706 },
                { "MHKToORD", 0.89468348426939 },
                { "MHKToLAX", 2.2469664778604 },
                { "MHKToCLT", 1.62150185160082 },
                { "MHKToLAS", 1.84593454014099 },
                { "MHKToPHX", 1.68049203295766 },
                { "MHKToMCO", 2.05550742495893 }
            };
            DateTime StartDate = Convert.ToDateTime("4/20/2022");
            DateTime EndDate = Convert.ToDateTime("4/30/2022");
            string airline = "American Airlines";
            List<string> strings = new List<string>() { "6:00 AM", "7:00 AM", "8:00 AM", "9:00 AM", "10:00 AM", "11:00 AM", "12:00 PM", "1:00 PM", "2:00 PM", "3:00 PM", "4:00 PM", "5:00 PM", "6:00 PM", "7:00 PM", "8:00 PM", "9:00 PM", "10:00 PM" };
            List<DateTime> Times = strings.Select(date => DateTime.Parse(date)).ToList();
            List<string> Airports = new List<string>() { "ATL", "DFW", "DEN", "ORD", "LAX", "CLT", "LAS", "PHX", "MCO", "MHK" };
            IList<ApplicationFlight> flights = new List<ApplicationFlight>();
            foreach (DateTime day in EachDay(StartDate, EndDate))
            {
                foreach (string airport in Airports)
                {
                    for (int i = 0; i < Airports.Count; i++)
                    {
                        if (Airports[i] != airport)
                        {
                            foreach (var time in Times)
                            {
                                dic.TryGetValue(airport + "To" + Airports[i], out double flightTime);
                                flights.Add(new ApplicationFlight
                                {
                                    FlightNumber = "0000",
                                    DepartingAirportCode = airport,
                                    DestinationAirportCode = Airports[i],
                                    Airline = airline,
                                    DepartureDate = day,
                                    DepartureTime = time,
                                    ArrivalTime = time.AddHours(flightTime),
                                    Capacity = 245,
                                    SeatsTaken = 0,
                                    Price = 175
                                });
                            }
                        }
                    }
                }
            }
            textBox1.AppendText("Done");
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        //575
        static double GenTime(double dist, double speed)
        {
            Console.WriteLine(" Distance(km) : " + dist);
            Console.WriteLine(" Speed(km / hr) : " + speed);

            return dist / speed;
        }

        static double kmphTomph(double kmph)
        {
            return 0.6214 * kmph;
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
