using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CdrNet.Data.Txt
{
    public class DosyaBulunamadıException: Exception 
    {
        public DosyaBulunamadıException(string dosyaYolu)
            : base($"{dosyaYolu}yolundaki dosya okunamadı.")
        {
        }
    }
}
