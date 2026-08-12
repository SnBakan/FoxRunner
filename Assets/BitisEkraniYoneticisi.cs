using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class BitisEkraniYoneticisi : MonoBehaviour
{
    public TMP_InputField isimInput;     // Müfettiþten sürükle
    public TextMeshProUGUI tabloYazisi;  // Müfettiþten sürükle (Liderlik listesi burada görünecek)

    void OnEnable() {
    // Panel her aktif olduðunda listeyi tazele
    ListeyiGuncelle();
    
    // Input alanýný her seferinde temiz ve yazýlabilir yap
    isimInput.text = "";
    isimInput.interactable = true;
}
    public AudioSource basariSesKaynagi; // Bunu public yaptýðýndan emin ol

    public void KaydetVeListele()
    {
        string oyuncuAdi = isimInput.text;
        int toplamPuan = Oyun_Yonetici.Instance.puan;

        if (!string.IsNullOrEmpty(oyuncuAdi))
        {
            // Tek seferde hem kaydediyoruz hem de sonucunu alýyoruz
            bool basarili = SkorSistemi.SkorKaydet(oyuncuAdi, toplamPuan);
            ListeyiGuncelle();

            isimInput.interactable = false;

            // Baþarýlýysa sesi buradan çalabilirsin
            if (basarili && basariSesKaynagi != null)
            {
                basariSesKaynagi.Play();
            }
        }
    }
    void ListeyiGuncelle()
    {
        // 1. Skorlarý sistemden çek
        List<SkorKaydi> skorlar = SkorSistemi.SkorlariGetir();

        // 2. Baþlýðý hazýrla
        //string sonuc = "--- EN ÝYÝ 5 SKOR ---\n";
        string sonuc = "";
        // 3. Liste dolu mu kontrol et
        if (skorlar != null && skorlar.Count > 0)
        {
            foreach (var kayit in skorlar)
            {
                // Her bir kaydý alt alta ekle
                sonuc += kayit.isim + ": " + kayit.puan + "\n";
            }
        }
        else
        {
            // Eðer hala boþ geliyorsa bunu yazdýr (Hata takibi için)
            sonuc += "Liste okunurken bir sorun oluþtu.";
        }

        // 4. Ekrana yazdýr
        tabloYazisi.text = sonuc;
    }
}