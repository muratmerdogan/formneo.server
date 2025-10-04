using System.ComponentModel;

namespace formneo.core.Models.PCTracking
{
    public enum ProcessTypes
    {
        [Description("Power On")]
        PowerOn = 1,
        [Description("Power Off")]
        PowerOff = 2,
        [Description("Login")]
        Login = 3,
        [Description("Logoff")]
        Logoff = 4,
        [Description("PC is Active")]
        LoginActive = 5,
        [Description("Process")]
        Process = 4624,

    }
}
