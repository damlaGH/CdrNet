using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CdrNet.Infrastructer.Logging
{
       public class LogServis
        {
            private static List<Log> liste = new List<Log>();
            private const string LOG_DOSYA_YOLU = "log.txt";    //Businessdaki sabitlere kaydedemeyiz çünkü business logdan ref alıyor, çarpraz ref alınmaz.
            private static IReadOnlyCollection<Log> LoglariListele()
            {
                try
                {
                    string json = File.ReadAllText(LOG_DOSYA_YOLU);
                    liste = JsonSerializer.Deserialize<List<Log>>(json, new JsonSerializerOptions() { IncludeFields = true });
                }
                catch (Exception)
                {

                }

                return liste.AsReadOnly();
            }
            private static void DosyayaKaydet()
            {
                string json = JsonSerializer.Serialize(liste, new JsonSerializerOptions { IncludeFields = true });
                File.WriteAllText(LOG_DOSYA_YOLU, json);
            }
            public static void Hata(string logMesaji)
            {
                LoglariListele();
                Log log = new Log(logMesaji, LogTipi.Hata);
                liste.Add(log);
                DosyayaKaydet();
            }
            public static void Uyari(string logMesaji)
            {
                LoglariListele();
                Log log = new Log(logMesaji, LogTipi.Uyari);
                liste.Add(log);
                DosyayaKaydet();

            }
            public static void Bilgilendirme(string logMesaji)
            {
                LoglariListele();
                Log log = new Log(logMesaji, LogTipi.Bilgilendirme);
                liste.Add(log);
                DosyayaKaydet();

            }
        }
    }



