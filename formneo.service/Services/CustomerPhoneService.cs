using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using formneo.core.DTOs.CRM;
using formneo.core.Models.CRM;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using formneo.service.Exceptions;

namespace formneo.service.Services
{
	public class CustomerPhoneService : ICustomerPhoneService
	{
		private readonly ICustomerPhoneRepository _customerPhoneRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerPhoneService(ICustomerPhoneRepository customerPhoneRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_customerPhoneRepository = customerPhoneRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CustomerPhoneDto> GetByIdAsync(Guid id)
		{
			var phone = await _customerPhoneRepository.GetDetailAsync(id);
			return _mapper.Map<CustomerPhoneDto>(phone);
		}

		public async Task<IEnumerable<CustomerPhoneDto>> GetByCustomerIdAsync(Guid customerId)
		{
			var phones = await _customerPhoneRepository.GetByCustomerIdAsync(customerId);
			return _mapper.Map<List<CustomerPhoneDto>>(phones);
		}

		public async Task<CustomerPhoneDto> CreateAsync(CustomerPhoneInsertDto dto)
		{
			var entity = _mapper.Map<CustomerPhone>(dto);
			entity.Id = Guid.NewGuid();

			await _customerPhoneRepository.AddAsync(entity);
			await _unitOfWork.CommitAsync();

			entity.ConcurrencyToken = _customerPhoneRepository.GetConcurrencyToken(entity);
			return _mapper.Map<CustomerPhoneDto>(entity);
		}

		public async Task<CustomerPhoneDto> UpdateAsync(CustomerPhoneUpdateDto dto)
		{
			var entity = await _customerPhoneRepository.GetDetailAsync(dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_customerPhoneRepository.Attach(entity);
			_customerPhoneRepository.SetConcurrencyToken(entity, dto.ConcurrencyToken);

			_customerPhoneRepository.Update(entity);

			try
			{
				await _unitOfWork.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}

			entity.ConcurrencyToken = _customerPhoneRepository.GetConcurrencyToken(entity);
			return _mapper.Map<CustomerPhoneDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _customerPhoneRepository.GetDetailAsync(id);
			if (entity != null)
			{
				_customerPhoneRepository.Attach(entity);
				_customerPhoneRepository.SetConcurrencyToken(entity, entity.ConcurrencyToken);
				_customerPhoneRepository.Remove(entity);
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

		public async Task SetPrimaryAsync(Guid customerId, Guid phoneId)
		{
			var phones = await _customerPhoneRepository.GetByCustomerIdAsync(customerId);
			foreach (var phone in phones)
			{
				phone.IsPrimary = phone.Id == phoneId;
				_customerPhoneRepository.Attach(phone);
				_customerPhoneRepository.SetConcurrencyToken(phone, phone.ConcurrencyToken);
				_customerPhoneRepository.Update(phone);
			}

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
}
