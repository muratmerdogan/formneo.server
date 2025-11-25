using System;
using System.Collections.Generic;

namespace formneo.core.DTOs
{
    /// <summary>
    /// Form input element bilgilerini içeren DTO
    /// </summary>
    public class FormInputElementDto
    {
        /// <summary>
        /// Element'in unique ID'si (x-designable-id)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Element'in adı/label'ı (title veya name)
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Element'in title değeri (eğer varsa)
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Element'in tipi (type - string, number, boolean vb.)
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Component tipi (x-component - Input, Password, DatePicker vb.)
        /// </summary>
        public string ComponentType { get; set; }

        /// <summary>
        /// Decorator tipi (x-decorator - FormItem vb.)
        /// </summary>
        public string? Decorator { get; set; }

        /// <summary>
        /// Validator kuralları (x-validator)
        /// </summary>
        public List<string> Validators { get; set; } = new List<string>();

        /// <summary>
        /// Component özellikleri (x-component-props)
        /// </summary>
        public Dictionary<string, object> ComponentProps { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Decorator özellikleri (x-decorator-props)
        /// </summary>
        public Dictionary<string, object> DecoratorProps { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Element'in sırası (x-index)
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// Element'in tam JSON yapısı (isteğe bağlı - debug için)
        /// </summary>
        public object? RawData { get; set; }
    }
}

