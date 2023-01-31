using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business.UrunAggregate
{
    public class Urun
    {
        public string urunAdi;
        public double alisFiyati;
        public double satisFiyati;
        public ushort stokAdedi;

        public Urun(string urunAdi, double alisFiyati, double satisFiyati, ushort stokAdedi)
        {
            //if (String.IsNullOrWhiteSpace(urunAdi))
            //    throw new ArgumentNullException("urunAdi", "parametresi boş, null yada boşluk karakteri olamaz.");
            urunAdi.NullOrWhiteSpace(nameof(urunAdi));
            alisFiyati.Zero(nameof(alisFiyati));
            satisFiyati.Zero(nameof(satisFiyati));
            stokAdedi.Zero(nameof(stokAdedi));

            this.urunAdi = urunAdi;
            this.alisFiyati = alisFiyati;
            this.satisFiyati = satisFiyati;
            this.stokAdedi = stokAdedi;
        }
    }
}

 
