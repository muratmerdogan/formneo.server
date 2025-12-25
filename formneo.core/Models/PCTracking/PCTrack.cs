using System.ComponentModel;

namespace formneo.core.Models.PCTracking
{
    public class PCTrack : BaseEntity
    {
        public string? PCname { get; set; }
        public DateTime? ProWorkFlowDefinationcessTime { get; set; }
        public ProcessTypes? ProcessType { get; set; }
        public LoginType? LoginType { get; set; }
        public string? LoginProcessName { get; set; }
        public string? LoginId { get; set; }
        public string? SubjectLoginId { get; set; }
        public string? XmlData { get; set; }
    }

    public enum LoginType
    {
        [Description("Login On")]
        LoginOn = 1,
        [Description("Login Off")]
        LoginOff = 2,
        [Description("PC is Active")]
        LoginActive = 5,
        [Description("Kilit Açma")]
        UnLocking = 7,
        [Description("Önbellek Giriş")]
        CachedEntry = 11,
    }

}
