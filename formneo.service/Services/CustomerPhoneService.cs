using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;
using vesa.core.Models.CRM;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.repository;

namespace vesa.service.Services
{
	public class CustomerPhoneService : ICustomerPhoneService
	{
		private readonly AppDbContext _context;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerPhoneService(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_context = context;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CustomerPhoneDto> GetByIdAsync(Guid id)
		{
			var phone = await _context.CustomerPhones
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
			return _mapper.Map<CustomerPhoneDto>(phone);
		}

		public async Task<IEnumerable<CustomerPhoneDto>> GetByCustomerIdAsync(Guid customerId)
		{
			var phones = await _context.CustomerPhones
				.Where(x => x.CustomerId == customerId)
				.AsNoTracking()
				.ToListAsync();
			return _mapper.Map<List<CustomerPhoneDto>>(phones);
		}

		public async Task<CustomerPhoneDto> CreateAsync(CustomerPhoneInsertDto dto)
		{
			var entity = _mapper.Map<CustomerPhone>(dto);
			entity.Id = Guid.NewGuid();

			_context.CustomerPhones.Add(entity);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<CustomerPhoneDto>(entity);
		}

		public async Task<CustomerPhoneDto> UpdateAsync(CustomerPhoneUpdateDto dto)
		{
			var entity = await _context.CustomerPhones.FirstOrDefaultAsync(x => x.Id == dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<CustomerPhoneDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _context.CustomerPhones.FirstOrDefaultAsync(x => x.Id == id);
			if (entity != null)
			{
				_context.CustomerPhones.Remove(entity);
				await _unitOfWork.CommitAsync();
			}
		}

		public async Task SetPrimaryAsync(Guid customerId, Guid phoneId)
		{
			// Önce mevcut primary'yi kaldır
			var currentPrimary = await _context.CustomerPhones
				.Where(x => x.CustomerId == customerId && x.IsPrimary)
				.ToListAsync();

			foreach (var phone in currentPrimary)
			{
				phone.IsPrimary = false;
			}

			// Yeni primary'yi ayarla
			var newPrimary = await _context.CustomerPhones
				.FirstOrDefaultAsync(x => x.Id == phoneId && x.CustomerId == customerId);

			if (newPrimary != null)
			{
				newPrimary.IsPrimary = true;
			}

			await _unitOfWork.CommitAsync();
		}
	}
}
