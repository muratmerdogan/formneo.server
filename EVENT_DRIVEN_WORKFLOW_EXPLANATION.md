# Event-Driven Workflow'lar - Detaylı Açıklama

## 📚 Temel Kavram

### **Event-Driven Workflow Nedir?**

Event-driven workflow, **dış olaylara (events) göre tetiklenen ve yönlendirilen** iş akışlarıdır. 

**Klasik Workflow (Mevcut Yapınız):**
```
Start → Form → Approve → End
```
- **Aktif:** Kullanıcı bir aksiyon yapar → workflow ilerler
- **Sıralı:** Her adım bir önceki adımın tamamlanmasını bekler
- **Senkron:** İşlem anında gerçekleşir

**Event-Driven Workflow:**
```
Start → Wait for Event → [Event Gelince] → Process → End
```
- **Pasif:** Workflow bir event bekler → event gelince devam eder
- **Asenkron:** Event'ler zamanında gelebilir
- **Reaktif:** Dış sistemlerden gelen event'lere tepki verir

---

## 🔄 İki Yaklaşımın Karşılaştırması

### **1. Request-Driven (Mevcut Yapınız)**

```csharp
// Kullanıcı bir butona basar
[HttpPost]
public async Task<ActionResult> Start(WorkFlowStartApiDto dto)
{
    // Workflow hemen başlar ve çalışır
    var result = await execute.StartAsync(workFlowDto, parameters, payloadJson);
    return result;
}
```

**Özellikler:**
- ✅ Anında çalışır
- ✅ Kullanıcı kontrolündedir
- ❌ Kullanıcı olmadan çalışmaz
- ❌ Dış sistemlerden tetiklenemez

**Örnek Senaryo:**
```
Kullanıcı formu doldurur → "Gönder" butonuna basar → Workflow başlar
```

---

### **2. Event-Driven (Eksik Olan)**

```csharp
// Dış bir sistemden event gelir
public async Task HandleExternalEvent(WorkflowEvent @event)
{
    // Event'i bekleyen workflow instance'ı bul
    var waitingInstance = await FindWaitingInstance(@event.CorrelationKey);
    
    // Event'i workflow'a gönder ve devam ettir
    await workflow.ContinueWithEvent(waitingInstance, @event);
}
```

**Özellikler:**
- ✅ Dış sistemlerden tetiklenebilir
- ✅ Zamanlanmış olaylar (timer) destekler
- ✅ Asenkron çalışır
- ✅ Sistemler arası entegrasyon sağlar

**Örnek Senaryo:**
```
Workflow başlar → Bir event bekler (örn: "Ödeme Onaylandı") 
→ Dış sistem ödeme yapar → Event gönderir → Workflow devam eder
```

---

## 🎯 Event-Driven Workflow Senaryoları

### **Senaryo 1: Timer Event (Zamanlanmış İşlemler)**

**Problem:**
```
Bir form gönderildi, 3 gün içinde onaylanmazsa otomatik reddedilsin
```

**Mevcut Yapınız (Çalışmaz):**
```csharp
// ❌ Timer yok, manuel kontrol gerekir
if (approvalDate.AddDays(3) < DateTime.Now)
{
    // Bu kod hiç çalışmaz çünkü event yok
}
```

**Event-Driven Çözüm:**
```csharp
// ✅ Timer event ile
public class TimerEventNode
{
    public DateTime? ScheduledTime { get; set; }
    public TimeSpan? Duration { get; set; }
    public string CronExpression { get; set; } // "0 0 0 * * ?" (Her gün gece yarısı)
}

// Workflow'da:
// Start → Approve (Pending) → [Timer: 3 gün] → Auto Reject → End
```

**Nasıl Çalışır:**
1. Workflow approve node'a gelir → Pending olur
2. Timer event kaydedilir: "3 gün sonra tetikle"
3. 3 gün sonra timer event tetiklenir
4. Workflow devam eder → Auto Reject → End

---

### **Senaryo 2: Message Event (Dış Sistem Entegrasyonu)**

**Problem:**
```
Form gönderildi → Dış ERP sisteminden stok kontrolü yapılsın 
→ ERP'den cevap gelince workflow devam etsin
```

**Mevcut Yapınız (Çalışmaz):**
```csharp
// ❌ Senkron bekliyor, timeout olur
var stockCheck = await erpService.CheckStock(productId);
// ERP yavaşsa workflow takılır
```

**Event-Driven Çözüm:**
```csharp
// ✅ Message event ile
public class MessageEventNode
{
    public string MessageName { get; set; } // "StockCheckResponse"
    public string CorrelationKey { get; set; } // Workflow instance ID
}

// Workflow'da:
// Start → Send Message to ERP → [Wait for Message] → Process Response → End
```

**Nasıl Çalışır:**
1. Workflow ERP'ye mesaj gönderir
2. Workflow message event'te bekler (Pending)
3. ERP işlemi tamamlar, mesaj gönderir
4. Message event tetiklenir
5. Workflow devam eder

---

### **Senaryo 3: Signal Event (Workflow'lar Arası İletişim)**

**Problem:**
```
Workflow A başladı → Workflow B'yi başlatır 
→ Workflow B bitince Workflow A'ya haber verir
```

**Mevcut Yapınız (Çalışmaz):**
```csharp
// ❌ Workflow'lar birbirini beklemez
var workflowB = await StartWorkflowB();
// Workflow A devam eder, B'yi beklemez
```

**Event-Driven Çözüm:**
```csharp
// ✅ Signal event ile
public class SignalEventNode
{
    public string SignalName { get; set; } // "WorkflowBCompleted"
}

// Workflow A:
// Start → Start Workflow B → [Wait for Signal] → Continue → End

// Workflow B:
// Start → Process → End → [Send Signal: "WorkflowBCompleted"]
```

---

## 🔧 Mevcut Yapınıza Event-Driven Özellikler Ekleme

### **1. Timer Event Desteği**

```csharp
// WorkFlowEngine.cs'e ekle
public class TimerEventNode
{
    public string NodeId { get; set; }
    public DateTime? ScheduledTime { get; set; }
    public TimeSpan? Duration { get; set; }
    public string CronExpression { get; set; }
    public Guid WorkflowInstanceId { get; set; }
    public string NextNodeId { get; set; }
}

public class TimerEventService
{
    private readonly IWorkFlowService _workFlowService;
    private readonly Timer _timer;
    
    public TimerEventService(IWorkFlowService workFlowService)
    {
        _workFlowService = workFlowService;
        _timer = new Timer(CheckTimers, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
    }
    
    public void ScheduleTimer(TimerEventNode timerNode)
    {
        // Timer'ı veritabanına kaydet
        _timerRepository.Add(timerNode);
    }
    
    private async void CheckTimers(object state)
    {
        var expiredTimers = await _timerRepository.GetExpiredTimers();
        
        foreach (var timer in expiredTimers)
        {
            // Timer event'i tetikle
            await TriggerTimerEvent(timer);
        }
    }
    
    private async Task TriggerTimerEvent(TimerEventNode timer)
    {
        var workflowHead = await _workFlowService.GetByIdGuidAsync(timer.WorkflowInstanceId);
        var workflow = DeserializeWorkflow(workflowHead.WorkFlowDefinationJson);
        
        // Timer event node'unu completed yap ve devam et
        workflow.ContinueFromTimerEvent(timer.NodeId, timer.NextNodeId);
    }
}
```

**Workflow'da Kullanım:**
```csharp
if (result.NodeType == "timerEventNode")
{
    string nextNode = ExecuteTimerEventNode(currentNode, result, Parameter);
    // Timer kaydedilir, workflow pending olur
    // Timer tetiklendiğinde Continue çağrılır
}
```

---

### **2. Message Event Desteği**

```csharp
// WorkFlowController.cs'e ekle
[HttpPost("ReceiveMessage")]
public async Task<ActionResult> ReceiveMessage(WorkflowMessageDto message)
{
    // Dış sistemden gelen mesajı al
    var waitingInstances = await _workFlowService
        .GetWaitingForMessage(message.MessageName, message.CorrelationKey);
    
    foreach (var instance in waitingInstances)
    {
        // Message event'i tetikle
        await TriggerMessageEvent(instance, message);
    }
    
    return Ok();
}

private async Task TriggerMessageEvent(WorkflowHead instance, WorkflowMessageDto message)
{
    var workflow = DeserializeWorkflow(instance.WorkFlowDefinationJson);
    
    // Message event node'unu completed yap ve devam et
    workflow.ContinueFromMessageEvent(
        messageEventNodeId, 
        messageEventNextNodeId, 
        message.Data
    );
}
```

**Dış Sistem Entegrasyonu:**
```csharp
// ERP sisteminden örnek
public class ERPIntegrationService
{
    public async Task ProcessOrder(string orderId)
    {
        // İşlemi yap
        var result = await ProcessOrderInternal(orderId);
        
        // Workflow'a mesaj gönder
        await _httpClient.PostAsync("/api/WorkFlow/ReceiveMessage", new
        {
            MessageName = "OrderProcessed",
            CorrelationKey = orderId,
            Data = result
        });
    }
}
```

---

### **3. Event Bus Entegrasyonu**

```csharp
// Event-driven mimari için
public interface IWorkflowEventBus
{
    Task PublishAsync<T>(T @event) where T : IWorkflowEvent;
    void Subscribe<T>(Func<T, Task> handler) where T : IWorkflowEvent;
}

public class WorkflowEventBus : IWorkflowEventBus
{
    private readonly Dictionary<Type, List<Func<object, Task>>> _handlers = new();
    
    public async Task PublishAsync<T>(T @event) where T : IWorkflowEvent
    {
        var eventType = typeof(T);
        if (_handlers.ContainsKey(eventType))
        {
            var handlers = _handlers[eventType];
            await Task.WhenAll(handlers.Select(h => h(@event)));
        }
    }
    
    public void Subscribe<T>(Func<T, Task> handler) where T : IWorkflowEvent
    {
        var eventType = typeof(T);
        if (!_handlers.ContainsKey(eventType))
        {
            _handlers[eventType] = new List<Func<object, Task>>();
        }
        
        _handlers[eventType].Add(async obj => await handler((T)obj));
    }
}

// Event tipleri
public interface IWorkflowEvent
{
    Guid WorkflowInstanceId { get; }
    DateTime Timestamp { get; }
}

public class TimerEvent : IWorkflowEvent
{
    public Guid WorkflowInstanceId { get; set; }
    public DateTime Timestamp { get; set; }
    public string NodeId { get; set; }
}

public class MessageEvent : IWorkflowEvent
{
    public Guid WorkflowInstanceId { get; set; }
    public DateTime Timestamp { get; set; }
    public string MessageName { get; set; }
    public object Data { get; set; }
}
```

---

## 📊 Karşılaştırma Tablosu

| Özellik | Request-Driven (Mevcut) | Event-Driven (Eksik) |
|---------|------------------------|----------------------|
| **Tetikleme** | Kullanıcı aksiyonu | Dış event'ler |
| **Zamanlama** | Anında | Zamanlanabilir |
| **Bekleme** | Senkron | Asenkron |
| **Entegrasyon** | Sınırlı | Geniş |
| **Timeout** | Yok | Var |
| **Kullanım** | Basit workflow'lar | Karmaşık süreçler |

---

## 🎯 Örnek: Tam Event-Driven Workflow

```json
{
  "nodes": [
    {
      "id": "start",
      "type": "startNode"
    },
    {
      "id": "form",
      "type": "formNode",
      "data": {
        "name": "Sipariş Formu"
      }
    },
    {
      "id": "sendToERP",
      "type": "scriptNode",
      "data": {
        "script": "sendMessageToERP(orderData)"
      }
    },
    {
      "id": "waitForERP",
      "type": "messageEventNode",
      "data": {
        "messageName": "ERPResponse",
        "timeout": "PT1H" // 1 saat timeout
      }
    },
    {
      "id": "checkStock",
      "type": "scriptNode",
      "data": {
        "script": "if (erpResponse.stockAvailable) return true; else return false;"
      }
    },
    {
      "id": "approve",
      "type": "approverNode"
    },
    {
      "id": "waitForApproval",
      "type": "timerEventNode",
      "data": {
        "duration": "P3D" // 3 gün
      }
    },
    {
      "id": "autoReject",
      "type": "scriptNode",
      "data": {
        "script": "rejectOrder()"
      }
    },
    {
      "id": "end",
      "type": "stopNode"
    }
  ],
  "edges": [
    { "source": "start", "target": "form" },
    { "source": "form", "target": "sendToERP" },
    { "source": "sendToERP", "target": "waitForERP" },
    { "source": "waitForERP", "target": "checkStock" },
    { "source": "checkStock", "target": "approve", "sourceHandle": "yes" },
    { "source": "approve", "target": "waitForApproval" },
    { "source": "waitForApproval", "target": "autoReject" },
    { "source": "autoReject", "target": "end" }
  ]
}
```

**Akış:**
1. ✅ Form doldurulur (Request-Driven)
2. ✅ ERP'ye mesaj gönderilir
3. ⏳ **Message Event'te bekler** (Event-Driven)
4. ✅ ERP'den cevap gelir → Devam eder
5. ✅ Onay bekler
6. ⏳ **Timer Event'te bekler** (Event-Driven)
7. ✅ 3 gün sonra otomatik reddedilir

---

## 💡 Sonuç

**Event-Driven Workflow'lar:**
- Dış sistemlerden tetiklenebilir
- Zamanlanmış işlemler yapabilir
- Asenkron çalışır
- Sistemler arası entegrasyon sağlar
- Karmaşık iş süreçlerini destekler

**Mevcut yapınız:** Request-Driven (Kullanıcı aksiyonlarına bağlı)
**Eksik olan:** Event-Driven özellikler (Timer, Message, Signal events)

