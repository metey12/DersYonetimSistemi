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

## 🧾 Lisans

Bu proje MIT lisansı ile lisanslanmıştır. Detaylar için `LICENSE` dosyasını inceleyin.
