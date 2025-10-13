using System.ComponentModel;

namespace formneo.core.Models.FormEnums
{
    public enum FormPublicationStatus
    {
        [Description("Taslak")]
        Draft = 1,

        [Description("Yayınlandı")]
        Published = 2,

        [Description("Arşivlendi")]
        Archived = 3
    }
}


