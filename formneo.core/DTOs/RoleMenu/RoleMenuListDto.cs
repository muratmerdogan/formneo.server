public class RoleMenuListDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string RoleId { get; set; }
    public Guid MenuId { get; set; }
    public bool CanView { get; set; }
    public bool CanAdd { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }

    public string Description { get; set; }
    public string CreatedAt { get; set; }
    public string Status { get; set; }
}

public class RoleMenuInsertDto
{
    public string RoleName { get; set; }

    public string Description { get; set; }

    public List<MenuPermissionDto> MenuPermissions { get; set; }
}

public class RoleMenuUpdateDto
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public List<MenuPermissionDto> MenuPermissions { get; set; }
}

public class RoleMenuResuResultDto
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public string Description { get; set; }
    public List<MenuPermissionDto> MenuPermissions { get; set; }
}

public class MenuPermissionDto
{
    public Guid MenuId { get; set; }
    public bool CanView { get; set; }
    public bool CanAdd { get; set; }
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
}