using System;

namespace formneo.core.Models.Security
{
    [Flags]
    public enum Actions
    {
        None = 0,
        View = 1 << 0,
        Create = 1 << 1,
        Update = 1 << 2,
        Delete = 1 << 3,
        Export = 1 << 4,
        Full = View | Create | Update | Delete | Export
    }
}


