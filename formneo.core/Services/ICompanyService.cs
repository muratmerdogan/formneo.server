using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.Company;
using vesa.core.Models;

namespace vesa.core.Services
{
    public interface ICompanyService:IService<Company>
    {
        Task<IEnumerable<CompanyListDto>> GetAllCompanyListWithClientName();
        Task<IEnumerable<CompanyListDto>> GetClientIdWithCompanyList(Guid clientId);
        Task<IEnumerable<CompanyListDto>> GetClientNameWithCompanyList(string clientName);
        Task<CompanyReturnId> GetCompanyNameReturnId(string companyName);
        Task<IEnumerable<CompanyNameListDto>> GetCompanyNameList(string clientName);
        
    }
}
