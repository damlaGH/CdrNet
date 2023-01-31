using CdrNet.Business.Exceptions;
using CdrNet.Data.Txt;
using CdrNet.Infrastructer.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CdrNet.Business.UrunAggregate
{
    internal class UrunServisi
    {
        private static List<Urun> liste = new List<Urun>();
        static UrunServisi()
        {
            UrunLeriYukle();
        }

        private static void UrunLeriYukle()
        {
            string data = "";
            try
            {
                data = DosyaIslemleri.Oku(Sabitler.urun_dosya_yolu);
                liste = JsonSerializer.Deserialize<List<Urun>>(data, new JsonSerializerOptions { IncludeFields = true });
            }
            catch (Exception ex)
            {
                LogServis.Uyari(ex.Message);
            }
        }
        public GenelDonusTipi Kaydet(string urunAdi, double alisFiyati, double satisFiyati, ushort stok)
        {
            try
            {
                var urunVarmiDonusTipi = UrunVarMi(urunAdi);
                if (urunVarmiDonusTipi.hataVarMi)
                    return urunVarmiDonusTipi;

                bool urunVarmi = (bool)urunVarmiDonusTipi.data;
                if (urunVarmi)
                    throw new UrunZatenKayitliException(urunAdi);


                var urun = new Urun(urunAdi, alisFiyati, satisFiyati, stok);
                liste.Add(urun);

                string json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { IncludeFields = true });
                DosyaIslemleri.Kaydet(Sabitler.urun_dosya_yolu, json);

                return new GenelDonusTipi(false, urun);

            }
            catch (Exception ex)
            {
                LogServis.Hata(ex.Message);
                return new GenelDonusTipi(true, ex.Message);
            }
        }
        public IReadOnlyCollection<Urun> UrunListesi()
        {
            UrunLeriYukle();
            return liste.AsReadOnly();
        }
        public GenelDonusTipi StokDusur(string urunAdi, ushort dusulecekUrunAdedi)
        {
            try
            {
                dusulecekUrunAdedi.Zero(nameof(dusulecekUrunAdedi));
            }
            catch (Exception ex)
            {
                return new GenelDonusTipi(true, "0 adet urun satışı yapılamaz.");
            }

            var aramaSonucu = UrunAra(urunAdi);

            if (aramaSonucu.hataVarMi)
                return aramaSonucu;

            Urun urun = (Urun)aramaSonucu.data;

            try
            {
                if (urun.stokAdedi < dusulecekUrunAdedi)
                    throw new YeterliStokYokException(urunAdi, urun.stokAdedi, dusulecekUrunAdedi);


                liste.Remove(urun);
                urun.stokAdedi -= dusulecekUrunAdedi;
                return Kaydet(urun.urunAdi, urun.alisFiyati, urun.satisFiyati, urun.stokAdedi);
            }
            catch (Exception ex)
            {
                LogServis.Uyari(ex.Message);
                return new GenelDonusTipi(true, ex.Message);
            }

        }
        public GenelDonusTipi UrunAra(string urunAdi)
        {
            try
            {
                urunAdi.NullOrWhiteSpace(nameof(urunAdi));

                var donusTipi = new GenelDonusTipi(false);
                donusTipi.data = liste.FirstOrDefault(u => u.urunAdi == urunAdi);

                if (donusTipi.data == null)
                    throw new UrunBulunamadiException(urunAdi);

                return donusTipi;
            }
            catch (Exception ex)
            {
                LogServis.Hata(ex.Message);
                return new GenelDonusTipi(true, ex.Message);
            }
        }
        private GenelDonusTipi UrunVarMi(string urunAdi)
        {
            try
            {
                urunAdi.NullOrWhiteSpace(nameof(urunAdi));

                var donusTipi = new GenelDonusTipi(false);
                donusTipi.data = liste.Any(u => u.urunAdi == urunAdi);
                return donusTipi;

            }
            catch (Exception ex)
            {
                LogServis.Hata(ex.Message);
                return new GenelDonusTipi(true, ex.Message);
            }
        }
    }
}

  
