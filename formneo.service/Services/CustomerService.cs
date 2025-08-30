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
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_customerRepository = customerRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CustomerListDto> CreateAsync(CustomerInsertDto dto)
		{
			var entity = _mapper.Map<Customer>(dto);

			// Collections mapping
			entity.SecondaryEmails = _mapper.Map<List<CustomerEmail>>(dto.Emails ?? new List<CustomerEmailDto>());
			entity.Phones = _mapper.Map<List<CustomerPhone>>(dto.Phones ?? new List<CustomerPhoneDto>());
			entity.Notes = _mapper.Map<List<CustomerNote>>(dto.Notes ?? new List<CustomerNoteDto>());
			entity.Tags = _mapper.Map<List<CustomerTag>>(dto.Tags ?? new List<string>());
			entity.Documents = _mapper.Map<List<CustomerDocument>>(dto.Documents ?? new List<string>());
			entity.Sectors = _mapper.Map<List<CustomerSector>>(dto.Sectors ?? new List<string>());
			entity.CustomFields = _mapper.Map<List<CustomerCustomField>>(dto.CustomFields ?? new List<CustomFieldDto>());
			entity.Addresses = _mapper.Map<List<CustomerAddress>>(dto.Addresses ?? new List<CustomerAddressDto>());
			entity.Officials = _mapper.Map<List<CustomerOfficial>>(dto.Officials ?? new List<CustomerOfficialDto>());

			await _customerRepository.AddAsync(entity);
			await _unitOfWork.CommitAsync();
			return _mapper.Map<CustomerListDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _customerRepository.GetDetailAsync(id);
			if (entity == null) return;

			// Soft delete
			entity.IsDelete = true;
			entity.UpdatedDate = DateTime.UtcNow;
			_customerRepository.Update(entity);
			await _unitOfWork.CommitAsync();
		}

		public async Task<CustomerListDto> GetByIdAsync(Guid id)
		{
			var entity = await _customerRepository.GetDetailAsync(id);
			return _mapper.Map<CustomerListDto>(entity);
		}

		public async Task<IEnumerable<CustomerListDto>> GetListAsync()
		{
			var customers = await _customerRepository.GetListWithDetailsAsync();
			return _mapper.Map<List<CustomerListDto>>(customers);
		}

		public async Task<CustomerListDto> UpdateAsync(CustomerUpdateDto dto)
		{
			var entity = await _customerRepository.GetDetailAsync(dto.Id);
			if (entity == null) return null;
			if (!(dto.RowVersion == null || (entity.RowVersion != null && entity.RowVersion.SequenceEqual(dto.RowVersion))))
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");

			// primitive fields
			_mapper.Map(dto, entity);

			// collections: replace with mapped collections
			entity.SecondaryEmails = _mapper.Map<List<CustomerEmail>>(dto.Emails ?? new List<CustomerEmailDto>());
			entity.Phones = _mapper.Map<List<CustomerPhone>>(dto.Phones ?? new List<CustomerPhoneDto>());
			entity.Notes = _mapper.Map<List<CustomerNote>>(dto.Notes ?? new List<CustomerNoteDto>());
			entity.Tags = _mapper.Map<List<CustomerTag>>(dto.Tags ?? new List<string>());
			entity.Documents = _mapper.Map<List<CustomerDocument>>(dto.Documents ?? new List<string>());
			entity.Sectors = _mapper.Map<List<CustomerSector>>(dto.Sectors ?? new List<string>());
			entity.CustomFields = _mapper.Map<List<CustomerCustomField>>(dto.CustomFields ?? new List<CustomFieldDto>());

			_customerRepository.Update(entity);
			await _unitOfWork.CommitAsync();
			return _mapper.Map<CustomerListDto>(entity);
		}
	}
}


