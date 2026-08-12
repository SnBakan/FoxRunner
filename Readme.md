# 🦊 Tilki Koşusu (Fox Runner) - 3D Sonsuz Koşu & Skor Oyunu

**Tilki Koşusu**, Unity 3D oyun motoru ile geliştirilmiş; oyuncunun bir tilki karakterini yönlendirerek engellerden kaçındığı, meyveleri toplayarak puan kazandığı ve zamana/can hakkına karşı yarıştığı dinamik bir 3D runner (koşu) oyunudur.

---

## 🎯 Proje Künyesi
* **Tür:** 3D Endless / Lane-Based Runner
* **Platform:** Masaüstü (Desktop - Windows .EXE)
* **Geliştirme Ortamı:** Unity 3D & C#
* **Hedef Kitle:** Genel Oyuncu Kitlesi (Casual / Arcade)

---

## 🕹️ Oyun Mekanikleri & Sistemler

### 1. Karakter & Engel Mekaniği
* **Şerit Değiştirme ve Hareket:** Karakter kulvarlar arasında sağa/sola hareket edebilir ve engellerin üzerinden zıplayabilir.
* **Can Sistemi (3 Can):** Oyuncu yoldaki engellere (taşlar, kütükler vb.) çarptığında 1 can kaybeder. Can sayısı `0` olduğunda oyun sonlanır.
* **Sesli Geri Bildirim:** Meyve toplama ve engellere çarpma anlarında anlık ses efektleri (SFX) tetiklenir.

### 2. Puan & Süre Yönetimi
* **Toplanabilir Öğeler (Collectibles):** Yol üzerine dizilmiş olan meyveler (karpuz, elma, kiraz vb.) toplandığında oyuncunun puan hanesine (+10 / +20) katkı sağlar.
* **Geri Sayım Sayacı (Timer):** Oyun 120 saniyelik bir süre limiti ile başlar. Süre bittiğinde veya canlar tükendiğinde oyun sonu paneli devreye girer.

### 3. Dinamik Liderlik Tablosu (Leaderboard System)
* **Skor Kaydı:** Oyun bittiğinde oyuncunun karşısına çıkan arayüzde kullanıcı adı (`Nick`) girme alanı yer alır.
* **Yerel Veri Saklama:** "Kaydet" butonuna basıldığında oyuncunun nicki ve elde ettiği puan Liderlik Tablosuna dinamik olarak işlenir ve sıralamaya dahil edilir.
* **Yeniden Oyna:** "Tekrar Oyna" seçeneği ile sahne derhal sıfırlanarak hızlıca yeni bir tura başlanabilir.

---

## 🛠️ Yazılım Mimarisi & Öne Çıkan Kod Yapıları

* **Trigger & Collision Management:** Toplanabilir objelerde `OnTriggerEnter` mimarisi kullanılarak performanslı bir algılama sağlanmış; engellerde ise can düşürme ve devrilme/hasar efektleri kontrol edilmiştir.
* **UI & Event Management:** Can, süre ve puan sayaçları UI Canvas üzerinden anlık olarak güncellenmektedir. Oyun bitişinde zaman ölçeği (`Time.timeScale`) ve girdi alanları mantıksal bir akışla yönetilir.
* **Audio Manager:** Oyun boyunca devam eden arka plan müziği (BGM) ve etkileşim anlarında devreye giren efektler (SFX) modüler bir ses mimarisi üzerinden kanallara ayrılmıştır.

---

## ⌨️ Kontroller

| Eylem | Tuş / Girdi |
| :--- | :--- |
| **Sol / Sağ Hareket** | `A` / `D` veya `Sol` / `Sağ` Yön Tuşları |
| **Zıplama** | `W` / `Yukarı Yön Tuşu` veya `Space` |

---

## 🚀 Kurulum ve Çalıştırma

1. Repository'nin sağ tarafındaki **Releases** bölümünden `.zip` uzantılı oyun dosyasını indirin.
2. Sıkıştırılmış dosyayı bilgisayarınızda bir klasöre çıkartın.
3. Klasör içindeki **`TilkiOyunu.exe`** dosyasına çift tıklayarak oyunu oynayabilirsiniz (Unity kurulumu gerektirmez).

---

👩‍💻 Geliştirici Bilgileri & İletişim
**Geliştirici: Şeyma Nur BAKAN
**Unvan / Rol: 2. Sınıf Yönetim Bilişim Sistemleri Öğrencisi
**GitHub: (https://github.com/SnBakan)
**LinkedIn: (https://tr.linkedin.com/in/seymanurbakan)
**E-posta: seymanurbakan.467@gmail.com

---

🎓 Akademik Bağlam & Proje Geçmişi
**Kurum / Bölüm: Bursa Uludağ Üniversitesi / Yönetim Bilişim Sistemleri
**Geliştirme Tarihi: Nisan 2026
