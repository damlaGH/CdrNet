
using CdrNet.Business;

Menu();
static void Menu()
{

    Console.Clear();
    var cevap = CevapAl("1.Ürün Ekle\n2.Ürün Sat\n3.Kasa Durumu\n4.Çıkış", false);
    switch (cevap)
    {
        case "1":
            UrunEkle();
            break;
        case "2":
            UrunSat();
            break;
        case "3":
            KasaDurumu();
            break;
        case "4":
            Environment.Exit(0);
            break;
        default:
            MenuyeDonus();
            break;
    }
}


static string CevapAl(string ekranMetni, bool ayniSatirdaMi=true)
{
    if(ayniSatirdaMi)
        Console.Write(ekranMetni);
    else
        Console.WriteLine(ekranMetni);
    return Console.ReadLine();
}

static void UrunEkle()
{
    Console.Clear();
    string urunAdi = CevapAl("Ürün Adı:");
    double alisFiyati = Convert.ToDouble(CevapAl("Alış Fiyatı:"));
    double satisFiyati = Convert.ToDouble(CevapAl("Satış Fiyatı : "));
    ushort stok = Convert.ToUInt16(CevapAl("Stok Adedi : "));

    UygulamaServisi servis = new UygulamaServisi();
    GenelDonusTipi sonuc = servis.UrunEkle(urunAdi, alisFiyati, satisFiyati, stok);

    if (sonuc.hataVarMi)
    {
        Console.WriteLine(sonuc.hataMesaji);
        TekrarDeneme();
        UrunEkle();
        return;
    }
    MenuyeDonus();
}


static void UrunSat()
{
    UygulamaServisi servis = new UygulamaServisi();
    string urunAdi = CevapAl("Ürün adı : ");
    ushort adet = Convert.ToUInt16(CevapAl("Adet : "));
    GenelDonusTipi sonuc = servis.SatisYap(urunAdi, adet);

    if (sonuc.hataVarMi)
    {
        Console.WriteLine(sonuc.hataMesaji);
        TekrarDeneme();
        UrunSat();
        return;
    }
    MenuyeDonus();
}

static void KasaDurumu()
{
    UygulamaServisi servis = new UygulamaServisi();
    var bakiye = servis.KasaBakiyesi();
    var liste = servis.KasaListesi();
    Console.WriteLine("Tarih \t\t\tTutar\t\tAçıklama");
    foreach(var k in liste)
        Console.WriteLine($"{ k._tarih.ToShortDateString()}\t\t{k._tutar}\t\t{k._aciklama}");
    Console.WriteLine("Güncel Kasa Bakiyesi:"+bakiye);
    MenuyeDonus();
}
static void MenuyeDonus()
{
    Console.WriteLine("Menuye dönmek için ENTER:");
    Console.ReadLine();
    Menu();
}

static void TekrarDeneme()
{
    Console.WriteLine("Tekrar denemek için ENTER");
    Console.ReadLine();
}

