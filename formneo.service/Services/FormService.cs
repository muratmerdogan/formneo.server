using AutoMapper;
using vesa.core.Models;
using vesa.core.Repositories;
using vesa.core.Services;
using vesa.core.UnitOfWorks;

namespace vesa.service.Services
{
    public class FormService : Service<Form>, IFormService
    {

        private readonly IFormRepository _formRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FormService(IGenericRepository<Form> repository, IUnitOfWork unitOfWork, IMapper mapper, IFormRepository formRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
   
            _formRepository = formRepository;

            _unitOfWork = unitOfWork;
        }
    }
}
 