using UnityEngine;
using System.Collections.Generic;

public class Nesne_Havuzu : MonoBehaviour
{
    public static Nesne_Havuzu Instance;
    public GameObject[] meyvePrefablar;
    public GameObject[] engelPrefablar;
    public int havuzBoyutu = 30; // Sayýyý biraz artýralým ki yetmemezlik yapmasýn

    private List<GameObject> meyveHavuzu = new List<GameObject>();
    private List<GameObject> engelHavuzu = new List<GameObject>();

    void Awake() { Instance = this; }

    void Start()
    {
        for (int i = 0; i < havuzBoyutu; i++)
        {
            ObjeOlustur(true);
            ObjeOlustur(false);
        }
    }

    void ObjeOlustur(bool engelMi)
    {
        GameObject[] liste = engelMi ? engelPrefablar : meyvePrefablar;
        if (liste.Length == 0) return;

        GameObject obj = Instantiate(liste[Random.Range(0, liste.Length)], transform);
        obj.SetActive(false);
        if (engelMi) engelHavuzu.Add(obj); else meyveHavuzu.Add(obj);
    }

    public GameObject ObjeAl(bool engelMi)
    {
        List<GameObject> hedefHavuz = engelMi ? engelHavuzu : meyveHavuzu;

        for (int i = 0; i < hedefHavuz.Count; i++)
        {
            // EÐER OBJE SÝLÝNMÝÞSE LÝSTEDEN ÇIKAR (Hatanýn çözümü burada)
            if (hedefHavuz[i] == null)
            {
                hedefHavuz.RemoveAt(i);
                i--;
                continue;
            }

            if (!hedefHavuz[i].activeInHierarchy) return hedefHavuz[i];
        }

        // Havuzda boþ yoksa yeni bir tane oluþtur (Yedek plan)
        return null;
    }
}