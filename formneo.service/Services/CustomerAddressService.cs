using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vesa.core.DTOs.CRM;
using vesa.core.Models.CRM;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;
using vesa.service.Exceptions;

namespace vesa.service.Services
{
	public class CustomerAddressService : ICustomerAddressService
	{
		private readonly ICustomerAddressRepository _customerAddressRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerAddressService(ICustomerAddressRepository customerAddressRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_customerAddressRepository = customerAddressRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<IEnumerable<CustomerAddressDto>> GetByCustomerIdAsync(Guid customerId)
		{
			var addresses = await _customerAddressRepository.GetByCustomerIdAsync(customerId);
			return _mapper.Map<List<CustomerAddressDto>>(addresses);
		}

		public async Task<CustomerAddressDto> GetByIdAsync(Guid id)
		{
			var address = await _customerAddressRepository.GetDetailAsync(id);
			return _mapper.Map<CustomerAddressDto>(address);
		}

		public async Task<CustomerAddressDto> CreateAsync(CustomerAddressInsertDto dto)
		{
			var entity = _mapper.Map<CustomerAddress>(dto);
			entity.Id = Guid.NewGuid();

			await _customerAddressRepository.AddAsync(entity);
			await _unitOfWork.CommitAsync();

			var refreshed = await _customerAddressRepository.GetDetailAsync(entity.Id);
			return _mapper.Map<CustomerAddressDto>(refreshed);
		}

		public async Task<CustomerAddressDto> UpdateAsync(CustomerAddressUpdateDto dto)
		{
			var entity = await _customerAddressRepository.GetByIdAsync(dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_customerAddressRepository.Attach(entity);
			_customerAddressRepository.SetConcurrencyToken(entity, dto.ConcurrencyToken);

			_customerAddressRepository.Update(entity);

			try
			{
				await _unitOfWork.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}

			var refreshed = await _customerAddressRepository.GetDetailAsync(entity.Id);
			return _mapper.Map<CustomerAddressDto>(refreshed);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _customerAddressRepository.GetByIdAsync(id);
			if (entity != null)
			{
				_customerAddressRepository.Remove(entity);
				try
				{
					await _unitOfWork.CommitAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
				}
			}
		}

		public async Task SetDefaultBillingAsync(Guid customerId, Guid addressId)
		{
			// Önce mevcut default billing'i kaldır
			var currentDefault = await _customerAddressRepository.GetByCustomerIdAsync(customerId);

			foreach (var address in currentDefault)
			{
				address.IsDefaultBilling = false;
			}

			// Yeni default'u ayarla
			var newDefault = await _customerAddressRepository.GetByIdAsync(addressId);

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
			var currentDefault = await _customerAddressRepository.GetByCustomerIdAsync(customerId);

			foreach (var address in currentDefault)
			{
				address.IsDefaultShipping = false;
			}

			// Yeni default'u ayarla
			var newDefault = await _customerAddressRepository.GetByIdAsync(addressId);

			if (newDefault != null)
			{
				newDefault.IsDefaultShipping = true;
				newDefault.IsShipping = true; // Otomatik olarak shipping'i de aktif et
			}

			await _unitOfWork.CommitAsync();
		}
	}
}
