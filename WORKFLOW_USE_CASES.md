# Mevcut Workflow Yapınızla Yapabileceğiniz İş Süreçleri

## 🎯 Mevcut Node Tipleri

✅ **startNode** - İş akışını başlatma  
✅ **formNode** - Form işleme ve kullanıcı etkileşimi  
✅ **approverNode** - Onay süreçleri  
✅ **scriptNode** - JavaScript ile koşullu mantık  
✅ **sqlConditionNode** - Veritabanı koşulları  
✅ **alertNode** - Bildirimler  
✅ **stopNode** - İş akışını sonlandırma  

---

## 📋 Kategori 1: Onay Süreçleri (Approval Workflows)

### 1.1 Basit Onay Süreci
```
Start → Form (İzin Talebi) → Manager Onayı → End
```

**Kullanım Senaryosu:**
- İzin talepleri
- Harcama onayları
- Basit doküman onayları

**Node Yapısı:**
- startNode
- formNode (İzin Formu)
- approverNode (Manager)
- stopNode

---

### 1.2 Çok Seviyeli Onay Süreci
```
Start → Form → Manager Onayı → Director Onayı → CEO Onayı → End
```

**Kullanım Senaryosu:**
- Yüksek tutarlı harcamalar
- İşe alım süreçleri
- Büyük proje onayları

**Node Yapısı:**
- startNode
- formNode
- approverNode (Manager)
- approverNode (Director)
- approverNode (CEO)
- stopNode

---

### 1.3 Koşullu Onay Süreci
```
Start → Form → Script (Tutar Kontrolü) 
  → Tutar < 1000: Otomatik Onay → End
  → Tutar >= 1000: Manager Onayı → End
```

**Kullanım Senaryosu:**
- Harcama onayları (tutara göre)
- İzin onayları (süreye göre)
- Satın alma onayları

**Node Yapısı:**
- startNode
- formNode
- scriptNode (Tutar kontrolü)
  - yes → stopNode (Otomatik Onay)
  - no → approverNode → stopNode

**Script Örneği:**
```javascript
if (previousNodes.HARCAMA.tutar < 1000) {
  return true; // Otomatik onay
} else {
  return false; // Manager onayı gerekli
}
```

---

### 1.4 Geri Gönderme (Send Back) Süreci
```
Start → Form → Approver (Onay/Red/Geri Gönder) 
  → Onay: End
  → Red: End
  → Geri Gönder: Form (Düzeltme) → Approver
```

**Kullanım Senaryosu:**
- Eksik bilgili formların düzeltilmesi
- Revizyon gerektiren dokümanlar

**Node Yapısı:**
- startNode
- formNode
- approverNode (Onay/Red/SendBack)
  - Onay → stopNode
  - Red → stopNode
  - SendBack → formNode (Düzeltme) → approverNode

---

## 📋 Kategori 2: Form-Based İş Süreçleri

### 2.1 Form Doldurma ve Doğrulama
```
Start → Form → Script (Validasyon) 
  → Geçerli: Alert (Başarılı) → End
  → Geçersiz: Alert (Hata) → Form (Düzeltme)
```

**Kullanım Senaryosu:**
- Müşteri kayıt formları
- Başvuru formları
- Anket formları

**Node Yapısı:**
- startNode
- formNode
- scriptNode (Validasyon)
  - yes → alertNode (Başarılı) → stopNode
  - no → alertNode (Hata) → formNode → scriptNode

---

### 2.2 Çok Adımlı Form Süreci
```
Start → Form 1 (Kişisel Bilgiler) → Form 2 (İletişim) 
  → Form 3 (Adres) → Alert (Tamamlandı) → End
```

**Kullanım Senaryosu:**
- KYC (Know Your Customer) süreçleri
- Çok adımlı kayıt formları
- Profil tamamlama

**Node Yapısı:**
- startNode
- formNode (Adım 1)
- formNode (Adım 2)
- formNode (Adım 3)
- alertNode
- stopNode

---

### 2.3 Koşullu Form Gösterimi
```
Start → Form → Script (Kullanıcı Tipi Kontrolü)
  → Müşteri: Form (Müşteri Bilgileri) → End
  → Çalışan: Form (Çalışan Bilgileri) → End
```

**Kullanım Senaryosu:**
- Farklı kullanıcı tipleri için formlar
- Dinamik form gösterimi

**Script Örneği:**
```javascript
if (previousNodes.KAYIT.kullaniciTipi == "Musteri") {
  return true; // Müşteri formu
} else {
  return false; // Çalışan formu
}
```

---

## 📋 Kategori 3: Koşullu İş Mantığı (Business Rules)

### 3.1 Tutar Bazlı İşlemler
```
Start → Form → Script (Tutar Kontrolü)
  → < 500: Otomatik İşlem → End
  → 500-5000: Manager Onayı → End
  → > 5000: Director Onayı → End
```

**Kullanım Senaryosu:**
- Harcama onayları
- Satın alma süreçleri
- Ödeme onayları

**Script Örneği:**
```javascript
var tutar = previousNodes.HARCAMA.tutar;

if (tutar < 500) {
  return "otomatik";
} else if (tutar < 5000) {
  return "manager";
} else {
  return "director";
}
```

---

### 3.2 Tarih/Süre Bazlı İşlemler
```
Start → Form → Script (Süre Kontrolü)
  → < 3 gün: Otomatik Onay → End
  → >= 3 gün: Manager Onayı → End
```

**Kullanım Senaryosu:**
- İzin talepleri
- Proje süre uzatmaları
- Deadline kontrolü

**Script Örneği:**
```javascript
var izinGunu = previousNodes.IZIN.gunSayisi;

if (izinGunu < 3) {
  return true; // Otomatik onay
} else {
  return false; // Manager onayı
}
```

---

### 3.3 Kullanıcı Rol Bazlı İşlemler
```
Start → Form → Script (Rol Kontrolü)
  → Admin: Otomatik Onay → End
  → Manager: Director Onayı → End
  → User: Manager Onayı → End
```

**Kullanım Senaryosu:**
- Yetki bazlı onaylar
- Rol bazlı iş akışları

**Script Örneği:**
```javascript
var kullaniciRolu = workflow.currentUser.role;

if (kullaniciRolu == "Admin") {
  return "otomatik";
} else if (kullaniciRolu == "Manager") {
  return "director";
} else {
  return "manager";
}
```

---

### 3.4 Veritabanı Koşullu İşlemler
```
Start → Form → SQL Condition (Stok Kontrolü)
  → Stok Var: Onay → End
  → Stok Yok: Alert (Stok Yok) → End
```

**Kullanım Senaryosu:**
- Stok kontrolü
- Müşteri kredi limiti kontrolü
- Kullanılabilirlik kontrolü

**SQL Condition Örneği:**
```json
{
  ">": [
    {"var": "stokMiktari"},
    0
  ]
}
```

---

## 📋 Kategori 4: Bildirim ve Uyarı Süreçleri

### 4.1 Başarı/Hata Bildirimleri
```
Start → Form → Script (İşlem)
  → Başarılı: Alert (Başarılı) → End
  → Hata: Alert (Hata) → Form (Düzeltme)
```

**Kullanım Senaryosu:**
- Form gönderimi bildirimleri
- İşlem sonucu bildirimleri
- Hata durumu uyarıları

---

### 4.2 Onay Bekleme Bildirimleri
```
Start → Form → Approver (Pending) → Alert (Onay Bekleniyor) 
  → Approver (Onay) → Alert (Onaylandı) → End
```

**Kullanım Senaryosu:**
- Onay süreçlerinde bildirimler
- Durum güncellemeleri

---

### 4.3 Çoklu Bildirim Süreci
```
Start → Form → Alert (Bilgilendirme) → Approver 
  → Alert (Onaylandı) → Alert (Tamamlandı) → End
```

**Kullanım Senaryosu:**
- Adım adım bildirimler
- Süreç takibi

---

## 📋 Kategori 5: Karmaşık İş Süreçleri

### 5.1 İşe Alım Süreci
```
Start → Form (Başvuru) → Script (CV Kontrolü)
  → Geçerli: HR Onayı → Manager Onayı → Alert (Mülakat) 
    → Form (Mülakat Sonucu) → Director Onayı → End
  → Geçersiz: Alert (Red) → End
```

**Node Yapısı:**
- startNode
- formNode (Başvuru)
- scriptNode (CV Kontrolü)
  - yes → approverNode (HR) → approverNode (Manager) 
    → alertNode → formNode → approverNode (Director) → stopNode
  - no → alertNode (Red) → stopNode

---

### 5.2 Satın Alma Süreci
```
Start → Form (Satın Alma Talebi) → Script (Tutar Kontrolü)
  → < 1000: Otomatik Onay → Alert (Onaylandı) → End
  → 1000-10000: Manager Onayı → Alert → End
  → > 10000: Manager → Director → Alert → End
```

**Node Yapısı:**
- startNode
- formNode
- scriptNode (Tutar)
  - < 1000 → alertNode → stopNode
  - 1000-10000 → approverNode → alertNode → stopNode
  - > 10000 → approverNode → approverNode → alertNode → stopNode

---

### 5.3 İzin Talebi Süreci
```
Start → Form (İzin Talebi) → Script (Süre Kontrolü)
  → < 3 gün: Manager Onayı → Alert → End
  → >= 3 gün: Manager → HR → Alert → End
```

**Script Örneği:**
```javascript
var izinGunu = previousNodes.IZIN.gunSayisi;

if (izinGunu < 3) {
  return "manager"; // Sadece manager onayı
} else {
  return "hr"; // Manager + HR onayı
}
```

---

### 5.4 Proje Onay Süreci
```
Start → Form (Proje Önerisi) → Script (Bütçe Kontrolü)
  → < 50000: Manager Onayı → End
  → >= 50000: Manager → Director → CEO → End
```

---

## 📋 Kategori 6: Veri İşleme Süreçleri

### 6.1 Veri Doğrulama ve Dönüştürme
```
Start → Form → Script (Veri Dönüştürme) 
  → Script (Validasyon) → Alert (Sonuç) → End
```

**Kullanım Senaryosu:**
- Veri import/export
- Veri temizleme
- Format dönüştürme

---

### 6.2 Hesaplama Süreçleri
```
Start → Form → Script (Hesaplama) → Alert (Sonuç) → End
```

**Kullanım Senaryosu:**
- Maaş hesaplamaları
- Fiyat hesaplamaları
- Komisyon hesaplamaları

**Script Örneği:**
```javascript
var tutar = previousNodes.SATIS.tutar;
var komisyonOrani = 0.10;
var komisyon = tutar * komisyonOrani;

return komisyon;
```

---

## 📋 Kategori 7: Entegrasyon Süreçleri

### 7.1 Ticket Sistemi Entegrasyonu
```
Start → Form (Ticket) → Script (Öncelik Belirleme)
  → Yüksek: Manager Onayı → Alert → End
  → Normal: Otomatik → Alert → End
```

**Mevcut Kullanım:**
```236:270:formneo.api/Controllers/TicketController.cs
            bool sendApprove = false;
            if (definationId != Guid.Empty.ToString())
            {
                WorkFlowStartApiDto workFlowApiDto = new WorkFlowStartApiDto();
                // ... workflow başlatılıyor
                var result = await execute.StartAsync(workFlowDto, parameters, json);
                ticket.WorkflowHeadId = new Guid(mapResult.Id);
                ticket.Status = TicketStatus.InApprove;
            }
```

---

## 🎯 Özet: Yapabileceğiniz İş Süreçleri

### ✅ **Yapabilecekleriniz:**

1. **Onay Süreçleri** ⭐⭐⭐⭐⭐
   - Basit onaylar
   - Çok seviyeli onaylar
   - Koşullu onaylar
   - Geri gönderme

2. **Form İşlemleri** ⭐⭐⭐⭐⭐
   - Form doldurma
   - Çok adımlı formlar
   - Form validasyonu
   - Koşullu form gösterimi

3. **Koşullu Mantık** ⭐⭐⭐⭐
   - Tutar bazlı işlemler
   - Tarih/süre bazlı işlemler
   - Rol bazlı işlemler
   - Veritabanı koşulları

4. **Bildirimler** ⭐⭐⭐⭐
   - Başarı/hata bildirimleri
   - Onay bildirimleri
   - Çoklu bildirimler

5. **Karmaşık Süreçler** ⭐⭐⭐
   - İşe alım
   - Satın alma
   - İzin talepleri
   - Proje onayları

### ❌ **Yapamayacaklarınız (Şu An İçin):**

1. **Paralel İşlemler** ❌
   - Aynı anda birden fazla işlem

2. **Zamanlanmış İşlemler** ❌
   - Timer-based otomasyon
   - Otomatik timeout

3. **Event-Driven İşlemler** ❌
   - Dış sistemlerden event bekleme
   - Asenkron işlemler

---

## 💡 Pratik Örnekler

### Örnek 1: Harcama Onay Süreci
```json
{
  "nodes": [
    {"id": "start", "type": "startNode"},
    {"id": "form", "type": "formNode", "data": {"name": "Harcama Formu"}},
    {"id": "script", "type": "scriptNode", "data": {
      "script": "if (previousNodes.HARCAMA.tutar < 1000) return true; else return false;"
    }},
    {"id": "approve", "type": "approverNode", "data": {"approvername": "Manager"}},
    {"id": "alert", "type": "alertNode", "data": {"message": "Onaylandı"}},
    {"id": "end", "type": "stopNode"}
  ],
  "edges": [
    {"source": "start", "target": "form"},
    {"source": "form", "target": "script"},
    {"source": "script", "target": "approve", "sourceHandle": "no"},
    {"source": "script", "target": "alert", "sourceHandle": "yes"},
    {"source": "approve", "target": "alert"},
    {"source": "alert", "target": "end"}
  ]
}
```

### Örnek 2: İzin Talebi Süreci
```json
{
  "nodes": [
    {"id": "start", "type": "startNode"},
    {"id": "form", "type": "formNode", "data": {"name": "İzin Formu"}},
    {"id": "script", "type": "scriptNode", "data": {
      "script": "if (previousNodes.IZIN.gunSayisi < 3) return true; else return false;"
    }},
    {"id": "manager", "type": "approverNode", "data": {"approvername": "Manager"}},
    {"id": "hr", "type": "approverNode", "data": {"approvername": "HR"}},
    {"id": "alert", "type": "alertNode", "data": {"message": "İzin Onaylandı"}},
    {"id": "end", "type": "stopNode"}
  ],
  "edges": [
    {"source": "start", "target": "form"},
    {"source": "form", "target": "script"},
    {"source": "script", "target": "manager", "sourceHandle": "no"},
    {"source": "script", "target": "alert", "sourceHandle": "yes"},
    {"source": "manager", "target": "hr"},
    {"source": "hr", "target": "alert"},
    {"source": "alert", "target": "end"}
  ]
}
```

---

## 🎯 Sonuç

Mevcut workflow yapınızla **%80-90** iş sürecini karşılayabilirsiniz:

✅ **Mükemmel:** Onay süreçleri, form işlemleri  
✅ **İyi:** Koşullu mantık, bildirimler  
✅ **Orta:** Karmaşık süreçler (paralel olmayan)  
❌ **Eksik:** Paralel işlemler, timer events, event-driven

**Öneri:** Mevcut yapıyı kullanmaya devam edin, ihtiyaç oldukça eksik özellikleri ekleyin!

