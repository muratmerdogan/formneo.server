# Workflow Sistemi - Komple Analiz Dokümantasyonu

## 📋 İçindekiler
1. [Genel Bakış](#genel-bakış)
2. [Mimari Yapı](#mimari-yapı)
3. [Node Tipleri](#node-tipleri)
4. [Workflow Akışı](#workflow-akışı)
5. [Veri Yapıları](#veri-yapıları)
6. [API Endpoint'leri](#api-endpointleri)
7. [İş Mantığı Detayları](#iş-mantığı-detayları)

---

## 🎯 Genel Bakış

Formneo Workflow sistemi, iş süreçlerini yönetmek için kullanılan bir **state machine** tabanlı workflow motorudur. Sistem şu ana bileşenlerden oluşur:

- **WorkFlowEngine**: Node'ları execute eden ve workflow akışını yöneten motor
- **WorkFlowExecute**: Start ve Continue işlemlerini yöneten ana orchestrator
- **WorkFlowController**: API endpoint'lerini sağlayan controller

### Temel Prensipler
- **Buton Bazlı Sistem**: Artık Input ile "yes/no" mantığı yok, form üzerindeki butonların action kodları kullanılıyor
- **FormTask ve UserTask**: İki tip görev sistemi var
  - **FormTask**: FormTaskNode'dan gelir, kullanıcı formu doldurur
  - **UserTask**: ApproverNode'dan gelir, kullanıcı onay/red işlemi yapar
- **FormInstance**: Her workflow için tek bir FormInstance tutulur (güncel form verisi)

---

## 🏗️ Mimari Yapı

### Katmanlar

```
┌─────────────────────────────────────┐
│   WorkFlowController (API Layer)    │
│   - Start                           │
│   - Continue                        │
│   - GetMyTasks                      │
│   - GetTaskDetailByWorkflowItemId   │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   WorkFlowExecute (Orchestrator)   │
│   - StartAsync                      │
│   - Workflow başlatma/devam ettirme │
│   - FormInstance yönetimi           │
└──────────────┬──────────────────────┘
               │
┌──────────────▼──────────────────────┐
│   WorkFlowEngine (Execution Engine) │
│   - Start                            │
│   - Continue                         │
│   - ExecuteNode                      │
│   - Node-specific execute metodları │
└──────────────────────────────────────┘
```

---

## 🔵 Node Tipleri

### 1. **startNode**
- **Amaç**: Workflow'un başlangıç noktası
- **Davranış**: 
  - Workflow başlatıldığında otomatik olarak execute edilir
  - Sonraki node'a geçer (genellikle formNode veya formTaskNode)
- **Durum**: Her zaman Completed

### 2. **formNode**
- **Amaç**: Form gösterimi ve kullanıcı etkileşimi
- **Davranış**:
  - **Action yoksa**: Pending durumuna geçer, kullanıcı formu doldurup butona basana kadar bekler
  - **Action varsa**: Completed olur ve action'a göre edge bulup sonraki node'a geçer
- **Action Mantığı**:
  - Start'ta action ile başlatıldıysa → action'a göre edge bul ve devam et
  - Continue'dan geliyorsa → parameter'dan action'ı al
- **Örnek Action'lar**: `SAVE`, `APPROVE`, `REJECT`, `SUBMIT`

### 3. **formTaskNode**
- **Amaç**: FormTask görevi oluşturur (kullanıcıya form doldurma görevi verir)
- **Davranış**:
  - **Parameter boşsa**: 
    - FormItem oluşturulur (Pending durumunda)
    - WorkflowItem Pending olur
    - Workflow durur, kullanıcı formu doldurana kadar bekler
  - **Parameter varsa** (form doldurulduysa):
    - FormItem Completed olur
    - WorkflowItem Completed olur
    - Sonraki node'a geçer
- **FormItem Oluşturma**:
  - FormId: NodeData'dan alınır (`formId` veya `code` property'sinden)
  - FormDesign: Boş başlar, sonra Form tablosundan alınır
  - FormTaskMessage: NodeData'dan alınır (`message`, `formTaskMessage` veya `description`)

### 4. **approverNode** (UserTask)
- **Amaç**: Onay görevi oluşturur
- **Davranış**:
  - **Parameter boşsa**:
    - ApproveItem oluşturulur
    - WorkflowItem Pending olur
    - Workflow durur, onaylayıcı onay/red yapana kadar bekler
  - **Parameter varsa** (`yes`/`no` veya action):
    - ApproveItem durumu güncellenir
    - WorkflowItem Completed olur
    - Sonraki node'a geçer
- **ApproveItem Oluşturma**:
  - `isManager == true` ise: Kullanıcının manager'ı bulunur
  - `isManager == false` ise: NodeData'daki `code` property'sinden onaylayıcı alınır

### 5. **sqlConditionNode**
- **Amaç**: SQL sorgusu ile koşul kontrolü
- **Davranış**:
  - JsonLogic kullanarak SQL sorgusu çalıştırılır
  - Sonuç `true` ise → `yes` edge'ine geçer
  - Sonuç `false` ise → `no` edge'ine geçer
- **Payload**: `_payloadJson` kullanılır (form verileri)

### 6. **scriptNode**
- **Amaç**: JavaScript kodu ile koşul kontrolü
- **Davranış**:
  - Jint engine kullanarak JavaScript kodu çalıştırılır
  - Script'e `previousNodes` ve `workflow` context'i verilir
  - Sonuç `true` ise → `yes` edge'ine geçer
  - Sonuç `false` ise → `no` edge'ine geçer
- **PreviousNodes Yapısı**:
  ```javascript
  previousNodes.PERSONELTALEP.uuk80m63ix3  // Form adı ve field ID'si
  workflow.instanceId                       // Workflow instance ID
  workflow.currentStep                      // Mevcut node ID
  ```

### 7. **alertNode**
- **Amaç**: Hata/uyarı mesajları göstermek
- **Davranış**:
  - **Start'ta**:
    - Error/Warning → Pending olur, rollback yapılır
    - Success/Info → Atlanır, sonraki node'a geçer
  - **Continue'da**:
    - Completed olur, sonraki node'a geçer
- **Rollback**: AlertNode'a gelince workflow kaydedilmez (Id = Guid.Empty)

### 8. **stopNode**
- **Amaç**: Workflow'u sonlandırır
- **Davranış**:
  - `stoptype.code == "FINISH"` → Workflow Completed olur
  - Diğer durumlar → Workflow Draft olur
- **Ticket İşlemleri**: StopNode'a gelince Ticket durumu güncellenir

---

## 🔄 Workflow Akışı

### Start Akışı (Yeni Workflow)

```
1. Start API çağrılır
   ↓
2. WorkFlowExecute.StartAsync çağrılır
   ↓
3. WorkflowDefinition'dan workflow JSON'u parse edilir
   ↓
4. Workflow.Start() çağrılır
   ↓
5. startNode bulunur ve ExecuteNode() çağrılır
   ↓
6. Node tipine göre execute metodu çağrılır
   ↓
7. Node Completed olursa sonraki node'a geçer
   ↓
8. Pending node'a gelirse workflow durur
   ↓
9. WorkflowHead ve WorkflowItems kaydedilir
   ↓
10. FormTaskNode varsa FormInstance oluşturulur
```

### Continue Akışı (Var Olan Workflow)

```
1. Continue API çağrılır
   ↓
2. WorkFlowExecute.StartAsync çağrılır (WorkFlowId dolu)
   ↓
3. Mevcut WorkflowHead ve WorkflowItems yüklenir
   ↓
4. Devam edilecek node bulunur (NodeId'den)
   ↓
5. Workflow.Continue() çağrılır
   ↓
6. ExecuteNode() çağrılır (mevcut workflowItem ile)
   ↓
7. Node tipine göre execute metodu çağrılır
   ↓
8. FormTaskNode ise:
   - FormInstance güncellenir (FormData ile)
   - FormItem güncellenir
   ↓
9. WorkflowHead ve WorkflowItems güncellenir
```

### Node Execution Mantığı

```csharp
ExecuteNode(nodeId, parameter, workflowItem)
{
    // 1. Node bulunur
    currentNode = Nodes.Find(node => node.Id == nodeId)
    
    // 2. WorkflowItem oluşturulur veya mevcut kullanılır
    if (workflowItem != null)
        result = workflowItem
    else
        result = currentNode.Execute()
    
    // 3. Node tipine göre execute metodu çağrılır
    switch (result.NodeType) {
        case "formNode": ExecuteFormNode(...)
        case "formTaskNode": ExecuteFormTaskNode(...)
        case "approverNode": ExecuteApprove(...)
        case "scriptNode": ExecuteScriptNode(...)
        case "sqlConditionNode": ExecuteSqlConditionNode(...)
        case "alertNode": ExecuteAlertNode(...)
        case "stopNode": ExecuteStopNode(...)
    }
    
    // 4. Sonraki node bulunur ve execute edilir
    nextNode = FindLinkForPort(currentNode.Id, parameter)
    if (nextNode != null)
        ExecuteNode(nextNode)
}
```

---

## 📊 Veri Yapıları

### WorkflowHead
- **Amaç**: Workflow instance'ını temsil eder
- **Önemli Property'ler**:
  - `Id`: Workflow instance ID
  - `WorkFlowDefinationId`: Workflow tanımı ID
  - `WorkFlowDefinationJson`: Workflow JSON tanımı
  - `workFlowStatus`: Workflow durumu (Pending, InProgress, Completed)
  - `FormId`: Ana form ID (FormTaskNode'dan alınır)
  - `workflowItems`: WorkflowItem listesi

### WorkflowItem
- **Amaç**: Bir node'un execution instance'ını temsil eder
- **Önemli Property'ler**:
  - `Id`: WorkflowItem ID
  - `NodeId`: Node ID (workflow definition'dan)
  - `NodeType`: Node tipi (formNode, formTaskNode, vb.)
  - `NodeName`: Node adı
  - `workFlowNodeStatus`: Node durumu (Pending, Completed)
  - `formItems`: FormItem listesi (FormTaskNode için)
  - `approveItems`: ApproveItem listesi (ApproverNode için)

### FormItems
- **Amaç**: FormTask görevini temsil eder
- **Önemli Property'ler**:
  - `Id`: FormItem ID
  - `WorkflowItemId`: İlişkili WorkflowItem ID
  - `FormId`: Form ID
  - `FormDesign`: Form tasarımı (JSON)
  - `FormData`: Form verileri (JSON)
  - `FormUser`: Formu dolduran kullanıcı
  - `FormTaskMessage`: Görev mesajı
  - `FormItemStatus`: Durum (Pending, Completed, Rejected)

### ApproveItems
- **Amaç**: Onay görevini temsil eder
- **Önemli Property'ler**:
  - `Id`: ApproveItem ID
  - `WorkflowItemId`: İlişkili WorkflowItem ID
  - `ApproveUser`: Onaylayıcı kullanıcı
  - `ApproverStatus`: Durum (Pending, Approve, Reject)
  - `ApprovedUser_Runtime`: Onaylayan kullanıcı (runtime)
  - `ApprovedUser_RuntimeNote`: Onay notu

### FormInstance
- **Amaç**: Workflow için güncel form verisini tutar
- **Önemli Property'ler**:
  - `Id`: FormInstance ID
  - `WorkflowHeadId`: İlişkili WorkflowHead ID
  - `FormId`: Form ID
  - `FormDesign`: Form tasarımı (JSON)
  - `FormData`: Form verileri (JSON) - **HER ZAMAN GÜNCEL**
- **Not**: Her workflow için tek bir FormInstance tutulur, her FormTaskNode tamamlandığında güncellenir

---

## 🌐 API Endpoint'leri

### 1. **POST /api/WorkFlow/Start**
- **Amaç**: Yeni workflow başlatır
- **Request**: `WorkFlowStartApiDto`
  ```json
  {
    "DefinationId": "guid",
    "UserName": "string",
    "WorkFlowInfo": "string",
    "Action": "SAVE|APPROVE|...",
    "FormData": "json string",
    "Note": "string"
  }
  ```
- **Response**: `WorkFlowHeadDtoResultStartOrContinue`
- **İşlem**:
  1. WorkflowDefinition yüklenir
  2. Workflow.Start() çağrılır
  3. WorkflowHead ve WorkflowItems kaydedilir
  4. FormTaskNode varsa FormInstance oluşturulur

### 2. **POST /api/WorkFlow/Contiune**
- **Amaç**: Var olan workflow'a devam eder
- **Request**: `WorkFlowContiuneApiDto`
  ```json
  {
    "workFlowItemId": "guid",
    "UserName": "string",
    "Action": "SAVE|APPROVE|...",
    "FormData": "json string",
    "Note": "string",
    "NumberManDay": "string"
  }
  ```
- **Response**: `WorkFlowHeadDtoResultStartOrContinue`
- **İşlem**:
  1. WorkflowItem ve WorkflowHead yüklenir
  2. Workflow.Continue() çağrılır
  3. FormTaskNode ise FormInstance güncellenir
  4. WorkflowHead ve WorkflowItems güncellenir

### 3. **GET /api/WorkFlow/my-tasks**
- **Amaç**: Kullanıcının pending görevlerini getirir
- **Response**: `MyTasksDto`
  ```json
  {
    "FormTasks": [...],  // FormTaskNode'dan gelen görevler
    "UserTasks": [...],  // ApproverNode'dan gelen görevler
    "TotalCount": 10
  }
  ```

### 4. **GET /api/WorkFlow/workflowitem/{workflowItemId}/task-detail**
- **Amaç**: Görev detayını getirir (FormTask veya UserTask)
- **Response**: `TaskFormDto`
  - FormTask için: FormDesign, FormData, FormId
  - UserTask için: FormDesign, FormData, ApproveItem bilgileri
- **Önemli**: FormData her zaman FormInstance'dan alınır (güncel veri)

---

## 🔧 İş Mantığı Detayları

### FormTaskNode İş Mantığı

#### Start'ta:
1. FormTaskNode'a gelince FormItem oluşturulur (Pending)
2. FormId NodeData'dan alınır
3. FormDesign boşsa Form tablosundan alınır
4. FormInstance oluşturulur (FormData ile)

#### Continue'da:
1. FormTaskNode'dan geliyorsa:
   - FormItem bulunur ve güncellenir
   - FormInstance güncellenir (yeni FormData ile)
   - FormDesign güncellenebilir

### ApproverNode İş Mantığı

#### Start'ta:
1. ApproverNode'a gelince ApproveItem oluşturulur
2. `isManager == true` ise manager bulunur
3. `isManager == false` ise NodeData.code kullanılır
4. WorkflowItem Pending olur

#### Continue'da:
1. ApproverNode'dan geliyorsa:
   - ApproveItem node'dan bulunur (kullanıcıya göre)
   - Action'a göre durum güncellenir (APPROVE/REJECT)
   - WorkflowItem Completed olur
   - Sonraki node'a geçer

**NOT**: ApproveItem işlemleri Start ve Continue'da yapılmaz, sadece WorkFlowEngine'deki ExecuteApprove metodunda yapılır.

### FormInstance Yönetimi

- **Tek Kaynak Prensibi**: Her workflow için tek bir FormInstance tutulur
- **Güncellenme**: FormTaskNode tamamlandığında FormInstance güncellenir
- **FormData**: Her zaman güncel form verisini tutar
- **FormDesign**: FormItem'dan veya Form tablosundan alınır

### Action Mantığı

- **Buton Bazlı**: Form üzerindeki butonların action kodları kullanılır
- **Action Örnekleri**: `SAVE`, `APPROVE`, `REJECT`, `SUBMIT`
- **Edge Bulma**: Action'a göre `SourceHandle` ile edge bulunur
- **Fallback**: Action'a göre edge bulunamazsa ilk edge kullanılır

### Edge ve Port Mantığı

- **SourceHandle**: Edge'in kaynak port'u (action kodu)
- **TargetHandle**: Edge'in hedef port'u
- **FindLinkForPort**: Port'a göre edge bulur (case-insensitive)
- **Örnek**: `formNode` → `SAVE` action → `SourceHandle="SAVE"` edge'i bulunur

### Mail Gönderme

- **Pending ApproveItem**: Onayınıza sunuldu maili gönderilir
- **Reject ApproveItem**: Reddedildi maili gönderilir
- **Completed Workflow**: Onay süreci tamamlandı maili gönderilir

---

## 🎨 Örnek Workflow Senaryoları

### Senaryo 1: Basit Form Gönderimi

```
startNode → formNode → stopNode
```

1. Start: formNode'a gelir, Pending olur
2. Kullanıcı formu doldurur, SAVE butonuna basar
3. Continue: formNode Completed olur, stopNode'a geçer
4. Workflow Completed olur

### Senaryo 2: FormTask ile Onay Süreci

```
startNode → formTaskNode → approverNode → stopNode
```

1. Start: formTaskNode'a gelir, FormItem oluşturulur (Pending)
2. Kullanıcı formu doldurur, SUBMIT butonuna basar
3. Continue: FormTaskNode Completed olur, approverNode'a geçer
4. ApproverNode: ApproveItem oluşturulur (Pending)
5. Onaylayıcı onaylar, APPROVE action gönderir
6. Continue: ApproverNode Completed olur, stopNode'a geçer
7. Workflow Completed olur

### Senaryo 3: Koşullu Akış

```
startNode → formNode → scriptNode → [yes/no] → approverNode → stopNode
```

1. Start: formNode'a gelir, Pending olur
2. Kullanıcı formu doldurur, SUBMIT butonuna basar
3. Continue: formNode Completed olur, scriptNode'a geçer
4. ScriptNode: JavaScript kodu çalıştırılır
5. Sonuç `true` ise → `yes` edge → approverNode
6. Sonuç `false` ise → `no` edge → stopNode (direkt tamamlanır)

---

## ⚠️ Önemli Notlar

1. **ApproverItem İşlemleri**: Start ve Continue'da ApproverItem işlemleri yapılmaz, sadece WorkFlowEngine'deki ExecuteApprove metodunda yapılır.

2. **FormInstance**: Her workflow için tek bir FormInstance tutulur ve her FormTaskNode tamamlandığında güncellenir.

3. **Action Mantığı**: Artık Input ile "yes/no" mantığı yok, buton bazlı sistem kullanılıyor.

4. **AlertNode Rollback**: Error/Warning tipindeki alertNode'a gelince workflow kaydedilmez (rollback).

5. **FormData Kaynağı**: GetTaskDetailByWorkflowItemId metodunda FormData her zaman FormInstance'dan alınır (güncel veri).

6. **PreviousNodes**: ScriptNode içinde önceki formNode'ların verilerine `previousNodes.FormName.FieldId` şeklinde erişilebilir.

---

## 📝 Sonuç

Formneo Workflow sistemi, esnek ve güçlü bir iş süreci yönetim sistemi sunar. Node tabanlı yapısı sayesinde karmaşık iş akışları kolayca modellenebilir ve yönetilebilir. FormTask ve UserTask ayrımı sayesinde farklı görev tipleri net bir şekilde yönetilir.



