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
using vesa.repository;
using vesa.service.Exceptions;

namespace vesa.service.Services
{
	public class CustomerEmailService : ICustomerEmailService
	{
		private readonly AppDbContext _context;
		private readonly ICustomerRepository _customerRepository;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerEmailService(AppDbContext context, ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_context = context;
			_customerRepository = customerRepository;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CustomerEmailDto> GetByIdAsync(Guid id)
		{
			var email = await _customerRepository.GetCustomerEmailAsync(id);
			return _mapper.Map<CustomerEmailDto>(email);
		}

		public async Task<IEnumerable<CustomerEmailDto>> GetByCustomerIdAsync(Guid customerId)
		{
			var emails = await _customerRepository.GetCustomerEmailsByCustomerAsync(customerId);
			return _mapper.Map<List<CustomerEmailDto>>(emails);
		}

		public async Task<CustomerEmailDto> CreateAsync(CustomerEmailInsertDto dto)
		{
			var entity = _mapper.Map<CustomerEmail>(dto);
			entity.Id = Guid.NewGuid();

			_context.CustomerEmails.Add(entity);
			await _unitOfWork.CommitAsync();

			entity.ConcurrencyToken = _context.Entry(entity).Property<uint>("xmin").CurrentValue;
			return _mapper.Map<CustomerEmailDto>(entity);
		}

		public async Task<CustomerEmailDto> UpdateAsync(CustomerEmailUpdateDto dto)
		{
			var entity = await _context.CustomerEmails.FirstOrDefaultAsync(x => x.Id == dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			_context.CustomerEmails.Attach(entity);
			_context.Entry(entity).Property("xmin").OriginalValue = dto.ConcurrencyToken;

			try
			{
				await _unitOfWork.CommitAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new ClientSideException("Kayıt başka biri tarafından güncellendi.");
			}

			entity.ConcurrencyToken = _context.Entry(entity).Property<uint>("xmin").CurrentValue;
			return _mapper.Map<CustomerEmailDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _context.CustomerEmails.FirstOrDefaultAsync(x => x.Id == id);
			if (entity != null)
			{
				_context.CustomerEmails.Remove(entity);
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

		public async Task SetPrimaryAsync(Guid customerId, Guid emailId)
		{
			var emails = await _context.CustomerEmails.Where(x => x.CustomerId == customerId).ToListAsync();
			foreach (var email in emails)
			{
				email.IsPrimary = email.Id == emailId;
				_context.Entry(email).Property("xmin").OriginalValue = email.ConcurrencyToken;
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
