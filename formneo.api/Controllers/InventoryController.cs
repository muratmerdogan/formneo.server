using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions;
using NLayer.Core.Services;
using System.ComponentModel;
using System.ServiceProcess;
using vesa.core.DTOs;
using vesa.core.DTOs.DepartmentUserDto;
using vesa.core.DTOs.Inventory;
using vesa.core.DTOs.Menu;
using vesa.core.DTOs.Ticket.TicketDepartments;
using vesa.core.DTOs.TicketProjects;
using vesa.core.Models;
using vesa.core.Models.Inventory;
using vesa.core.Models.Ticket;
using vesa.core.Services;
using vesa.service.Services;

namespace vesa.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InventoryController : CustomBaseController
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;
        private readonly UserManager<UserApp> _userManager;
        private readonly IServiceWithDto<TicketDepartment, TicketDepartmensListDto> _ticketDepartments;
        public InventoryController(IMapper mapper, IInventoryService inventoryService, UserManager<UserApp> userManager, IServiceWithDto<TicketDepartment, TicketDepartmensListDto> ticketDepartments)
        {
            _mapper = mapper;
            _inventoryService = inventoryService;
            _userManager = userManager;
            _ticketDepartments = ticketDepartments;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventoryListDto>>> GetAll()
        {
            try
            {
                var data = await _inventoryService
                    .Where(e => true)
                    .Include(e => e.UserApp)
                    .Include(e => e.TicketDepartment)
                    .ToListAsync();

                var result = data.Select(e => new InventoryListDto
                {
                    Id = e.Id,
                    AssetTag = e.AssetTag,
                    DeviceName = e.DeviceName,
                    Type = e.Type,
                    Brand = e.Brand,
                    Model = e.Model,
                    SerialNumber = e.SerialNumber,
                    Status = e.Status,
                    Description = e.Description,
                    CPU = e.CPU,
                    RAM = e.RAM,
                    DiskType = e.DiskType,
                    DiskSize = e.DiskSize,
                    GPU = e.GPU,
                    MACAddress = e.MACAddress,
                    StaticIPAddress = e.StaticIPAddress,
                    OperatingSystem = e.OperatingSystem,
                    OS_LicenseStatus = e.OS_LicenseStatus,
                    OfficeLicense = e.OfficeLicense,
                    UserAppId = e.UserAppId,
                    UserApp = e.UserApp == null ? null : new UserAppDto
                    {
                        Id = e.UserApp.Id,
                        FirstName = e.UserApp.FirstName,
                        LastName = e.UserApp.LastName,
                    },
                    TicketDepartmentId = e.TicketDepartmentId,
                    TicketDepartment = e.TicketDepartment == null ? null : new TicketDepartmensListDto
                    {
                        Id = e.TicketDepartment.Id.ToString(),
                        DeparmentCode = e.TicketDepartment.DeparmentCode,
                        DepartmentText = e.TicketDepartment.DepartmentText,
                    },
                    OfficeLocation = e.OfficeLocation,
                    PurchaseDate = e.PurchaseDate,
                    InvoiceOrVendor = e.InvoiceOrVendor,
                    WarrantyEndDate = e.WarrantyEndDate,
                    AssetNumber = e.AssetNumber,
                    LastMaintenanceDate = e.LastMaintenanceDate,
                    QRorBarcode = e.QRorBarcode

                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryListDto>> GetById(Guid id)
        {
            try
            {
                var data = await _inventoryService.Where(e => e.Id == id).Include(e => e.UserApp).Include(e => e.TicketDepartment).FirstOrDefaultAsync();

                if (data == null)
                {
                    return NotFound("Inventory not found.");
                }

                var result = new InventoryListDto
                {
                    Id = data.Id,
                    AssetTag = data.AssetTag,
                    DeviceName = data.DeviceName,
                    Type = data.Type,
                    Brand = data.Brand,
                    Model = data.Model,
                    SerialNumber = data.SerialNumber,
                    Status = data.Status,
                    Description = data.Description,
                    CPU = data.CPU,
                    RAM = data.RAM,
                    DiskType = data.DiskType,
                    DiskSize = data.DiskSize,
                    GPU = data.GPU,
                    MACAddress = data.MACAddress,
                    StaticIPAddress = data.StaticIPAddress,
                    OperatingSystem = data.OperatingSystem,
                    OS_LicenseStatus = data.OS_LicenseStatus,
                    OfficeLicense = data.OfficeLicense,
                    UserAppId = data.UserAppId,
                    //UserApp = data.UserApp == null ? null : new UserAppDto
                    //{
                    //    Id = data.UserApp.Id,
                    //    FirstName = data.UserApp.FirstName,
                    //    LastName = data.UserApp.LastName,
                    //    UserName = data.UserApp.UserName
                    //},
                    TicketDepartmentId = data.TicketDepartmentId,
                    TicketDepartment = data.TicketDepartment == null ? null : new TicketDepartmensListDto
                    {
                        Id = data.TicketDepartment.Id.ToString(),
                        DeparmentCode = data.TicketDepartment.DeparmentCode,
                        DepartmentText = data.TicketDepartment.DepartmentText,
                    },
                    OfficeLocation = data.OfficeLocation,
                    PurchaseDate = data.PurchaseDate,
                    InvoiceOrVendor = data.InvoiceOrVendor,
                    WarrantyEndDate = data.WarrantyEndDate,
                    AssetNumber = data.AssetNumber,
                    LastMaintenanceDate = data.LastMaintenanceDate,
                    QRorBarcode = data.QRorBarcode
                };

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InventoryInsertDto dto)
        {
            try
            {
                var newInventory = _mapper.Map<Inventory>(dto);

                await _inventoryService.AddAsync(newInventory);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(InventoryUpdateDto dto)
        {
            try
            {
                var existing = await _inventoryService.Where(e => e.Id == dto.Id).FirstOrDefaultAsync();

                if (existing == null)
                {
                    return NotFound("Inventory not found.");
                }

                existing.AssetTag = dto.AssetTag;
                existing.DeviceName = dto.DeviceName;
                existing.Type = dto.Type;
                existing.Brand = dto.Brand;
                existing.Model = dto.Model;
                existing.SerialNumber = dto.SerialNumber;
                existing.Status = dto.Status;
                existing.Description = dto.Description;
                existing.CPU = dto.CPU;
                existing.RAM = dto.RAM;
                existing.DiskType = dto.DiskType;
                existing.DiskSize = dto.DiskSize;
                existing.GPU = dto.GPU;
                existing.MACAddress = dto.MACAddress;
                existing.StaticIPAddress = dto.StaticIPAddress;
                existing.OperatingSystem = dto.OperatingSystem;
                existing.OS_LicenseStatus = dto.OS_LicenseStatus;
                existing.OfficeLicense = dto.OfficeLicense;
                existing.UserAppId = dto.UserAppId;
                existing.TicketDepartmentId = dto.TicketDepartmentId;
                existing.OfficeLocation = dto.OfficeLocation;
                existing.PurchaseDate = dto.PurchaseDate;
                existing.InvoiceOrVendor = dto.InvoiceOrVendor;
                existing.WarrantyEndDate = dto.WarrantyEndDate;
                existing.AssetNumber = dto.AssetNumber;
                existing.LastMaintenanceDate = dto.LastMaintenanceDate;
                existing.QRorBarcode = dto.QRorBarcode;

                await _inventoryService.UpdateAsync(existing);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                return StatusCode(500, $"An error occurred while updating the ticket project: {ex.Message}");
            }
        }

        [HttpPut("Assign")]
        public async Task<IActionResult> Assign([FromQuery] Guid inventoryId, string? userId)
        {
            try
            {
                #region Null Kontrolü
                if (inventoryId == Guid.Empty)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400, "Envanter Id zorunludur."));
                }
                #endregion

                var existing = await _inventoryService.Where(e => e.Id == inventoryId).FirstOrDefaultAsync();

                if (existing == null)
                {
                    return NotFound("Inventory not found.");
                }

                existing.UserAppId = userId;

                await _inventoryService.UpdateAsync(existing);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the ticket project: {ex.Message}");
            }
        }

        [HttpGet("GetAssignUser")]
        public async Task<ActionResult<UserApp>> GetAssignUser([FromQuery] Guid inventoryId)
        {
            try
            {
                var existing = await _inventoryService.Where(e => e.Id == inventoryId).Include(e=>e.UserApp).FirstOrDefaultAsync();

                if (existing == null)
                {
                    return NotFound("Inventory not found.");
                }

                return existing.UserApp;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the ticket project: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Deletet(Guid id)
        {
            try
            {
                var data = await _inventoryService.GetByIdStringGuidAsync(id);

                if (data == null)
                {
                    return NotFound("Inventory not found.");
                }

                await _inventoryService.RemoveAsync(data);

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(500, ex.Message));
            }
        }

        [HttpGet("GetEnums")]
        public async Task<ActionResult<List<EnumListDto>>> GetEnums()
        {
            try
            {
                var enumTypes = new[]
                 {
                    typeof(OfficeLocation),
                    typeof(DeviceType),
                    typeof(DeviceStatus),
                    typeof(vesa.core.Models.Inventory.DiskType),
                    typeof(LicenseStatus)
                };

                var result = new List<EnumListDto>();

                foreach (var enumType in enumTypes)
                {
                    var enumValues = Enum.GetValues(enumType)
                        .Cast<Enum>()
                        .Select(e => new EnumDto
                        {
                            Name = e.ToString(),
                            Number = Convert.ToInt32(e),
                            Description = GetEnumDescription(e)
                        })
                        .ToList();

                    result.Add(new EnumListDto
                    {
                        EnumClass = enumType.Name,
                        Enums = enumValues
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        private string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
            }
            return value.ToString();
        }
    }
}
