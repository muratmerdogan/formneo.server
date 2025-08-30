using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vesa.core.DTOs;
using vesa.core.Models;

namespace NLayer.Core.Services
{
    public interface IGlobalServiceWithDto<Entity, Dto> where Entity : GlobalBaseEntity where Dto : class
    {
        Task<CustomResponseDto<Dto>> GetByIdAsync(string id);
        Task<CustomResponseDto<Dto>> GetByIdGuidAsync(Guid id);
        Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync();
        Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);
        Task<CustomResponseDto<Dto>> AddAsync(Dto dto);
        Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos);
        Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto dto);
        Task<CustomResponseDto<NoContentDto>> RemoveAsync(string id);
        Task<CustomResponseDto<NoContentDto>> RemoveAsyncByGuid(Guid id);
        Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids);
        Task<CustomResponseDto<Dto>> Find(Expression<Func<Entity, bool>> expression);
        Task<IQueryable<Entity>> Include();
        Task<CustomResponseDto<NoContentDto>> SoftDeleteAsync(Guid id);
    }
}


