using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için þart
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public void OyunuBaslat()
    {
        // Ses çalmasý için bu satýrý ekledik
        GetComponent<AudioSource>().Play();
        // Coroutine dediðimiz "zamanlamalý" iþlemi baþlatýyoruz
        StartCoroutine(BekleVeGec());
    }

    IEnumerator BekleVeGec()
    {
        // Notunda yazdýðýn gibi: 2 saniye bekle
        yield return new WaitForSeconds(1f);

        // Oyun sahnesine (Index 1) geç
        SceneManager.LoadScene(1);
    }

}