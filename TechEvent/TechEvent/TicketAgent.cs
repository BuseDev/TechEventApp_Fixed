using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechEvent
{
    internal class TicketAgent : BiletFirmalari
    {
        public override void EtkinlikleriJSONOlarakAlir()
        {
            Console.WriteLine("TicketAgent firması dataları JSON olarak almamaktadır");
        }

        public override void EtkinlikleriXMLOlarakAlir()
        {
            string data = TechEventHelper.XMLDataOlustur();
            this.DataCekildiMi = true;
        }
    }
}
