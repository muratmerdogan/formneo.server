using AutoMapper;
using formneo.core.Models;
using formneo.core.Models.FormEnums;
using formneo.core.Repositories;
using formneo.core.Services;
using formneo.core.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace formneo.service.Services
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

        public async Task<Form> CreateRevisionAsync(Guid formId)
        {
            var current = await _formRepository.GetByIdStringGuidAsync(formId);
            if (current == null) throw new Exception("Form not found");
            var parentIdForFamily = current.ParentFormId ?? current.Id;
            var maxRevision = await _formRepository.Where(f => f.ParentFormId == parentIdForFamily || f.Id == parentIdForFamily)
                .MaxAsync(f => f.Revision);
            var clone = new Form
            {
                FormName = current.FormName,
                FormDescription = current.FormDescription,
                Revision = maxRevision + 1,
                FormDesign = current.FormDesign,
                IsActive = current.IsActive,
                JavaScriptCode = current.JavaScriptCode,
                FormType = current.FormType,
                FormCategory = current.FormCategory,
                FormPriority = current.FormPriority,
                WorkFlowDefinationId = current.WorkFlowDefinationId,
                ParentFormId = current.ParentFormId ?? current.Id,
                CanEdit = current.CanEdit,
                ShowInMenu = current.ShowInMenu,
                PublicationStatus = FormPublicationStatus.Draft
            };
            await _formRepository.AddAsync(clone);
            await _unitOfWork.CommitAsync();
            return clone;
        }

        public async Task<Form> PublishAsync(Guid formId)
        {
            var form = await _formRepository.GetByIdStringGuidAsync(formId);
            if (form == null) throw new Exception("Form not found");
            if (form.PublicationStatus != FormPublicationStatus.Draft)
            {
                throw new InvalidOperationException("Only Draft forms can be published.");
            }
            var parentId = form.ParentFormId ?? form.Id;
            var maxRevision = await _formRepository.Where(f => f.ParentFormId == parentId || f.Id == parentId)
                .MaxAsync(f => f.Revision);
            if (form.Revision != maxRevision)
            {
                throw new InvalidOperationException("Only the latest revision can be published.");
            }
            form.PublicationStatus = FormPublicationStatus.Published;
            await base.UpdateAsync(form);

            // Archive older revisions under same parent
            var siblings = await _formRepository.Where(f => (f.ParentFormId == parentId || f.Id == parentId) && f.Id != form.Id)
                .ToListAsync();
            foreach (var s in siblings)
            {
                if (s.PublicationStatus == FormPublicationStatus.Published)
                {
                    s.PublicationStatus = FormPublicationStatus.Archived;
                    _formRepository.Update(s);
                }
            }
            await _unitOfWork.CommitAsync();
            return form;
        }

        public async Task<IReadOnlyList<Form>> GetVersionsAsync(Guid parentFormId)
        {
            var list = await _formRepository.Where(f => f.ParentFormId == parentFormId || f.Id == parentFormId)
                .OrderBy(f => f.Revision)
                .ToListAsync();
            return list;
        }
    }
}
 