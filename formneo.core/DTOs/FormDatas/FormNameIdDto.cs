using System;

namespace formneo.core.DTOs.FormDatas
{
    /// <summary>
    /// Form adı ve ID'sini içeren basit DTO
    /// </summary>
    public class FormNameIdDto
    {
        /// <summary>
        /// Form ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Form adı
        /// </summary>
        public string FormName { get; set; }
    }
}

