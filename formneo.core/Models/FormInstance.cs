using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace formneo.core.Models
{
    /// <summary>
    /// WorkflowHead'e bağlı form instance'ı (her zaman son/güncel form verisi)
    /// </summary>
    public class FormInstance : BaseEntity
    {
        /// <summary>
        /// WorkflowHead ID (ForeignKey)
        /// </summary>
        [ForeignKey("WorkflowHead")]
        public Guid WorkflowHeadId { get; set; }

        /// <summary>
        /// Form ID (Form tablosuna referans)
        /// </summary>
        [ForeignKey("Form")]
        public Guid? FormId { get; set; }

        /// <summary>
        /// FormDesign JSON verisi (form tasarımı)
        /// </summary>
        public string? FormDesign { get; set; }

        /// <summary>
        /// Form verileri (JSON) - Her zaman son/güncel veri
        /// </summary>
        public string? FormData { get; set; }

        /// <summary>
        /// Son güncelleyen kullanıcı
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Son güncelleyen kullanıcının adı soyadı
        /// </summary>
        public string? UpdatedByNameSurname { get; set; }

        /// <summary>
        /// WorkflowHead ile ilişki
        /// </summary>
        public virtual WorkflowHead WorkflowHead { get; set; }

        /// <summary>
        /// Form ile ilişki
        /// </summary>
        public virtual Form? Form { get; set; }
    }
}

