# Workflow Engine - Best Practice Analizi

## 🔴 Kritik Sorunlar

### 1. **Single Responsibility Principle İhlali**
- `Workflow` class'ı çok fazla sorumluluk taşıyor:
  - Node execution
  - State management
  - Edge finding
  - Business logic
- `WorkFlowExecute` class'ı hem execution hem de email gönderme yapıyor

### 2. **Magic Strings**
```csharp
if (result.NodeType == "stopNode")  // ❌ Magic string
if (result.NodeType == "sqlConditionNode")  // ❌ Magic string
if (result.NodeType == "approverNode")  // ❌ Magic string
```
**Çözüm:** Constants veya Enum kullanılmalı

### 3. **Code Duplication**
- Start ve Continue metodlarında alert bilgilerini ekleme kodu tekrar ediyor
- Node type kontrolü her seferinde if-else ile yapılıyor

### 4. **Error Handling Eksik**
```csharp
WorkflowNode currentNode = Nodes.Find(node => node.Id == nodeId);
// ❌ currentNode null olabilir, kontrol yok
```

### 5. **Hardcoded Values**
```csharp
string senderEmail = "support@formneo.com";  // ❌ Configuration'dan gelmeli
string senderPassword = "Sifre2634@!!";  // ❌ Güvenlik riski!
```

### 6. **State Management**
- `_Action` global state olarak kullanılıyor, thread-safe değil
- `_workFlowItems` listesi mutable ve kontrolsüz

### 7. **Dependency Injection Eksik**
```csharp
Utils utils = new Utils();  // ❌ DI kullanılmalı
PositionCreateRunner runner = new PositionCreateRunner();  // ❌ DI kullanılmalı
```

## 🟡 İyileştirme Gereken Alanlar

### 1. **Strategy Pattern Kullanılmalı**
Her node type için ayrı strategy class'ı:
```csharp
public interface INodeExecutor
{
    string Execute(WorkflowNode node, WorkflowItem item, string parameter);
}

public class FormNodeExecutor : INodeExecutor { }
public class AlertNodeExecutor : INodeExecutor { }
public class ApproverNodeExecutor : INodeExecutor { }
```

### 2. **Null Safety**
```csharp
// ❌ Mevcut
WorkflowNode currentNode = Nodes.Find(node => node.Id == nodeId);

// ✅ Önerilen
WorkflowNode currentNode = Nodes?.FirstOrDefault(node => node.Id == nodeId)
    ?? throw new WorkflowException($"Node not found: {nodeId}");
```

### 3. **Configuration Management**
```csharp
// ❌ Mevcut
string senderEmail = "support@formneo.com";

// ✅ Önerilen
public class EmailSettings
{
    public string SenderEmail { get; set; }
    public string SenderPassword { get; set; }
}
```

### 4. **Separation of Concerns**
- Email gönderme işlemi ayrı bir service'e taşınmalı
- Workflow execution logic'i ayrı bir service'e taşınmalı

## ✅ İyi Olan Kısımlar

1. ✅ DTO kullanımı iyi
2. ✅ AutoMapper kullanımı
3. ✅ Async/await pattern doğru kullanılmış
4. ✅ Controller'da authorization var

## 📋 Önerilen Refactoring

### 1. Node Type Constants
```csharp
public static class NodeTypes
{
    public const string StartNode = "startNode";
    public const string FormNode = "formNode";
    public const string AlertNode = "alertNode";
    public const string ApproverNode = "approverNode";
    public const string StopNode = "stopNode";
    public const string SqlConditionNode = "sqlConditionNode";
    public const string EmailNode = "EmailNode";
}
```

### 2. Strategy Pattern Implementation
```csharp
public interface INodeExecutor
{
    bool CanExecute(string nodeType);
    Task<NodeExecutionResult> ExecuteAsync(
        WorkflowNode node, 
        WorkflowItem item, 
        WorkflowContext context);
}

public class NodeExecutionResult
{
    public string NextNodeId { get; set; }
    public WorkflowStatus Status { get; set; }
    public bool ShouldPause { get; set; }
    public object AdditionalData { get; set; }
}
```

### 3. Workflow Context
```csharp
public class WorkflowContext
{
    public Guid HeadId { get; set; }
    public string ApiSendUser { get; set; }
    public string PayloadJson { get; set; }
    public string Action { get; set; }
    public WorkFlowParameters Parameters { get; set; }
    public List<WorkflowItem> WorkflowItems { get; set; }
}
```

### 4. Error Handling
```csharp
public class WorkflowException : Exception
{
    public string NodeId { get; }
    public WorkflowException(string message, string nodeId) 
        : base(message) 
    {
        NodeId = nodeId;
    }
}
```

## 🎯 Öncelik Sırası

1. **Yüksek Öncelik:**
   - Magic strings'i constants'a çevir
   - Null safety ekle
   - Hardcoded credentials'ı configuration'a taşı

2. **Orta Öncelik:**
   - Strategy pattern implementasyonu
   - Code duplication'ı azalt
   - Error handling ekle

3. **Düşük Öncelik:**
   - Unit test yazılabilirliğini artır
   - Logging ekle
   - Performance optimizasyonu

