using System.Text;

namespace CdrNet.Data.Txt
{
    public class DosyaIslemleri
    {
        public static void Kaydet(string dosyaYolu, string icerik)
            {
            File.WriteAllText(dosyaYolu, icerik);
            }
        public static string Oku(string dosyaYolu)
        {
            try
            {
               return File.ReadAllText(dosyaYolu);
            }

            catch (Exception ex) 
            {
                StringBuilder sb=new StringBuilder();
                sb.Append("******************");
                sb.Append($"Log Seviyesi: Error \r\n");
                sb.Append($"Log Zamanı: {DateTime.Now} \r\n");
                sb.Append($"Hata Mesajı: {ex.Message}\r\n");
                sb.Append($"Dosya Yolu: {dosyaYolu} \r\n");
                sb.Append("******************");

                File.AppendAllText("log.txt", sb.ToString());
                throw new DosyaBulunamadıException(dosyaYolu);
            }
        }
    }
}