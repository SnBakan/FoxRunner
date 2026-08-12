using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class SkorKaydi
{
    public string isim;
    public int puan;
}

public class SkorSistemi : MonoBehaviour
{
   
    public static bool SkorKaydet(string isim, int puan)
    {
        List<SkorKaydi> skorlar = SkorlariGetir();
        bool ilkBeseGirdi = false;
        // 1. Bu isimde biri listede zaten var mý?
        SkorKaydi mevcutOyuncu = skorlar.Find(s => s.isim.ToLower() == isim.ToLower());

        if (mevcutOyuncu != null)
        {
            // Eðer oyuncu varsa, sadece yeni puaný eskisinden yüksekse güncelle
            if (puan > mevcutOyuncu.puan)
            {
                mevcutOyuncu.puan = puan;
                Debug.Log(isim + " için yeni rekor: " + puan);
            }
        }
        else
        {
            // Eðer oyuncu listede yoksa, yeni bir kayýt olarak ekle
            skorlar.Add(new SkorKaydi { isim = isim, puan = puan });
        }
       
        // 2. Listeyi büyükten küçüðe sýrala ve en iyi 5'i al
        var siraliListe = skorlar.OrderByDescending(s => s.puan).Take(5).ToList();
        // Sýralama yapýldýktan sonra oyuncu listede mi bak:
        if (siraliListe.Any(s => s.isim == isim && s.puan == puan))
        {
            ilkBeseGirdi = true;
        }
        // 3. Kaydet
        string json = JsonHelper.ToJson(siraliListe.ToArray());
        PlayerPrefs.SetString("LiderlikTablosu", json);
        PlayerPrefs.Save();
        return ilkBeseGirdi;
    }
    public static List<SkorKaydi> SkorlariGetir()
    {
        string json = PlayerPrefs.GetString("LiderlikTablosu", "");

        // Eðer kayýt yoksa hemen boþ liste dön, aþaðýya hiç inme
        if (string.IsNullOrEmpty(json) || json == "{}")
        {
            return new List<SkorKaydi>();
        }

        try
        {
            SkorKaydi[] dizi = JsonHelper.FromJson<SkorKaydi>(json);
            // Dizi null deðilse listeye çevir, null ise boþ liste dön
            if (dizi != null)
            {
                return new List<SkorKaydi>(dizi);
            }
        }
        catch
        {
            // Hata olursa boþ liste dön
        }

        return new List<SkorKaydi>();
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        // Konsoldaki {"Items":...} formatýný okumak için tam olarak bu yapý gerekir
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        if (wrapper == null || wrapper.Items == null) return new T[0];
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items; // Buradaki 'Items' ismi konsoldakiyle ayný olmalý
    }
}