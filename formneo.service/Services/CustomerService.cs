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
			// Aynı kod kontrolü (tenant-aware)
			var existingCustomer = await _customerRepository.GetByCodeAsync(dto.Code);
			if (existingCustomer != null)
			{
				throw new InvalidOperationException($"'{dto.Code}' koduna sahip müşteri zaten mevcut. Lütfen farklı bir kod kullanınız.");
			}

			var entity = _mapper.Map<Customer>(dto);

			// Collections mapping
			entity.SecondaryEmails = _mapper.Map<List<CustomerEmail>>(dto.Emails ?? new List<CustomerEmailDto>());
			entity.Phones = _mapper.Map<List<CustomerPhone>>(dto.Phones ?? new List<CustomerPhoneDto>());
			entity.Notes = _mapper.Map<List<CustomerNote>>(dto.Notes ?? new List<CustomerNoteDto>());
			entity.Documents = _mapper.Map<List<CustomerDocument>>(dto.Documents ?? new List<CustomerDocument>());
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

			_mapper.Map(dto, entity);

			_customerRepository.Attach(entity);
			_customerRepository.SetConcurrencyToken(entity, dto.ConcurrencyToken);

			_customerRepository.Update(entity);
			
			try
			{
				await _unitOfWork.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}
			
			return _mapper.Map<CustomerListDto>(entity);
		}

		// Optimize edilmiş metodlar
		public async Task<CustomerPagedResultDto> GetListPagedAsync(int page = 1, int pageSize = 50, bool includeDetails = false, string search = "")
		{
			var skip = (page - 1) * pageSize;
			var totalCount = await _customerRepository.GetTotalCountAsync(search);
			
			List<Customer> customers;
			if (includeDetails)
			{
				customers = await _customerRepository.GetListWithSelectedDetailsAsync(skip, pageSize, true, true, true, true, search);
			}
			else
			{
				customers = await _customerRepository.GetListBasicAsync(skip, pageSize, search);
			}

			var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

			return new CustomerPagedResultDto
			{
				Items = _mapper.Map<List<CustomerBasicDto>>(customers),
				TotalCount = totalCount,
				Page = page,
				PageSize = pageSize,
				TotalPages = totalPages,
				HasNextPage = page < totalPages,
				HasPreviousPage = page > 1
			};
		}

		public async Task<IEnumerable<CustomerBasicDto>> GetListBasicAsync(int skip = 0, int take = 50)
		{
			var customers = await _customerRepository.GetListBasicAsync(skip, take);
			return _mapper.Map<List<CustomerBasicDto>>(customers);
		}

		public async Task<int> GetTotalCountAsync()
		{
			return await _customerRepository.GetTotalCountAsync();
		}
	}
}


