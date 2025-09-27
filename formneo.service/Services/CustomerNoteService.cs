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
	public class CustomerNoteService : ICustomerNoteService
	{
		private readonly ICustomerNoteRepository _customerNoteRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerNoteService(ICustomerNoteRepository customerNoteRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_customerNoteRepository = customerNoteRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CustomerNoteDto> CreateAsync(CustomerNoteInsertDto dto)
		{
			var entity = _mapper.Map<CustomerNote>(dto);
			entity.Date = dto.Date == default ? DateTime.Now : dto.Date;

			await _customerNoteRepository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			return _mapper.Map<CustomerNoteDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _customerNoteRepository.GetByIdAsync(id);
			if (entity == null) return;

			// Soft delete
			entity.IsDelete = true;
			entity.UpdatedDate = DateTime.UtcNow;
			_customerNoteRepository.Update(entity);
			await _unitOfWork.CommitAsync();
		}

		public async Task<CustomerNoteDto> GetByIdAsync(Guid id)
		{
			var entity = await _customerNoteRepository.GetDetailAsync(id);
			return _mapper.Map<CustomerNoteDto>(entity);
		}

		public async Task<IEnumerable<CustomerNoteDto>> GetByCustomerIdAsync(Guid customerId)
		{
			var notes = await _customerNoteRepository.GetByCustomerIdAsync(customerId);
			return _mapper.Map<List<CustomerNoteDto>>(notes);
		}

		public async Task<IEnumerable<CustomerNoteDto>> GetListAsync()
		{
			var notes = await _customerNoteRepository.GetAllAsync();
			return _mapper.Map<List<CustomerNoteDto>>(notes.OrderByDescending(x => x.Date));
		}

		public async Task<CustomerNoteDto> UpdateAsync(CustomerNoteUpdateDto dto)
		{
			var entity = await _customerNoteRepository.GetByIdAsync(dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_customerNoteRepository.Update(entity);
			try
			{
				await _unitOfWork.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}
			
			return _mapper.Map<CustomerNoteDto>(entity);
		}
	}
}
