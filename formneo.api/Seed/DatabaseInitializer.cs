using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using vesa.core.Models;
using vesa.repository;
using vesa.core.Models.CRM;
using vesa.core.Models.Lookup;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace vesa.api.Seed
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserApp>>();

            await context.Database.EnsureCreatedAsync();
            // Lookup seed: Modules + CRM kategorileri ve örnek itemlar
            if (!await context.LookupModules.AnyAsync())
            {
                var crmModule = new LookupModule { Id = Guid.NewGuid(), Key = "CRM", Name = "CRM", IsTenantScoped = false };
                await context.LookupModules.AddAsync(crmModule);
                await context.SaveChangesAsync();

                var catCustomerType = new LookupCategory { Id = Guid.NewGuid(), Key = "CustomerType", Description = "Müşteri Tipi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catCustomerStatus = new LookupCategory { Id = Guid.NewGuid(), Key = "CustomerStatus", Description = "Müşteri Durumu", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catCustomerCategory = new LookupCategory { Id = Guid.NewGuid(), Key = "CustomerCategory", Description = "Müşteri Kategorisi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catSectorType = new LookupCategory { Id = Guid.NewGuid(), Key = "SectorType", Description = "Sektör Tipi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catCompanyType = new LookupCategory { Id = Guid.NewGuid(), Key = "CompanyType", Description = "Firma Türü", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catActivityArea = new LookupCategory { Id = Guid.NewGuid(), Key = "ActivityArea", Description = "Faaliyet Alanı", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catContactRole = new LookupCategory { Id = Guid.NewGuid(), Key = "ContactRole", Description = "Yetkili Rolü", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catOpportunityStage = new LookupCategory { Id = Guid.NewGuid(), Key = "OpportunityStage", Description = "Fırsat Aşaması", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catActivityType = new LookupCategory { Id = Guid.NewGuid(), Key = "ActivityType", Description = "Aktivite Tipi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catLeadSource = new LookupCategory { Id = Guid.NewGuid(), Key = "LeadSource", Description = "Lead Kaynağı", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catPriority = new LookupCategory { Id = Guid.NewGuid(), Key = "Priority", Description = "Öncelik", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catRegion = new LookupCategory { Id = Guid.NewGuid(), Key = "Region", Description = "Bölge", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catInteractionChannel = new LookupCategory { Id = Guid.NewGuid(), Key = "InteractionChannel", Description = "İletişim Kanalı", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catPaymentMethod = new LookupCategory { Id = Guid.NewGuid(), Key = "PaymentMethod", Description = "Ödeme Yöntemi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catPaymentTerm = new LookupCategory { Id = Guid.NewGuid(), Key = "PaymentTerm", Description = "Vade", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catCurrency = new LookupCategory { Id = Guid.NewGuid(), Key = "Currency", Description = "Para Birimi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catLossReason = new LookupCategory { Id = Guid.NewGuid(), Key = "LossReason", Description = "Kayıp Nedeni", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catWinReason = new LookupCategory { Id = Guid.NewGuid(), Key = "WinReason", Description = "Kazanma Nedeni", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catLeadStatus = new LookupCategory { Id = Guid.NewGuid(), Key = "LeadStatus", Description = "Lead Durumu", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catLeadDisqualifyReason = new LookupCategory { Id = Guid.NewGuid(), Key = "LeadDisqualifyReason", Description = "Lead Diskalifiye Nedeni", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catContactTitle = new LookupCategory { Id = Guid.NewGuid(), Key = "ContactTitle", Description = "Unvan", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catCommunicationPreference = new LookupCategory { Id = Guid.NewGuid(), Key = "CommunicationPreference", Description = "İletişim Tercihi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catGDPRConsent = new LookupCategory { Id = Guid.NewGuid(), Key = "GDPRConsent", Description = "KVKK/Consent", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catDoNotContactReason = new LookupCategory { Id = Guid.NewGuid(), Key = "DoNotContactReason", Description = "İletişim Engelleme Nedeni", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catSLAPlan = new LookupCategory { Id = Guid.NewGuid(), Key = "SLAPlan", Description = "SLA Planı", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catCaseStatus = new LookupCategory { Id = Guid.NewGuid(), Key = "CaseStatus", Description = "Vaka Durumu", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catCaseOrigin = new LookupCategory { Id = Guid.NewGuid(), Key = "CaseOrigin", Description = "Vaka Kaynağı", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catOpportunityProbabilityBand = new LookupCategory { Id = Guid.NewGuid(), Key = "OpportunityProbabilityBand", Description = "Fırsat Olasılık Bandı", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catCampaignType = new LookupCategory { Id = Guid.NewGuid(), Key = "CampaignType", Description = "Kampanya Tipi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catCampaignStatus = new LookupCategory { Id = Guid.NewGuid(), Key = "CampaignStatus", Description = "Kampanya Durumu", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catMarketSegment = new LookupCategory { Id = Guid.NewGuid(), Key = "MarketSegment", Description = "Pazar Segmenti", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catAccountOwnership = new LookupCategory { Id = Guid.NewGuid(), Key = "AccountOwnership", Description = "Hesap Sahipliği", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catRelationshipType = new LookupCategory { Id = Guid.NewGuid(), Key = "RelationshipType", Description = "İlişki Türü", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catRenewalLikelihood = new LookupCategory { Id = Guid.NewGuid(), Key = "RenewalLikelihood", Description = "Yenileme Olasılığı", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catSatisfactionLevel = new LookupCategory { Id = Guid.NewGuid(), Key = "SatisfactionLevel", Description = "Memnuniyet Düzeyi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catLanguage = new LookupCategory { Id = Guid.NewGuid(), Key = "Language", Description = "Dil", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catIncoterm = new LookupCategory { Id = Guid.NewGuid(), Key = "Incoterm", Description = "Incoterm", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catTaxStatus = new LookupCategory { Id = Guid.NewGuid(), Key = "TaxStatus", Description = "Vergi Durumu", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catInvoiceType = new LookupCategory { Id = Guid.NewGuid(), Key = "InvoiceType", Description = "Fatura Tipi", IsTenantScoped = false, ModuleId = crmModule.Id };
                var catShippingMethod = new LookupCategory { Id = Guid.NewGuid(), Key = "ShippingMethod", Description = "Sevkiyat Yöntemi", IsTenantScoped = false, ModuleId = crmModule.Id };

                await context.LookupCategories.AddRangeAsync(
                    catCustomerType, catCustomerStatus, catCustomerCategory,
                    catSectorType, catCompanyType, catActivityArea,
                    catContactRole, catOpportunityStage, catActivityType,
                    catLeadSource, catPriority, catRegion, catInteractionChannel,
                    catPaymentMethod, catPaymentTerm, catCurrency,
                    catLossReason, catWinReason, catLeadStatus, catLeadDisqualifyReason,
                    catContactTitle, catCommunicationPreference, catGDPRConsent, catDoNotContactReason,
                    catSLAPlan, catCaseStatus, catCaseOrigin, catOpportunityProbabilityBand,
                    catCampaignType, catCampaignStatus, catMarketSegment, catAccountOwnership,
                    catRelationshipType, catRenewalLikelihood, catSatisfactionLevel, catLanguage,
                    catIncoterm, catTaxStatus, catInvoiceType, catShippingMethod
                );
                await context.SaveChangesAsync();

                var items = new List<LookupItem>
                {
                    // Customer Type
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerType.Id, Code = "BIREYSEL", Name = "Bireysel", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerType.Id, Code = "KURUMSAL", Name = "Kurumsal", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerType.Id, Code = "KAMU", Name = "Kamu", OrderNo = 3, IsActive = true },

                    // Customer Status
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerStatus.Id, Code = "AKTIF", Name = "Aktif", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerStatus.Id, Code = "PASIF", Name = "Pasif", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerStatus.Id, Code = "POTANSIYEL", Name = "Potansiyel", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerStatus.Id, Code = "KARALISTE", Name = "Kara Liste", OrderNo = 4, IsActive = true },

                    // Customer Category
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerCategory.Id, Code = "STANDART", Name = "Standart", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerCategory.Id, Code = "GOLD", Name = "Gold", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerCategory.Id, Code = "PREMIUM", Name = "Premium", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCustomerCategory.Id, Code = "VIP", Name = "VIP", OrderNo = 4, IsActive = true },

                    // Sector Type
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSectorType.Id, Code = "DIGER", Name = "Diğer", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSectorType.Id, Code = "TEKNOLOJI", Name = "Teknoloji", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSectorType.Id, Code = "URETIM", Name = "Üretim", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSectorType.Id, Code = "TICARET", Name = "Ticaret", OrderNo = 4, IsActive = true },

                    // Company Type
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCompanyType.Id, Code = "AS", Name = "Anonim Şirket", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCompanyType.Id, Code = "LTD", Name = "Limited Şirket", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCompanyType.Id, Code = "SIRKET", Name = "Kollektif/Komandit", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCompanyType.Id, Code = "SAHIBI", Name = "Şahıs", OrderNo = 4, IsActive = true },

                    // Activity Area
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catActivityArea.Id, Code = "DAGITIM", Name = "Dağıtım", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catActivityArea.Id, Code = "PERAKENDE", Name = "Perakende", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catActivityArea.Id, Code = "HIZMET", Name = "Hizmet", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catActivityArea.Id, Code = "IMALAT", Name = "İmalat", OrderNo = 4, IsActive = true },

                    // Contact Role
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactRole.Id, Code = "KARAR_VERICI", Name = "Karar Verici", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactRole.Id, Code = "ETKILEYICI", Name = "Etkileyici", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactRole.Id, Code = "KULLANICI", Name = "Kullanıcı", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactRole.Id, Code = "SATINALMACI", Name = "Satın Almacı", OrderNo = 4, IsActive = true },

                    // Opportunity Stage
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityStage.Id, Code = "ADAY", Name = "Aday", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityStage.Id, Code = "DEGERLENDIRME", Name = "Değerlendirme", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityStage.Id, Code = "TEKLIF", Name = "Teklif", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityStage.Id, Code = "MUZAKERE", Name = "Müzakere", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityStage.Id, Code = "KAZANILDI", Name = "Kazanıldı", OrderNo = 5, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityStage.Id, Code = "KAYBEDILDI", Name = "Kaybedildi", OrderNo = 6, IsActive = true },

                    // Activity Type
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catActivityType.Id, Code = "ARAMA", Name = "Arama", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catActivityType.Id, Code = "EPOSTA", Name = "E-posta", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catActivityType.Id, Code = "TOPLANTI", Name = "Toplantı", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catActivityType.Id, Code = "Ziyaret", Name = "Ziyaret", OrderNo = 4, IsActive = true },

                    // Lead Source
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadSource.Id, Code = "WEB", Name = "Web", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadSource.Id, Code = "ETKINLIK", Name = "Etkinlik", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadSource.Id, Code = "REFERANS", Name = "Referans", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadSource.Id, Code = "REKLAM", Name = "Reklam", OrderNo = 4, IsActive = true },

                    // Priority
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPriority.Id, Code = "DUSUK", Name = "Düşük", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPriority.Id, Code = "ORTA", Name = "Orta", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPriority.Id, Code = "YUKSEK", Name = "Yüksek", OrderNo = 3, IsActive = true },

                    // Region
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRegion.Id, Code = "MARMARA", Name = "Marmara", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRegion.Id, Code = "EGE", Name = "Ege", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRegion.Id, Code = "AKDENIZ", Name = "Akdeniz", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRegion.Id, Code = "ICANADOLU", Name = "İç Anadolu", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRegion.Id, Code = "KARADENIZ", Name = "Karadeniz", OrderNo = 5, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRegion.Id, Code = "DOGU", Name = "Doğu Anadolu", OrderNo = 6, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRegion.Id, Code = "GUNEYDOGU", Name = "Güneydoğu", OrderNo = 7, IsActive = true },

                    // Interaction Channel
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catInteractionChannel.Id, Code = "TELEFON", Name = "Telefon", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catInteractionChannel.Id, Code = "EPOSTA", Name = "E-posta", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catInteractionChannel.Id, Code = "Ziyaret", Name = "Ziyaret", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catInteractionChannel.Id, Code = "Toplanti", Name = "Toplantı", OrderNo = 4, IsActive = true },

                    // Payment Method
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPaymentMethod.Id, Code = "NAKIT", Name = "Nakit", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPaymentMethod.Id, Code = "HAVALE", Name = "Havale/EFT", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPaymentMethod.Id, Code = "KREDIKARTI", Name = "Kredi Kartı", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPaymentMethod.Id, Code = "CEK", Name = "Çek/Senet", OrderNo = 4, IsActive = true },

                    // Payment Term
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPaymentTerm.Id, Code = "PEIN", Name = "Peşin", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPaymentTerm.Id, Code = "30GUN", Name = "30 Gün", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPaymentTerm.Id, Code = "60GUN", Name = "60 Gün", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catPaymentTerm.Id, Code = "90GUN", Name = "90 Gün", OrderNo = 4, IsActive = true },

                    // Currency
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCurrency.Id, Code = "TRY", Name = "TL", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCurrency.Id, Code = "USD", Name = "Dolar", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCurrency.Id, Code = "EUR", Name = "Euro", OrderNo = 3, IsActive = true },

                    // Loss Reason
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLossReason.Id, Code = "BUTCE", Name = "Bütçe", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLossReason.Id, Code = "RAKIP", Name = "Rakip Tercihi", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLossReason.Id, Code = "KARARSIZLIK", Name = "Kararsızlık/İlerlenmedi", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLossReason.Id, Code = "UYUMSUZLUK", Name = "Gereksinim Uyumsuzluğu", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLossReason.Id, Code = "ZAMAN", Name = "Zamanlama", OrderNo = 5, IsActive = true },

                    // Win Reason
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catWinReason.Id, Code = "FIYAT", Name = "Fiyat", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catWinReason.Id, Code = "URUN_UYUMU", Name = "Ürün Uyumu", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catWinReason.Id, Code = "ILISKI", Name = "İlişki", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catWinReason.Id, Code = "SERVIS", Name = "Hizmet Kalitesi", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catWinReason.Id, Code = "TESLIMAT", Name = "Teslimat", OrderNo = 5, IsActive = true },

                    // Lead Status
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadStatus.Id, Code = "YENI", Name = "Yeni", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadStatus.Id, Code = "CALISILIYOR", Name = "Çalışılıyor", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadStatus.Id, Code = "NITELIKLI", Name = "Nitelikli", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadStatus.Id, Code = "DISKALIFIYE", Name = "Diskalifiye", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadStatus.Id, Code = "DONUSTU", Name = "Dönüştü", OrderNo = 5, IsActive = true },

                    // Lead Disqualify Reason
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadDisqualifyReason.Id, Code = "BUTCE_YOK", Name = "Bütçe Yok", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadDisqualifyReason.Id, Code = "KARAR_VEREN_DEGIL", Name = "Karar Verici Değil", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadDisqualifyReason.Id, Code = "ZAMAN_UYGUN_DEGIL", Name = "Zaman Uygun Değil", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadDisqualifyReason.Id, Code = "DUPLIKASYON", Name = "Duplikasyon", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLeadDisqualifyReason.Id, Code = "GECERSIZ", Name = "Geçersiz", OrderNo = 5, IsActive = true },

                    // Contact Title
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactTitle.Id, Code = "OWNER", Name = "Sahip", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactTitle.Id, Code = "CEO", Name = "CEO", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactTitle.Id, Code = "CTO", Name = "CTO", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactTitle.Id, Code = "CFO", Name = "CFO", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactTitle.Id, Code = "COO", Name = "COO", OrderNo = 5, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactTitle.Id, Code = "MANAGER", Name = "Yönetici", OrderNo = 6, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catContactTitle.Id, Code = "SPECIALIST", Name = "Uzman", OrderNo = 7, IsActive = true },

                    // Communication Preference
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCommunicationPreference.Id, Code = "EPOSTA", Name = "E-posta", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCommunicationPreference.Id, Code = "TELEFON", Name = "Telefon", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCommunicationPreference.Id, Code = "SMS", Name = "SMS", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCommunicationPreference.Id, Code = "WHATSAPP", Name = "WhatsApp", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCommunicationPreference.Id, Code = "FARKETMEZ", Name = "Farketmez", OrderNo = 5, IsActive = true },

                    // GDPR Consent
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catGDPRConsent.Id, Code = "ONAY", Name = "Onaylı", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catGDPRConsent.Id, Code = "RED", Name = "Reddedildi", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catGDPRConsent.Id, Code = "BEKLEMEDE", Name = "Beklemede", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catGDPRConsent.Id, Code = "UYGULANMAZ", Name = "Uygulanmaz", OrderNo = 4, IsActive = true },

                    // Do Not Contact Reason
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catDoNotContactReason.Id, Code = "OPT_OUT", Name = "Abonelikten çıktı", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catDoNotContactReason.Id, Code = "BOUNCE", Name = "E-posta geri döndü", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catDoNotContactReason.Id, Code = "COMPLAINT", Name = "Şikayet", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catDoNotContactReason.Id, Code = "LEGAL", Name = "Yasal kısıt", OrderNo = 4, IsActive = true },

                    // SLA Plan
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSLAPlan.Id, Code = "BRONZ", Name = "Bronz", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSLAPlan.Id, Code = "GUMUS", Name = "Gümüş", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSLAPlan.Id, Code = "ALTIN", Name = "Altın", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSLAPlan.Id, Code = "PLATIN", Name = "Platin", OrderNo = 4, IsActive = true },

                    // Case Status
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseStatus.Id, Code = "ACIK", Name = "Açık", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseStatus.Id, Code = "DEVAM", Name = "Devam Ediyor", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseStatus.Id, Code = "MUSTERI_BEKLENIYOR", Name = "Müşteri Bekleniyor", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseStatus.Id, Code = "COZUMLENDI", Name = "Çözümlendi", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseStatus.Id, Code = "KAPANDI", Name = "Kapandı", OrderNo = 5, IsActive = true },

                    // Case Origin
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseOrigin.Id, Code = "TELEFON", Name = "Telefon", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseOrigin.Id, Code = "EPOSTA", Name = "E-posta", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseOrigin.Id, Code = "WEB", Name = "Web", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseOrigin.Id, Code = "PORTAL", Name = "Portal", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCaseOrigin.Id, Code = "SOSYAL", Name = "Sosyal", OrderNo = 5, IsActive = true },

                    // Opportunity Probability Band
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityProbabilityBand.Id, Code = "DUSUK", Name = "Düşük (0-25)", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityProbabilityBand.Id, Code = "ORTA", Name = "Orta (26-50)", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityProbabilityBand.Id, Code = "YUKSEK", Name = "Yüksek (51-75)", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catOpportunityProbabilityBand.Id, Code = "COKYUKSEK", Name = "Çok Yüksek (76-100)", OrderNo = 4, IsActive = true },

                    // Campaign Type
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignType.Id, Code = "EPOSTA", Name = "E-posta", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignType.Id, Code = "WEBINAR", Name = "Webinar", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignType.Id, Code = "ETKINLIK", Name = "Etkinlik", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignType.Id, Code = "SOSYAL", Name = "Sosyal", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignType.Id, Code = "REKLAM", Name = "Reklam", OrderNo = 5, IsActive = true },

                    // Campaign Status
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignStatus.Id, Code = "PLANLI", Name = "Planlı", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignStatus.Id, Code = "AKTIF", Name = "Aktif", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignStatus.Id, Code = "DURDURULDU", Name = "Durduruldu", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignStatus.Id, Code = "TAMAMLANDI", Name = "Tamamlandı", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catCampaignStatus.Id, Code = "IPTAL", Name = "İptal", OrderNo = 5, IsActive = true },

                    // Market Segment
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catMarketSegment.Id, Code = "SMB", Name = "Küçük İşletme", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catMarketSegment.Id, Code = "MID", Name = "Orta Ölçek", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catMarketSegment.Id, Code = "ENT", Name = "Kurumsal", OrderNo = 3, IsActive = true },

                    // Account Ownership
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catAccountOwnership.Id, Code = "DOGRUDAN", Name = "Doğrudan", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catAccountOwnership.Id, Code = "IS_ORTAGI", Name = "İş Ortağı", OrderNo = 2, IsActive = true },

                    // Relationship Type
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRelationshipType.Id, Code = "ADAY", Name = "Aday", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRelationshipType.Id, Code = "MUSTERI", Name = "Müşteri", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRelationshipType.Id, Code = "PARTNER", Name = "Partner", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRelationshipType.Id, Code = "TEDARIKCI", Name = "Tedarikçi", OrderNo = 4, IsActive = true },

                    // Renewal Likelihood
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRenewalLikelihood.Id, Code = "DUSUK", Name = "Düşük", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRenewalLikelihood.Id, Code = "ORTA", Name = "Orta", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catRenewalLikelihood.Id, Code = "YUKSEK", Name = "Yüksek", OrderNo = 3, IsActive = true },

                    // Satisfaction Level
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSatisfactionLevel.Id, Code = "COOK_KOTU", Name = "Çok Kötü", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSatisfactionLevel.Id, Code = "KOTU", Name = "Kötü", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSatisfactionLevel.Id, Code = "NÖTR", Name = "Nötr", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSatisfactionLevel.Id, Code = "IYI", Name = "İyi", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catSatisfactionLevel.Id, Code = "COOK_IYI", Name = "Çok İyi", OrderNo = 5, IsActive = true },

                    // Language
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLanguage.Id, Code = "TR", Name = "Türkçe", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLanguage.Id, Code = "EN", Name = "İngilizce", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLanguage.Id, Code = "DE", Name = "Almanca", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLanguage.Id, Code = "FR", Name = "Fransızca", OrderNo = 4, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catLanguage.Id, Code = "AR", Name = "Arapça", OrderNo = 5, IsActive = true },

                    // Incoterm
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catIncoterm.Id, Code = "EXW", Name = "EXW", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catIncoterm.Id, Code = "FOB", Name = "FOB", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catIncoterm.Id, Code = "CIF", Name = "CIF", OrderNo = 3, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catIncoterm.Id, Code = "DDP", Name = "DDP", OrderNo = 4, IsActive = true },

                    // Tax Status
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catTaxStatus.Id, Code = "TAM", Name = "Tam Mükellef", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catTaxStatus.Id, Code = "MUAF", Name = "Vergi Muaf", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catTaxStatus.Id, Code = "TERS_ISLEM", Name = "Ters İşlem/RC", OrderNo = 3, IsActive = true },

                    // Invoice Type
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catInvoiceType.Id, Code = "E_FATURA", Name = "E-Fatura", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catInvoiceType.Id, Code = "E_ARSIV", Name = "E-Arşiv", OrderNo = 2, IsActive = true },

                    // Shipping Method
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catShippingMethod.Id, Code = "KURYE", Name = "Kurye", OrderNo = 1, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catShippingMethod.Id, Code = "KARGO", Name = "Kargo", OrderNo = 2, IsActive = true },
                    new LookupItem { Id = Guid.NewGuid(), CategoryId = catShippingMethod.Id, Code = "TESLIMALMA", Name = "Teslim Alma", OrderNo = 3, IsActive = true },
                };

                await context.LookupItems.AddRangeAsync(items);
                await context.SaveChangesAsync();
            }
            // Mevcut kategorilerde ModuleId boş ise CRM'e bağla (opsiyonel)
            var crm = await context.LookupModules.FirstOrDefaultAsync(x => x.Key == "CRM");
            if (crm != null)
            {
                var categoriesWithoutModule = await context.LookupCategories.Where(c => c.ModuleId == null).ToListAsync();
                if (categoriesWithoutModule.Any())
                {
                    foreach (var c in categoriesWithoutModule)
                    {
                        c.ModuleId = crm.Id;
                    }
                    await context.SaveChangesAsync();
                }
            }

            var companyName = "Danube Yazılım";
            var workCompany = await context.WorkCompany.FirstOrDefaultAsync(w => w.Name == companyName);
            if (workCompany == null)
            {
                workCompany = new WorkCompany
                {
                    Id = Guid.NewGuid(),
                    Name = companyName,
                    ApproveWorkDesign = ApproveWorkDesign.NoApprove,
                    IsActive = true
                };
                context.WorkCompany.Add(workCompany);
                await context.SaveChangesAsync();
            }

            var adminEmail = "muratmerdogan@gmail.com";
            var existingUser = await userManager.FindByEmailAsync(adminEmail);
            if (existingUser == null)
            {
                var password = Environment.GetEnvironmentVariable("SEED_ADMIN_PASSWORD") ?? "Test1234!";
                var user = new UserApp
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Murat",
                    LastName = "Merdoğan",
                    isSystemAdmin = true,
                    WorkCompanyId = workCompany.Id,
                    canSsoLogin = true
                };

                await userManager.CreateAsync(user, password);
            }


            // CRM: Seed 250 customers if none exist
            if (!await context.Customers.AnyAsync())
            {
                var random = new Random();
                var customers = new List<core.Models.CRM.Customer>();

                // Şehirler, sektörler ve firma türleri
                var cities = new[] { "İstanbul", "Ankara", "İzmir", "Bursa", "Antalya", "Adana", "Konya", "Gaziantep", "Kayseri", "Eskişehir" };
                var districts = new[] { "Merkez", "Kadıköy", "Beşiktaş", "Şişli", "Ataşehir", "Kartal", "Maltepe", "Ümraniye", "Pendik", "Tuzla" };
                var sectors = new[] { "Teknoloji", "İmalat", "Perakende", "Finans", "Sağlık", "Eğitim", "İnşaat", "Lojistik", "Gıda", "Otomotiv" };
                var companyTypes = new[] { "A.Ş.", "Ltd. Şti.", "San. ve Tic. A.Ş.", "Tic. Ltd. Şti.", "İnş. San. Tic. A.Ş." };
                var tags = new[] { "Potansiyel", "Aktif", "VIP", "Stratejik", "Yeni", "Demo", "Test", "Aday", "Premium", "Gold" };

                for (int i = 1; i <= 250; i++)
                {
                    var cityIndex = random.Next(cities.Length);
                    var city = cities[cityIndex];
                    var district = districts[random.Next(districts.Length)];
                    var sector = sectors[random.Next(sectors.Length)];
                    var companyType = companyTypes[random.Next(companyTypes.Length)];
                    var customerType = (CustomerType)random.Next(0, 4);
                    var category = (CustomerCategory)random.Next(0, 4);
                    var status = (CustomerStatus)random.Next(0, 3); // Karaliste hariç
                    var lifecycleStage = (LifecycleStage)random.Next(0, 5);

                    var customerCompanyName = $"{GetRandomCompanyName(random)} {companyType}";
                    var legalName = customerCompanyName.Replace(companyType, $"Sanayi ve Ticaret {companyType}");

                    var cust = new core.Models.CRM.Customer
                    {
                        Name = customerCompanyName,
                        LegalName = legalName,
                        Code = $"CUST{i:0000}",
                        CustomerType = customerType,
                        Category = category,
                        Status = status,
                        LifecycleStage = lifecycleStage,
                        TaxOffice = $"{city} {random.Next(1, 10)}. Vergi Dairesi",
                        TaxNumber = GenerateRandomTaxNumber(random),
                        IsReferenceCustomer = random.NextDouble() < 0.1, // %10 referans müşteri
                        OwnerId = Guid.NewGuid().ToString(),
                        NextActivityDate = DateTime.Now.AddDays(random.Next(1, 30)),
                        Note = $"Otomatik oluşturulan {sector} sektöründen demo müşteri.",


                        // Adresler
                        Addresses = new List<CustomerAddress>
                        {
                            new CustomerAddress
                            {
                                Type = AddressType.Merkez,
                                Country = "TR",
                                City = city,
                                District = district,
                                PostalCode = $"{34000 + random.Next(0, 100):00000}",
                                Line1 = $"{GetRandomStreetName(random)} Cd. No:{random.Next(1, 200)}",
                                Line2 = random.NextDouble() < 0.3 ? $"Kat:{random.Next(1, 10)} Daire:{random.Next(1, 20)}" : "",
                                IsDefaultBilling = true,
                                IsDefaultShipping = random.NextDouble() < 0.7,
                                IsBilling = true,
                                IsShipping = random.NextDouble() < 0.8,
                                IsActive = true,

                            }
                        },

                        // Email'ler
                        SecondaryEmails = new List<CustomerEmail>
                        {
                            new CustomerEmail
                            {
                                Email = $"info@{GetEmailDomain(customerCompanyName)}.com",
                                Description = "Genel",
                                Notify = true,
                                Bulk = false,
                                IsActive = true,
                                IsPrimary = true,

                            },
                            new CustomerEmail
                            {
                                Email = $"sales@{GetEmailDomain(customerCompanyName)}.com",
                                Description = "Satış",
                                Notify = false,
                                Bulk = true,
                                IsActive = true,
                                IsPrimary = false,

                            }
                        },

                        // Telefonlar
                        Phones = new List<CustomerPhone>
                        {
                            new CustomerPhone
                            {
                                Label = "Merkez",
                                Number = $"+90 {random.Next(200, 600)} {random.Next(100, 999)} {random.Next(10, 99)} {random.Next(10, 99)}",
                                IsPrimary = true,
                                IsActive = true,

                            },
                            new CustomerPhone
                            {
                                Label = "Mobil",
                                Number = $"+90 5{random.Next(10, 99)} {random.Next(100, 999)} {random.Next(10, 99)} {random.Next(10, 99)}",
                                IsPrimary = false,
                                IsActive = true,

                            }
                        },

                        // Notlar
                        Notes = new List<CustomerNote>
                        {
                            new CustomerNote
                            {
                                Date = DateTime.Now.AddDays(-random.Next(1, 30)),
                                Title = "İlk Görüşme",
                                Content = $"{sector} sektöründe faaliyet gösteren firma ile ilk görüşme gerçekleştirildi.",
 // PostgreSQL için boş RowVersion
                            },
                            new CustomerNote
                            {
                                Date = DateTime.Now.AddDays(-random.Next(31, 60)),
                                Title = "Firma Kuruluşu",
                                Content = "Firma kaydı sisteme eklendi.",
 // PostgreSQL için boş RowVersion
                            }
                        },

                        // Tag'ler
                        Tags = new List<CustomerTag>
                        {
                            new CustomerTag { Tag = tags[random.Next(tags.Length)] },
                            new CustomerTag { Tag = "Demo" }
                        },

                        // Sektörler
                        Sectors = new List<CustomerSector>
                        {
                            new CustomerSector { Sector = sector }
                        },

                        Documents = new List<CustomerDocument>(),
                        CustomFields = new List<CustomerCustomField>()
                    };

                    customers.Add(cust);
                }

                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }
        }

        private static string GetRandomCompanyName(Random random)
        {
            var adjectives = new[] { "Güç", "Hız", "Yeni", "Güvenli", "Modern", "Akıllı", "Dijital", "Global", "Elite", "Premier" };
            var nouns = new[] { "Teknoloji", "Yazılım", "Sistem", "Çözüm", "İnovasyon", "Endüstri", "Makina", "Enerji", "Ticaret", "Hizmet" };

            return $"{adjectives[random.Next(adjectives.Length)]} {nouns[random.Next(nouns.Length)]}";
        }

        private static string GetRandomStreetName(Random random)
        {
            var streetNames = new[] { "Atatürk", "İstiklal", "Cumhuriyet", "Gazi", "Kılıçarslan", "Mimar Sinan", "Barbaros", "Fatih", "Yıldırım", "İnönü" };
            return streetNames[random.Next(streetNames.Length)];
        }

        private static string GetEmailDomain(string companyName)
        {
            return companyName.ToLowerInvariant()
                .Replace(" ", "")
                .Replace("a.ş.", "")
                .Replace("ltd.şti.", "")
                .Replace("san.vetic.a.ş.", "")
                .Replace("tic.ltd.şti.", "")
                .Replace("inş.san.tic.a.ş.", "")
                .Replace("ç", "c")
                .Replace("ğ", "g")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ş", "s")
                .Replace("ü", "u");
        }

        private static string GenerateRandomTaxNumber(Random random)
        {
            return $"{random.Next(1000000000, 2000000000)}";
        }
    }
}


