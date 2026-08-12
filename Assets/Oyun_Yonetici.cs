using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Oyun_Yonetici : MonoBehaviour
{
    public static Oyun_Yonetici Instance;

    public int puan = 0;
    public int can = 3;
    public float kalanSure = 120f;
    private bool oyunDevamEdiyor = true;

    public TextMeshProUGUI puanYazisi;
    public TextMeshProUGUI sureYazisi;
    public TextMeshProUGUI canYazisi; // Müfettiþten sürüklemeyi unutma!
    public GameObject[] kalpGorselleri;

    [Header("Bitiþ Ekraný Elemanlarý")]
    public GameObject bitisPaneli;
    public TextMeshProUGUI sonSkorYazisi;
    void Awake() { Instance = this; }

    void Update()
    {
        // Eðer oyun bittiyse süre saymayý durdur
        if (!oyunDevamEdiyor) return;

        if (kalanSure > 0)
        {
            kalanSure -= Time.deltaTime;
            // Süreyi ekrana yazdýr ve eksiye düþmesini engelle
            sureYazisi.text = "Süre: " + Mathf.Max(0, Mathf.FloorToInt(kalanSure)).ToString();
        }
        else
        {
            kalanSure = 0;
            OyunBitti(); // Süre tam 0 olduðunda bu fonksiyon çalýþýr ve paneli açar
        }
    }

    //public void PuanEkle(int miktar)
    //{
    //    if (!oyunDevamEdiyor) return;
    //    puan += miktar;
    //    puanYazisi.text = "Puan: " + puan;
    //    // 2. --- VFX KURULUMU ---
    //    // Eðer inspector'da efekti sürüklediysek
    //    if (meyveVFX != null)
    //    {
    //        // Efekti meyvenin olduðu pozisyonda oluþtur
    //        // (Quaternion.identity efekti döndürmeden, orijinal açýsýyla oluþturur)
    //        GameObject yeniEfekt = Instantiate(meyveVFX, pozisyon, Quaternion.identity);

    //        // --- BU KISIM KRÝTÝK ---
    //        // Efektin 'Stop Action' ayarý 'None' olduðu için (görselde öyle gördüm)
    //        // kodla silmeliyiz. Efekt bittikten sonra silinsin diye süre veriyoruz.
    //        // Durasyonu 1sn, ömrü 0.5sn demiþtik, 2 saniye sonra silmek güvenlidir.
    //        Destroy(yeniEfekt, 2f);
    //    }
    //}

    public void CanAzalt()
    {
        if (!oyunDevamEdiyor) return;
        can--;
        canYazisi.text = "Can: " + can;

        // Can bittiyse paneli açan fonksiyonu çaðýr
        if (can <= 0)
        {
            OyunBitti(); // Bu fonksiyon paneli SetActive(true) yapar.
        }
    }
    public AudioSource oyunBittiSesKaynagi; // Inspector'dan baðlayacaðýz
    void OyunBitti()
    {
        oyunDevamEdiyor = false;
        Time.timeScale = 0; // Oyunu dondur
        if (bitisPaneli != null)
        {
            bitisPaneli.SetActive(true); // Ýþte kapattýðýn o tiki bu satýr açar!
        }
        if (sonSkorYazisi != null) sonSkorYazisi.text = "TOPLAM PUAN: " + puan;
        Debug.Log("Oyun Tamamlandý! Skor: " + puan);
        // Buraya Bitiþ Paneli açma kodu gelecek
        // Oyun bitti sesini çal
        if (oyunBittiSesKaynagi != null)
            oyunBittiSesKaynagi.Play();
    }

    [Header("Ses Efektleri")]
    public AudioSource sesKaynagi;
    public AudioClip meyveSesi;
    public AudioClip engelSesi;
    public AudioClip bitisSesi;

    // Bu fonksiyonu diðer scriptlerden (KarakterKontrol gibi) çaðýracaðýz
    public void SesCal(string tip)
    {
        if (sesKaynagi == null) return; // Hata almamak için kontrol

        switch (tip)
        {
            case "meyve":
                sesKaynagi.PlayOneShot(meyveSesi);
                break;
            case "engel":
                sesKaynagi.PlayOneShot(engelSesi);
                break;
            case "bitis":
                sesKaynagi.PlayOneShot(bitisSesi);
                break;
        }
    }
    // Butona baðlayacaðýmýz fonksiyon
    public void YenidenBaslat()
    {
        Time.timeScale = 1; // Zamaný geri al
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Mevcut sahneyi tekrar yükle
    }
    public GameObject meyveVFX;
    public void PuanEkle(int miktar, Vector3 pozisyon)
    {
        puan += miktar;
        puanYazisi.text = "Puan: " + puan;

        if (meyveVFX != null)
        {
            // Efekti meyvenin pozisyonunda oluþtur
            GameObject yeniEfekt = Instantiate(meyveVFX, pozisyon, Quaternion.identity);
            // 2 saniye sonra sahnenden sil
            Destroy(yeniEfekt, 2f);
        }
    }
}