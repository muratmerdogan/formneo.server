using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Services;
using System;
using formneo.core.DTOs;
using formneo.core.DTOs.Budget.SF;
using formneo.core.Models;
using formneo.core.Services;
using formneo.core.UnitOfWorks;

namespace formneo.service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGlobalServiceWithDto<AspNetRolesMenu, RoleMenuListDto> _roleMenuService;


        public UserService(UserManager<UserApp> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IUnitOfWork unitOfWork,  IGlobalServiceWithDto<AspNetRolesMenu, RoleMenuListDto> roleMenuService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleMenuService = roleMenuService;
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {

            _unitOfWork.BeginTransaction();
            var user = new UserApp
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                photo = createUserDto.photo,
                Email = createUserDto.Email,
                UserName = createUserDto.UserName,
                WorkCompanyId = null,
                TicketDepartmentId =null,
                canSsoLogin = createUserDto.canSsoLogin,
                isSystemAdmin = createUserDto.isSystemAdmin,
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseDto<UserAppDto>.Fail(400, errors);
            }


            var userAppDto = _mapper.Map<UserAppDto>(user);

            _unitOfWork.Commit();
            return CustomResponseDto<UserAppDto>.Success(200, userAppDto);
        }

        public async Task<CustomResponseDto<List<UserAppDto>>> GetAllUsersAsync()
        {
            // Tüm kullanıcıları çekiyoruz
            var users = _userManager.Users.ToList();

            // Kullanıcıları UserAppDto'ya map ediyoruz
            var list = _mapper.Map<List<UserAppDto>>(users);

            // Başarılı bir yanıt dönüyoruz
            return CustomResponseDto<List<UserAppDto>>.Success(200, list);
        }


        public async Task<CustomResponseDto<List<UserAppDto>>> GetAllUsersAsyncWitName(string name)
        {
            name = NormalizeTurkishCharacters(name);
            // Tüm kullanıcıları çekiyoruz
            var users = _userManager.Users
           .Where(u => EF.Functions.Like(u.UserName, $"{name}%") && u.isBlocked == false && u.isTestData != true).ToList();

            // Kullanıcıları UserAppDto'ya map ediyoruz
            var list = _mapper.Map<List<UserAppDto>>(users);

            // Başarılı bir yanıt dönüyoruz
            return CustomResponseDto<List<UserAppDto>>.Success(200, list);
        }
        private string NormalizeTurkishCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace('ş', 's')
                .Replace('Ş', 'S')
                .Replace('ğ', 'g')
                .Replace('Ğ', 'G')
                .Replace('ı', 'i')
                .Replace('İ', 'I')
                .Replace('ç', 'c')
                .Replace('Ç', 'C')
                .Replace('ö', 'o')
                .Replace('Ö', 'O')
                .Replace('ü', 'u')
                .Replace('Ü', 'U');
        }
        public async Task<CustomResponseDto<List<UserAppDto>>> GetAllUsersAsyncWitNameCompany(string name, List<string> companies)
        {
            name = NormalizeTurkishCharacters(name);
            // Tüm kullanıcıları çekiyoruz
            var users = _userManager.Users
           .Where(u => u.isBlocked == false && u.isTestData != true && EF.Functions.Like(u.UserName, $"{name}%") && companies.Contains(u.WorkCompanyId.ToString())).ToList();

            // Kullanıcıları UserAppDto'ya map ediyoruzm
            var list = _mapper.Map<List<UserAppDto>>(users);

            // Başarılı bir yanıt dönüyoruz
            return CustomResponseDto<List<UserAppDto>>.Success(200, list);
        }

        public async Task<CustomResponseDto<List<UserAppDtoWithoutPhoto>>> GetAllUserWithOutPhoto()
        {
            var users = _userManager.Users
                .Where(e => e.isBlocked == false)
                   .Include(user => user.WorkCompany).Include(e => e.TicketDepartment)
                .Select(user => new UserAppDtoWithoutPhoto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    SAPPositionText = user.SAPPositionText,
                    DepartmentText = user.TicketDepartment.DepartmentText,
                    WorkCompanyText = user.WorkCompany.Name,
                    LastLoginDate = user.LastLoginDate ?? DateTime.MinValue,
                    isBlocked = user.isBlocked,
                    TicketDepartmentId = user.TicketDepartmentId,
                    PositionId = user.PositionId
                })
                .ToList();

            var list = _mapper.Map<List<UserAppDtoWithoutPhoto>>(users.ToList());

            return CustomResponseDto<List<UserAppDtoWithoutPhoto>>.Success(200, list);
        }
        public async Task<CustomResponseDto<List<UserAppDtoWithoutPhoto>>> GetAllUserWithOutPhotoForManagement()
        {
            // Tüm kullanıcıları çekiyoruz
            var users = _userManager.Users
                .Include(user => user.WorkCompany).Include(e => e.TicketDepartment)
                .Select(user => new UserAppDtoWithoutPhoto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName,
                    SAPPositionText = user.SAPPositionText,
                    DepartmentText = user.TicketDepartment.DepartmentText,
                    WorkCompanyText = user.WorkCompany.Name,
                    LastLoginDate = user.LastLoginDate ?? DateTime.MinValue,
                    isBlocked = user.isBlocked,
                })
                .ToList();

            var list = _mapper.Map<List<UserAppDtoWithoutPhoto>>(users.ToList());

            return CustomResponseDto<List<UserAppDtoWithoutPhoto>>.Success(200, list);
        }

        public async Task<CustomResponseDto<List<UserAppDtoOnlyNameId>>> GetAllUsersNameIdOnly()
        {
            // Tüm kullanıcıları çekiyoruz


            var users = _userManager.Users
                .Where(e=>e.isBlocked == false)
                .Select(user => new UserAppDtoOnlyNameId
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,

                })
                .ToList();

            // Kullanıcıları UserAppDto'ya map ediyoruz
            var list = _mapper.Map<List<UserAppDtoOnlyNameId>>(users.ToList());

            // Başarılı bir yanıt dönüyoruz
            return CustomResponseDto<List<UserAppDtoOnlyNameId>>.Success(200, list);
        }

        public async Task<CustomResponseDto<List<UserAppDtoOnlyNameId>>> GetAllUsersNameIdOnlyCompany(List<string> companies)
        {

            var users = _userManager.Users
                .Where(e=>companies.Contains(e.WorkCompanyId.ToString()) && e.isBlocked == false && e.isTestData != true)
                .Select(user => new UserAppDtoOnlyNameId
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,

                })
                .ToList();

            // Kullanıcıları UserAppDto'ya map ediyoruz
            var list = _mapper.Map<List<UserAppDtoOnlyNameId>>(users.ToList());

            // Başarılı bir yanıt dönüyoruz
            return CustomResponseDto<List<UserAppDtoOnlyNameId>>.Success(200, list);
        }

        public async Task<CustomResponseDto<UserAppDto>> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return CustomResponseDto<UserAppDto>.Fail(404, "UserEmail not found");
            }


            var userAppDtp = _mapper.Map<UserAppDto>(user);

            return CustomResponseDto<UserAppDto>.Success(200, userAppDtp);
        }

        public async Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);


            var roles = await _userManager.GetRolesAsync(user);
            var rolesWithIds = new List<UserRoleDto>();

            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    rolesWithIds.Add(new UserRoleDto { RoleId = role.Id, RoleName = role.Name });
                }
            }
            if (user == null)
            {
                return CustomResponseDto<UserAppDto>.Fail(404, "UserName not found");
            }
            var userAppDtp = _mapper.Map<UserAppDto>(user);

            userAppDtp.Roles = rolesWithIds;
            return CustomResponseDto<UserAppDto>.Success(200, userAppDtp);
        }

        public async Task<CustomResponseDto<UserAppDto>> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                // Kullanıcıyı bul
                var user = await _userManager.FindByIdAsync(updateUserDto.Id);
                if (user == null)
                {
                    return CustomResponseDto<UserAppDto>.Fail(404, "Kullanıcı bulunamadı");
                }

                // Kullanıcı bilgilerini güncelle
                _mapper.Map(updateUserDto, user);
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();
                    return CustomResponseDto<UserAppDto>.Fail(400, errors);
                }

                // Rolleri güncelle
                if (updateUserDto.RoleIds != null)
                {
                    // Mevcut rolleri al
                    var currentRoles = await _userManager.GetRolesAsync(user);

                    // Mevcut rolleri kaldır
                    if (currentRoles.Any())
                    {
                        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                        if (!removeResult.Succeeded)
                        {
                            return CustomResponseDto<UserAppDto>.Fail(400, "Mevcut roller silinirken hata oluştu");
                        }
                    }

                    // Yeni rolleri ekle
                    foreach (var roleId in updateUserDto.RoleIds)
                    {
                        var role = await _roleManager.FindByIdAsync(roleId.RoleId);
                        if (role != null)
                        {
                            var addRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
                            if (!addRoleResult.Succeeded)
                            {
                                return CustomResponseDto<UserAppDto>.Fail(400, "Roller eklenirken hata oluştu");
                            }
                        }
                    }
                }

                // Şifre güncellemesi
                if (!string.IsNullOrEmpty(updateUserDto.Password))
                {
                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResult = await _userManager.ResetPasswordAsync(user, resetToken, updateUserDto.Password);
                    if (!passwordResult.Succeeded)
                    {
                        var errors = passwordResult.Errors.Select(x => x.Description).ToList();
                        return CustomResponseDto<UserAppDto>.Fail(400, errors);
                    }
                }

                // Güncellenmiş kullanıcı bilgilerini döndür
                var updatedUser = await _userManager.FindByIdAsync(user.Id);
                var userAppDto = _mapper.Map<UserAppDto>(updatedUser);

                // Güncel rolleri de ekle
                var updatedRoles = await _userManager.GetRolesAsync(updatedUser);
                userAppDto.Roles = new List<UserRoleDto>();

                foreach (var roleName in updatedRoles)
                {
                    var role = await _roleManager.FindByNameAsync(roleName);
                    if (role != null)
                    {
                        userAppDto.Roles.Add(new UserRoleDto
                        {
                            RoleId = role.Id,
                            RoleName = role.Name
                        });
                    }
                }

                _unitOfWork.Commit();
                return CustomResponseDto<UserAppDto>.Success(200, userAppDto);
            }
            catch (Exception ex)
            {
                return CustomResponseDto<UserAppDto>.Fail(500, ex.Message);
            }
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return CustomResponseDto<NoContentDto>.Fail(404, "UserName not found");
            }
            var userAppDtp = _userManager.DeleteAsync(user);

            return CustomResponseDto<NoContentDto>.Success(200);
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            var user= await _userManager.FindByEmailAsync(email);
            return user!=null ? true:false;
        }

        public async Task<bool> ChangePassWord(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null ? true : false;
        }
        public async Task<bool> ResetPassword(string username, string newPassword)
        {
            try
            {
                var result = await _userManager.FindByNameAsync(username);
                var token = await _userManager.GeneratePasswordResetTokenAsync(result);
                var user = await _userManager.ResetPasswordAsync(result, token, newPassword);
                return user != null ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> SetLoginDate(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            user.LastLoginDate = DateTime.Now;

            await _userManager.UpdateAsync(user);
            return true;
        }



    }
}