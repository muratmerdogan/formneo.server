using System;
using System.ComponentModel.DataAnnotations;

namespace formneo.core.DTOs
{
    /// <summary>
    /// Tüm Update DTO'lar için base sınıf
    /// </summary>
    public abstract class BaseUpdateDto
    {
        [Required(ErrorMessage = "ID zorunludur")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Concurrency bilgisi zorunludur")]
        public uint ConcurrencyToken { get; set; }
    }

    /// <summary>
    /// Tüm List DTO'lar için base sınıf (sadece okunacak veriler)
    /// </summary>
    public abstract class BaseListDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public uint ConcurrencyToken { get; set; }
    }

    /// <summary>
    /// Tüm Detail DTO'lar için base sınıf (detaylı bilgiler)
    /// </summary>
    public abstract class BaseDetailDto : BaseListDto
    {
        // Detail DTO'larda ekstra alanlar olabilir
        // Bu sınıf genişletilebilir
    }
}