using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechEvent
{
    internal class BiletEvi : BiletFirmalari
    {
        public override void EtkinlikleriJSONOlarakAlir()
        {
            string data = TechEventHelper.JSONDataGonder();
            this.DataCekildiMi = true;
        }

        public override void EtkinlikleriXMLOlarakAlir()
        {
            string data = TechEventHelper.XMLDataOlustur();
            this.DataCekildiMi = true;
        }
    }
}
