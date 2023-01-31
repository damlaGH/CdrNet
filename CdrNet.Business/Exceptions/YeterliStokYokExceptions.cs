using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Business.Exceptions
{
    public class YeterliStokYokException : Exception
    {
        public YeterliStokYokException(string urunAdi, ushort stok, ushort dusulecekUrunAdedi)
            : base($"{urunAdi} isimli urunden stokta {stok} adet var.{dusulecekUrunAdedi} kadar mevcut değil.")
        {

        }
    }
}
