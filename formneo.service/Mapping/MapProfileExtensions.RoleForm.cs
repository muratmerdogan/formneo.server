using AutoMapper;
using formneo.core.DTOs.RoleForm;
using formneo.core.Models;

namespace formneo.service.Mapping
{
    public static class MapProfileExtensionsRoleForm
    {
        public static void AddRoleFormMappings(this Profile profile)
        {
            profile.CreateMap<AspNetRolesTenantForm, RoleTenantFormListDto>().ReverseMap();
        }
    }
}


