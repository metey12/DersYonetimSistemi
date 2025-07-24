# 🎓 Ders Yönetim Sistemi

ASP.NET Core MVC ile geliştirilmiş bu sistem, öğretmen ve öğrenciler için ders yönetimi, içerik paylaşımı ve kullanıcı yönetimi gibi işlevleri barındırır. Kullanıcılar rollerine göre sisteme giriş yaparak yetkili oldukları işlemleri gerçekleştirebilir.

---

## 📚 Özellikler

### 👨‍🏫 Öğretmenler
- Ders oluşturma
- Haftalara PDF materyali yükleme
- Derse öğrenci ekleme ve çıkarma
- Kendi derslerini görüntüleme ve düzenleme

### 👨‍🎓 Öğrenciler
- Derse kayıt olma
- Kayıtlı ders içeriklerini görüntüleme
- Haftalık PDF materyallerini indirme

### 🔐 Kimlik Doğrulama & Rol Yönetimi
- ASP.NET Core Identity sistemi
- Roller: `Ogretmen`, `Ogrenci`
- Yetkilendirme: Her kullanıcı sadece kendi yetkili olduğu içeriklere erişebilir

---

## 🧰 Kullanılan Teknolojiler

- ASP.NET Core MVC
- Entity Framework Core (Code First)
- ASP.NET Identity
- SQLite Veritabanı
- Bootstrap 5
- LINQ, Asenkron programlama
- File Upload işlemleri (`wwwroot/uploads`)

---


## ⚙️ Kurulum Talimatları

### 1. Bağımlılıkları yükleyin:
```bash
dotnet restore
```

### 2. Veritabanını oluşturun:
```bash
dotnet ef database update
```

> Gerekirse Entity Framework CLI yükleyin:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

### 3. Uygulamayı başlatın:
```bash
dotnet run
```

> Varsayılan olarak `https://localhost:5001` adresinde çalışır.

---

## 📝 Notlar

- Kayıt sırasında kullanıcıya rol (`Ogretmen` veya `Ogrenci`) atanır.
- Öğrenciler sadece kayıt oldukları ders içeriklerine erişebilir.
- Öğretmenler yalnızca kendi derslerini yönetebilir.
- Dosyalar `wwwroot/uploads` klasörüne kaydedilir.

---

## 👨‍💻 Geliştirici

**Mete Yıldırım**  
Bilgisayar Mühendisliği Öğrencisi  
🌐 [mete.wtf](https://mete.wtf)

---

## Galeri

<a href="https://github.com/user-attachments/assets/ebaeb8a2-ebc3-4c4e-a901-abb33088e197">
  <img src="https://github.com/user-attachments/assets/ebaeb8a2-ebc3-4c4e-a901-abb33088e197" width="400"/>
</a>

<a href="https://github.com/user-attachments/assets/34b29b23-f1b5-44c1-9682-36ce0fa51a75">
  <img src="https://github.com/user-attachments/assets/34b29b23-f1b5-44c1-9682-36ce0fa51a75" width="400"/>
</a>

<a href="https://github.com/user-attachments/assets/937f50b8-67c9-412e-8a0d-8e726e66cb38">
  <img src="https://github.com/user-attachments/assets/937f50b8-67c9-412e-8a0d-8e726e66cb38" width="400"/>
</a>

<a href="https://github.com/user-attachments/assets/d4e95d30-bfc5-4c27-9b47-05c163c76ef6">
  <img src="https://github.com/user-attachments/assets/d4e95d30-bfc5-4c27-9b47-05c163c76ef6" width="400"/>
</a>

<a href="https://github.com/user-attachments/assets/b16077c4-1688-4b43-a4f5-d283417d72be">
  <img src="https://github.com/user-attachments/assets/b16077c4-1688-4b43-a4f5-d283417d72be" width="400"/>
</a>

<a href="https://github.com/user-attachments/assets/5b149c34-dace-402d-b255-cab9bca851d0">
  <img src="https://github.com/user-attachments/assets/5b149c34-dace-402d-b255-cab9bca851d0" width="400"/>
</a>

<a href="https://github.com/user-attachments/assets/bd65237f-3d42-48ef-90e8-3d883306841e">
  <img src="https://github.com/user-attachments/assets/bd65237f-3d42-48ef-90e8-3d883306841e" width="400"/>
</a>

<a href="https://github.com/user-attachments/assets/03f884c7-7b27-49b5-8059-2f2d1cfc3820">
  <img src="https://github.com/user-attachments/assets/03f884c7-7b27-49b5-8059-2f2d1cfc3820" width="400"/>
</a>

<a href="https://github.com/user-attachments/assets/ebaeb8a2-ebc3-4c4e-a901-abb33088e197">
  <img src="https://github.com/user-attachments/assets/ebaeb8a2-ebc3-4c4e-a901-abb33088e197" width="400"/>
</a>

---

## 🧾 Lisans

Bu proje MIT lisansı ile lisanslanmıştır. Detaylar için `LICENSE` dosyasını inceleyin.



