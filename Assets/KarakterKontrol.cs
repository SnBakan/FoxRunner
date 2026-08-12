using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class KarakterKontrol : MonoBehaviour
{
    private CharacterController characterController; // Ýsmi netleþtirdik
    public float kosmaHizi = 70f;
    public float seritMesafesi = 5f;
    private int serit = 1;
    private bool hareketEdebilir = false;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Geciktirme iþlemini baþlatan fonksiyonu çaðýr
        StartCoroutine(BaslangicGecikmesi());
    }
    IEnumerator BaslangicGecikmesi()
    {
        // 2 saniye bekle
        yield return new WaitForSeconds(3f);
        hareketEdebilir = true;
    }
    void Update()
    {
        // 1. Ýleri Koþma
        Vector3 hareket = transform.forward * kosmaHizi;

        // 2. Þerit Kontrolü (A-D tuþlarý)
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            if (serit > 0) serit--;
        }
        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            if (serit < 2) serit++;
        }

        // 3. Hedef X Hesabý
        float hedefX = (serit - 1) * seritMesafesi;
        float yeniX = Mathf.Lerp(transform.position.x, hedefX, Time.deltaTime * 15f);

        Vector3 finalHareket = new Vector3(yeniX - transform.position.x, 0, hareket.z * Time.deltaTime);

        characterController.Move(finalHareket);
    }

    public int can = 3;
    private bool oyunBittimi = false;
    public float oyunSuresi = 120f;

    void OnTriggerEnter(Collider other)
    {
        // Eðer oyun zaten bittiyse (TimeScale=0 ise), çarpýþmalarý algýlama
        if (Time.timeScale == 0) return;

        if (other.CompareTag("Meyve"))
        {
            Oyun_Yonetici.Instance.PuanEkle(10, other.transform.position);
            Oyun_Yonetici.Instance.SesCal("meyve");
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Engel"))
        {
            Oyun_Yonetici.Instance.SesCal("engel");
            other.gameObject.SetActive(false);

            // --- BU KISIM ÇOK ÖNEMLÝ ---
            // CanAzalt() fonksiyonunu OyunYonetici'den çaðýr
            // Bu fonksiyonun içinde can <= 0 kontrolü ve paneli açma kodu var
            Oyun_Yonetici.Instance.CanAzalt();
        }
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    // Instance'ýn boþ olup olmadýðýný kontrol ederek hatayý önlüyoruz
    //    if (Oyun_Yonetici.Instance == null)
    //    {
    //        Debug.LogError("Sahnede Oyun_Yonetici scripti takýlý bir GameManager objesi bulunamadý!");
    //        return;
    //    }

    //    if (other.CompareTag("Meyve"))
    //    {
    //        Oyun_Yonetici.Instance.PuanEkle(10, other.transform.position);
    //        Oyun_Yonetici.Instance.SesCal("meyve"); // Ses tetiklendi
    //        other.gameObject.SetActive(false);
    //    }
    //    else if (other.CompareTag("Engel"))
    //    {
    //        Oyun_Yonetici.Instance.CanAzalt();
    //        Oyun_Yonetici.Instance.SesCal("engel"); // Ses tetiklendi
    //        other.gameObject.SetActive(false);
    //    }
    //}
    void OyunBitti()
    {
        oyunBittimi = true;
        Debug.Log("Oyun Sona Erdi.");
        Time.timeScale = 0; // Oyunu dondur
                            // Buraya oyun bitti paneli kodlarýný ekleyebilirsin
    }
}