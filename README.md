# 🧠 C# Hand-Written Digit Recognition (ML.NET & OpenCV)

Bu proje, **C# Windows Forms** üzerinde **Makine Öğrenimi (ML)**, **Görüntü İşleme (Image Processing)** ve **Matematiksel Hesaplamaları** birleştirerek el yazısı rakamları tanıyan bir masaüstü uygulamasıdır.

Uygulama, kullanıcının çizdiği rakamları OpenCV ile işler, matematiksel matrislere dönüştürür ve **ONNX (MNIST)** modeli kullanarak tahmin eder. Çift haneli sayıları (örn: "12", "45") ayırıp okuyabilir.

---

## ✨ Özellikler

* ✍️ **İnteraktif Çizim Alanı:** Fare ile serbest çizim yapabilme.
* 🔍 **Görüntü İşleme Pipeline'ı:** Gürültü temizleme, boyutlandırma (Resizing) ve gri tonlama.
* 🔢 **Çoklu Rakam Desteği:** Tek bir rakamı değil, yan yana yazılmış birden fazla rakamı (örn: 1923) algılayıp birleştirme.
* 🧠 **Yapay Zeka Entegrasyonu:** Eğitilmiş **MNIST ONNX** modeli ile yüksek doğruluklu tahmin.
* 📊 **Güven Skoru (Confidence Score):** Modelin tahmininden ne kadar emin olduğunu yüzdelik olarak gösterme (Softmax algoritması ile).

---

## 🛠️ Kullanılan Teknolojiler ve Kütüphaneler

* **Dil:** C# (.NET 6.0 / .NET 8.0)
* **Arayüz:** Windows Forms (WinForms)
* **Yapay Zeka:** `Microsoft.ML` (ML.NET), `Microsoft.ML.OnnxTransformer`
* **Görüntü İşleme:** `OpenCvSharp4`, `OpenCvSharp4.runtime.win`
* **Model:** MNIST (ONNX formatında)

---

## 🚀 Kurulum ve Çalıştırma

Projeyi bilgisayarınızda çalıştırmak için şu adımları izleyin:

1.  **Projeyi Klonlayın:**
    ```bash
    git clone [https://github.com/SugraKeles/ML-math-image-process.git](https://github.com/SugraKeles/ML-math-image-process.git)
    ```
2.  **Visual Studio ile Açın:** `.sln` dosyasını Visual Studio 2022 ile açın.
3.  **NuGet Paketlerini Yükleyin:**
    * Çözüme sağ tıklayıp "Restore NuGet Packages" diyerek gerekli kütüphanelerin inmesini sağlayın.
4.  **Model Dosyasını Ekleyin (ÖNEMLİ):**
    * `mnist.onnx` dosyasının projenin ana dizininde olduğundan emin olun.
    * Dosyaya sağ tıklayıp **Properties** (Özellikler) penceresinden **"Copy to Output Directory"** seçeneğini **"Copy if newer"** yapın.
5.  **Başlatın:** Projeyi `x64` modunda derleyip çalıştırın.

---

## ⚙️ Nasıl Çalışır? (Teknik Detay)

Uygulamanın arka planındaki veri akışı (Pipeline) şu şekildedir:

1.  **Input (Girdi):** Kullanıcı `PictureBox` üzerine çizim yapar (`Bitmap`).
2.  **Preprocessing (OpenCV):**
    * Görüntü gri tona çevrilir (`CvtColor`).
    * Renkler ters çevrilir (Siyah arka plan, beyaz yazı).
    * `FindContours` ile rakamlar birbirinden ayrıştırılır.
    * Her rakam kesilip 28x28 boyutundaki bir matrisin merkezine yerleştirilir.
3.  **Math (Matematiksel Dönüşüm):**
    * Piksel değerleri 0-255 aralığından **0-1** aralığına normalize edilir.
    * 2D Matris -> 1D Vektöre (Flattening) dönüştürülür.
4.  **Inference (Tahmin):**
    * Vektör, ML.NET üzerinden ONNX modeline gönderilir.
    * Model ham skorları (Logits) üretir.
5.  **Post-Processing:**
    * **Softmax** fonksiyonu ile skorlar olasılığa çevrilir.
    * En yüksek olasılığa sahip rakam ekrana yazdırılır.

---

## 🤝 Katkıda Bulunma

1.  Bu depoyu Fork'layın.
2.  Yeni bir özellik dalı (branch) oluşturun (`git checkout -b feature/YeniOzellik`).
3.  Değişikliklerinizi commit edin (`git commit -m 'Yeni özellik eklendi'`).
4.  Branch'inizi Push edin (`git push origin feature/YeniOzellik`).
5.  Bir Pull Request oluşturun.

---

## 📄 Lisans

Bu proje [MIT](LICENSE) lisansı ile lisanslanmıştır.
