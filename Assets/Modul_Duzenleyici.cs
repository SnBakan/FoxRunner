using System.Collections.Generic;
using UnityEngine;

public class Modul_Duzenleyici : MonoBehaviour
{
    public float yolUzunlugu = 10f;
    private float[] seritler = { -30f, 0f, 30f };
    private List<GameObject> suankiObjeler = new List<GameObject>();

    void OnEnable()
    {
        DizilimiTemizle();
        // Sadece 40 birimde bir (her 4 yol bloðundan birinde) nesne oluþtur
        // transform.position.z % 40 kontrolü ile aralýklarý açýyoruz
        if (transform.position.z > 30f && Mathf.Abs(transform.position.z % 20f) < 1f)
        {
            Invoke("ObjeleriYerlestir", 0.1f);
        }
    }

    void ObjeleriYerlestir()
    {
        List<int> kullanilabilirSeritler = new List<int> { 0, 1, 2 };
        int engelSayisi = Random.Range(0, 3); // 0, 1 veya 2 engel
        int meyveSayisi;

        // Kural: 2 engel varsa, en fazla 1 meyve olabilir
        if (engelSayisi == 2) meyveSayisi = Random.Range(0, 2);
        else meyveSayisi = Random.Range(0, 3); // 0, 1 veya 2 meyve

        // Önce Engelleri Yerleþtir
        for (int i = 0; i < engelSayisi; i++)
        {
            if (kullanilabilirSeritler.Count == 0) break;
            int index = Random.Range(0, kullanilabilirSeritler.Count);
            int seritIdx = kullanilabilirSeritler[index];
            NesneOlustur(seritIdx, true);
            kullanilabilirSeritler.RemoveAt(index);
        }

        // Sonra Meyveleri Yerleþtir
        for (int i = 0; i < meyveSayisi; i++)
        {
            if (kullanilabilirSeritler.Count == 0) break;
            int index = Random.Range(0, kullanilabilirSeritler.Count);
            int seritIdx = kullanilabilirSeritler[index];
            NesneOlustur(seritIdx, false);
            kullanilabilirSeritler.RemoveAt(index);
        }
    }

    void NesneOlustur(int seritIndex, bool engelMi)
    {
        if (Nesne_Havuzu.Instance == null) return;

        GameObject obje = Nesne_Havuzu.Instance.ObjeAl(engelMi);
        if (obje != null)
        {
            float secilenSerit = seritler[seritIndex];
            obje.transform.position = transform.position + new Vector3(secilenSerit, 0.8f, 5f);
            obje.transform.SetParent(this.transform);
            obje.SetActive(true);
            suankiObjeler.Add(obje);
        }
    }

    //public void DizilimiTemizle()
    //{
    //    for (int i = suankiObjeler.Count - 1; i >= 0; i--)
    //    {
    //        if (suankiObjeler[i] != null)
    //        {
    //            suankiObjeler[i].SetActive(false);
    //            if (Nesne_Havuzu.Instance != null)
    //                suankiObjeler[i].transform.SetParent(Nesne_Havuzu.Instance.transform);
    //        }
    //    }
    //    suankiObjeler.Clear();
    //}

    public void DizilimiTemizle()
    {
        // Listeyi ters döngüyle temizlemek her zaman daha güvenlidir
        for (int i = suankiObjeler.Count - 1; i >= 0; i--)
        {
            if (suankiObjeler[i] != null)
            {
                suankiObjeler[i].SetActive(false);
                if (Nesne_Havuzu.Instance != null)
                    suankiObjeler[i].transform.SetParent(Nesne_Havuzu.Instance.transform);
            }
        }
        suankiObjeler.Clear();
    }
}