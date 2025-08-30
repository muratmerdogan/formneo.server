using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vesa.core.EnumExtensions
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {

            try
            {
                if (value == null)
                    return null;

                FieldInfo field = value.GetType().GetField(value.ToString());

                DescriptionAttribute attribute
                        = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                            as DescriptionAttribute;

                return attribute == null ? value.ToString() : attribute.Description;
            }
            catch
            {
                return "";
            }
        }
    }

    // Shared enums for MainClient
    public enum MainClientStatus
    {
        Pending = 0,
        Active = 1,
        Suspended = 2,
        Closed = 3
    }

    public enum MainClientPlan
    {
        Free = 0,
        Pro = 1,
        Enterprise = 2
    }

    public enum SsoType
    {
        AzureAD = 0,
        Okta = 1,
        Keycloak = 2,
        Custom = 3
    }
}
