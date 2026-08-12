using UnityEngine;
using System.Collections.Generic;

public class Yol_Uretici : MonoBehaviour
{
    public GameObject yolPrefab;
    public Transform oyuncu;
    public float yolUzunlugu = 10f; // Artýk 10f olduðunu biliyoruz
    private float zSpawn = 0;
    private int yolSayisi = 30; // Sahne ayný anda 30 parça (300 birim) yol olsun
    private List<GameObject> aktifYollar = new List<GameObject>();

    void Start()
    {
        // Baþlangýçta 200 birimlik yolu hemen döþe
        for (int i = 0; i < yolSayisi; i++)
        {
            YolEkle();
        }
    }

    void Update()
    {
        // Yeni yol ekleme mesafesi (Ýleride oluþsun)
        if (oyuncu.position.z > zSpawn - (yolSayisi * yolUzunlugu +1))
        {
            YolEkle();
        }

        // Yolu silme mesafesi (Tilkiden en az 40 birim arkada kalsýn)
        // 10 birimlik yollarda, tilkinin 40 birim gerisindeki yollarý silmek güvenlidir
        if (aktifYollar.Count > 0 && oyuncu.position.z - 40f > aktifYollar[0].transform.position.z)
        {
            YolSil();
        }
    }

    void YolEkle()
    {
        GameObject go = Instantiate(yolPrefab, new Vector3(0, 0, zSpawn), Quaternion.identity);
        aktifYollar.Add(go);
        zSpawn += yolUzunlugu;
    }

    void YolSil()
    {
        // Arkada kalan yollarý temizle
        if (aktifYollar.Count > 0)
        {
            Modul_Duzenleyici md = aktifYollar[0].GetComponent<Modul_Duzenleyici>();
            if (md != null) md.DizilimiTemizle();

            Destroy(aktifYollar[0]);
            aktifYollar.RemoveAt(0);
        }
    }
}