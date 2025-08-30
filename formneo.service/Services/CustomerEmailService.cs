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

namespace vesa.service.Services
{
	public class CustomerEmailService : ICustomerEmailService
	{
		private readonly AppDbContext _context;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CustomerEmailService(AppDbContext context, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_context = context;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<CustomerEmailDto> GetByIdAsync(Guid id)
		{
			var email = await _context.CustomerEmails
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
			return _mapper.Map<CustomerEmailDto>(email);
		}

		public async Task<IEnumerable<CustomerEmailDto>> GetByCustomerIdAsync(Guid customerId)
		{
			var emails = await _context.CustomerEmails
				.Where(x => x.CustomerId == customerId)
				.AsNoTracking()
				.ToListAsync();
			return _mapper.Map<List<CustomerEmailDto>>(emails);
		}

		public async Task<CustomerEmailDto> CreateAsync(CustomerEmailInsertDto dto)
		{
			var entity = _mapper.Map<CustomerEmail>(dto);
			entity.Id = Guid.NewGuid();

			_context.CustomerEmails.Add(entity);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<CustomerEmailDto>(entity);
		}

		public async Task<CustomerEmailDto> UpdateAsync(CustomerEmailUpdateDto dto)
		{
			var entity = await _context.CustomerEmails.FirstOrDefaultAsync(x => x.Id == dto.Id);
			if (entity == null) return null;

			_mapper.Map(dto, entity);
			await _unitOfWork.CommitAsync();

			return _mapper.Map<CustomerEmailDto>(entity);
		}

		public async Task DeleteAsync(Guid id)
		{
			var entity = await _context.CustomerEmails.FirstOrDefaultAsync(x => x.Id == id);
			if (entity != null)
			{
				_context.CustomerEmails.Remove(entity);
				await _unitOfWork.CommitAsync();
			}
		}

		public async Task SetPrimaryAsync(Guid customerId, Guid emailId)
		{
			// Önce mevcut primary'yi kaldır
			var currentPrimary = await _context.CustomerEmails
				.Where(x => x.CustomerId == customerId && x.IsPrimary)
				.ToListAsync();

			foreach (var email in currentPrimary)
			{
				email.IsPrimary = false;
			}

			// Yeni primary'yi ayarla
			var newPrimary = await _context.CustomerEmails
				.FirstOrDefaultAsync(x => x.Id == emailId && x.CustomerId == customerId);

			if (newPrimary != null)
			{
				newPrimary.IsPrimary = true;
			}

			await _unitOfWork.CommitAsync();
		}
	}
}
