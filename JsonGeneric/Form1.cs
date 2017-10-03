using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonGeneric
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;

            Organisation org = new Organisation() { Name="rietveld"};
            Tycoon tyc = new Tycoon() { Name = "bigboss" };

            org.Owner = tyc;
            org.o = new parsec() { Name = "ppparsec" };
          
           // var jj = AltiJsonSerializer.SerializeJson(org);
            var jj = JsonConvert.SerializeObject(org);
            Console.WriteLine(jj);
            // var jj2 = JsonConvert.SerializeObject(org, Formatting.Indented, settings);
            // Console.WriteLine(jj2);

            /// var dd = AltiJsonSerializer.DeserializeJson<Organisation>(jj);
            var dd = JsonConvert.DeserializeObject<Organisation>(jj);
            if (dd.o !=null)
            {
                Console.WriteLine("is an parsec!");
                }
            if (dd.Owner != null)
            {
                Console.WriteLine("is an tycoon!");
            }
            Console.WriteLine(dd.o);
        }
    }
}
