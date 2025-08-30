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
	public class CustomerAddressService : ICustomerAddressService
	{
		private readonly AppDbContext _context;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerAddressService(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_context = context;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CustomerAddressDto> GetByIdAsync(Guid id)
		{
			var address = await _context.CustomerAddresses
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
			return _mapper.Map<CustomerAddressDto>(address);
		}

		public async Task<IEnumerable<CustomerAddressDto>> GetByCustomerIdAsync(Guid customerId)
		{
			var addresses = await _context.CustomerAddresses
				.Where(x => x.CustomerId == customerId)
				.AsNoTracking()
				.ToListAsync();
			return _mapper.Map<List<CustomerAddressDto>>(addresses);
		}

		public async Task<CustomerAddressDto> CreateAsync(CustomerAddressInsertDto dto)
		{
			var entity = _mapper.Map<CustomerAddress>(dto);
			entity.Id = Guid.NewGuid();

			_context.CustomerAddresses.Add(entity);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<CustomerAddressDto>(entity);
		}

		public async Task<CustomerAddressDto> UpdateAsync(CustomerAddressUpdateDto dto)
		{
			var entity = await _context.CustomerAddresses.FirstOrDefaultAsync(x => x.Id == dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<CustomerAddressDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _context.CustomerAddresses.FirstOrDefaultAsync(x => x.Id == id);
			if (entity != null)
			{
				_context.CustomerAddresses.Remove(entity);
				await _unitOfWork.CommitAsync();
			}
		}

		public async Task SetDefaultBillingAsync(Guid customerId, Guid addressId)
		{
			// Önce mevcut default billing'i kaldır
			var currentDefault = await _context.CustomerAddresses
				.Where(x => x.CustomerId == customerId && x.IsDefaultBilling)
				.ToListAsync();

			foreach (var address in currentDefault)
			{
				address.IsDefaultBilling = false;
			}

			// Yeni default'u ayarla
			var newDefault = await _context.CustomerAddresses
				.FirstOrDefaultAsync(x => x.Id == addressId && x.CustomerId == customerId);

			if (newDefault != null)
			{
				newDefault.IsDefaultBilling = true;
				newDefault.IsBilling = true; // Otomatik olarak billing'i de aktif et
			}

			await _unitOfWork.CommitAsync();
		}

		public async Task SetDefaultShippingAsync(Guid customerId, Guid addressId)
		{
			// Önce mevcut default shipping'i kaldır
			var currentDefault = await _context.CustomerAddresses
				.Where(x => x.CustomerId == customerId && x.IsDefaultShipping)
				.ToListAsync();

			foreach (var address in currentDefault)
			{
				address.IsDefaultShipping = false;
			}

			// Yeni default'u ayarla
			var newDefault = await _context.CustomerAddresses
				.FirstOrDefaultAsync(x => x.Id == addressId && x.CustomerId == customerId);

			if (newDefault != null)
			{
				newDefault.IsDefaultShipping = true;
				newDefault.IsShipping = true; // Otomatik olarak shipping'i de aktif et
			}

			await _unitOfWork.CommitAsync();
		}
	}
}
