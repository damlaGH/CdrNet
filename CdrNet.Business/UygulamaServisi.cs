using CdrNet.Business.KasaAggregate;
using CdrNet.Business.UrunAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
  
namespace CdrNet.Business
{
    public class UygulamaServisi
    {
        private KasaServisi kasaServisi= new KasaServisi();
        private UrunServisi urunServisi = new UrunServisi();
        public GenelDonusTipi UrunEkle(string urunAdi, double alisFiyati, double satisFiyati, ushort stok)
        {
            var urunKayitSonuc = urunServisi.Kaydet(urunAdi, alisFiyati, satisFiyati, stok);
            if (urunKayitSonuc.hataVarMi)
                return urunKayitSonuc;
            var kasaKayitSonuc = kasaServisi.Kaydet(IslemTipi.Gider, (alisFiyati * stok),$"{urunAdi} isimli üründen {stok} adet alındı.");
            if (kasaKayitSonuc.hataVarMi)
                return kasaKayitSonuc;

            return new GenelDonusTipi(false);
        }
        public GenelDonusTipi SatisYap(string urunAdi, ushort satisAdedi)
        {
            var stokDusurSonuc = urunServisi.StokDusur(urunAdi, satisAdedi);
            if (stokDusurSonuc.hataVarMi)
                return stokDusurSonuc;

            var urun = (Urun)stokDusurSonuc.data;
            var kasaKayitSonuc = kasaServisi.Kaydet(IslemTipi.Gelir, (urun.satisFiyati * satisAdedi),
                $"{urun.urunAdi} isimli üründen {urun.satisFiyati} liradan {satisAdedi} adet satıldı.");

            if (kasaKayitSonuc.hataVarMi)
                return kasaKayitSonuc;

            return new GenelDonusTipi(false);
        }

        public double KasaBakiyesi() => kasaServisi.Bakiye();
        public IReadOnlyCollection<Kasa> KasaListesi() => kasaServisi.KasaListesi();
    }
}


      