using CdrNet.Data.Txt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CdrNet.Infrastructer.Logging;

namespace CdrNet.Business.KasaAggregate
{
    internal class KasaServisi
    {
        private static List<Kasa> liste = new List<Kasa>();
        static KasaServisi()
        {
            KasaYukle();
        }
        private static void KasaYukle()
        {
            string data = "[]";


            try
            {
                data = DosyaIslemleri.Oku(Sabitler.kasa_dosya_yolu);
                liste = JsonSerializer.Deserialize<List<Kasa>>(data, new JsonSerializerOptions { IncludeFields = true });
            }
            catch (DosyaBulunamadıException)
            {
                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { IncludeFields = true });
                DosyaIslemleri.Kaydet(Sabitler.kasa_dosya_yolu, json);
            }
            catch (Exception ex)
            {
                LogServis.Hata(ex.Message);
            }

        }
        public GenelDonusTipi Kaydet(IslemTipi tip, double tutar, string aciklama)
        {
            try
            {
                    KasaYukle();
                    Kasa k = new Kasa(tip, DateTime.Now, aciklama, tutar);
                    liste.Add(k);
                    string json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { IncludeFields = true });
                    DosyaIslemleri.Kaydet(Sabitler.kasa_dosya_yolu, json);
                    return new GenelDonusTipi(false);
            }

            catch (Exception ex)
            {
                LogServis.Hata(ex.Message);
                return new GenelDonusTipi(true, "Kasa işlemi kayıt edilirken bir hata oluştu!\n" + ex.Message);
            }
        }
        public IReadOnlyCollection<Kasa> KasaListesi() => liste.AsReadOnly();
        public IReadOnlyCollection<Kasa> GelirListesi() => liste.Where(k => k._islemTipi == IslemTipi.Gelir).ToList().AsReadOnly();
        public IReadOnlyCollection<Kasa> GiderListesi() => liste.Where(k => k._islemTipi == IslemTipi.Gider).ToList().AsReadOnly();
        public double Bakiye() => GelirListesi().Sum(g => g._tutar) - GiderListesi().Sum(g => g._tutar);
    }
 }

 
       

