using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechEvent
{
    public abstract class BiletFirmalari
    {
        public string FirmaAdi { get; set; }
        public string WebSitesi { get; set; }
        public bool DataCekildiMi { get; set; }

        public abstract void EtkinlikleriXMLOlarakAlir();
        public abstract void EtkinlikleriJSONOlarakAlir();
    }
}
