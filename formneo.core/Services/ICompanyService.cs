using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.DTOs.Company;
using formneo.core.Models;

namespace formneo.core.Services
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
