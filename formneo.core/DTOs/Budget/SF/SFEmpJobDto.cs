using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vesa.core.DTOs.Budget.UpsertDto;

namespace vesa.core.DTOs.Budget.SF
{
    public class SFPositionAllPropertyDto
    {

        [JsonProperty("name")]
        public string name { get; set; }


        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("effectiveStartDate")]
        public string effectiveStartDate { get; set; }


    }

    public class SFPositionDto
    {
        public Metadata __metadata { get; set; }
        public string? code { get; set; }
        public DateTime? effectiveStartDate { get; set; }
        public string? cust_jobfunction { get; set; }
        public object? cust_calismaYeriTuru { get; set; }
        public string? cust_customlegalEntity { get; set; }
        public object? cust_integrationCheck { get; set; }
        public DateTime? createdDateTime { get; set; }
        public string? jobCode { get; set; }
        public object? mdfSystemVersionId { get; set; }
        public string? type { get; set; }
        public string? division { get; set; }
        public string? cust_EmpGroup { get; set; }
        public object cust_plannedEndDate { get; set; }
        public string? costCenter { get; set; }
        public object legacyPositionId { get; set; }
        public string? externalName_localized { get; set; }
        public string? mdfSystemRecordStatus { get; set; }
        public object? cust_CompanyGroup8 { get; set; }
        public bool? vacant { get; set; }
        public string? cust_locationGroup { get; set; }
        public string? externalName_tr_TR { get; set; }
        public object? cust_IseBaslamaTarihi { get; set; }
        public string? positionTitle { get; set; }
        public string? cust_legalEntityOrgCount { get; set; }
        public string? externalName_defaultValue { get; set; }
        public object? cust_incumbent { get; set; }
        public object? payGrade { get; set; }
        public object? cust_ticket { get; set; }
        public string? cust_orgChartNameText { get; set; }
        public string? mdfSystemObjectType { get; set; }
        public string? creationSource { get; set; }
        public object? cust_actualhiredate { get; set; }
        public string? cust_customORGLabel_tr_TR { get; set; }
        public string? targetFTE { get; set; }
        public string? externalName_ru_RU { get; set; }
        public string? cust_customORGLabel_defaultValue { get; set; }
        public string? cust_hasChildPosition { get; set; }
        public object? jobLevel { get; set; }
        public DateTime? createdDate { get; set; }
        public string? mdfSystemRecordId { get; set; }
        public object? cust_CompanyGroup6Org { get; set; }
        public bool? multipleIncumbentsAllowed { get; set; }
        public string? businessUnit { get; set; }
        public object? cust_parentDepartment { get; set; }
        public DateTime? lastModifiedDateTime { get; set; }
        public object? cust_businessUnitOrg { get; set; }
        public string? jobTitle { get; set; }
        public object? criticality { get; set; }
        public string? cust_customORGLabel_en_DEBUG { get; set; }
        public string cust_ronesansjoblevel { get; set; }
        public object? incumbent { get; set; }
        public object? cust_subDivisionOrg { get; set; }
        public object? cust_phisicalLocation { get; set; }
        public string? mdfSystemEntityId { get; set; }
        public string? cust_companyGroup { get; set; }
        public object? payRange { get; set; }
        public object? cust_DivisionOrg { get; set; }
        public string? regularTemporary { get; set; }
        public string? cust_customORG { get; set; }
        public object? cust_legalEntityOrg { get; set; }
        public string? cust_sub_division { get; set; }
        public string? standardHours { get; set; }
        public string? cust_departmentOrgCount { get; set; }
        public string? cust_businessUnitOrgCount { get; set; }
        public string? effectiveStatus { get; set; }
        public object? cust_sidebenefit { get; set; }
        public object? technicalParameters { get; set; }
        public string? cust_incumbentName { get; set; }
        public object? cust_PlanlananIseGiris { get; set; }
        public object? cust_ChiefPosition { get; set; }
        public object? cust_CompanyGroup4Org { get; set; }
        public string? cust_empSubGroup { get; set; }
        public string? cust_customORGLabel_en_US { get; set; }
        public string? cust_customORGLabel_ru_RU { get; set; }
        public object? cust_payGroup { get; set; }
        public DateTime? effectiveEndDate { get; set; }
        public object? positionCriticality { get; set; }
        public object? cust_positionstartdate { get; set; }
        public object? description { get; set; }
        public string? cust_parentDepartment2 { get; set; }
        public object? positionControlled { get; set; }
        public object? cust_parentDepartmentOrg { get; set; }
        public object? cust_GeoZone { get; set; }
        public object? cust_HayKademe { get; set; }
        public string? company { get; set; }
        public object? cust_departmentOrg { get; set; }
        public string? department { get; set; }
        public object? cust_ronesansKademe { get; set; }
        public string? employeeClass { get; set; }
        public object? cust_isAlani { get; set; }
        public string? cust_parentDepartmentOrgCount { get; set; }
        public string? cust_subDivisionOrgCount { get; set; }
        public string? changeReason { get; set; }
        public object? cust_workArea { get; set; }
        public DateTime? lastModifiedDate { get; set; }
        public string? lastModifiedBy { get; set; }
        public DateTime? lastModifiedDateWithTZ { get; set; }
        public string? transactionSequence { get; set; }
        public string? cust_customORGLabel_localized { get; set; }
        public string? createdBy { get; set; }
        public string? cust_divisonOrgCount { get; set; }
        public string? mdfSystemOptimisticLockUUID { get; set; }
        public string? comment { get; set; }
        public object? location { get; set; }
        public object? cust_jobFamily { get; set; }
        public string? externalName_en_US { get; set; }
        public object? externalName_en_DEBUG { get; set; }

        public string? parentPositionTxt { get; set; }
        public string? parentPositionValue { get; set; }
    }
    public class Deferred
    {
        public string Uri { get; set; }
    }

    public class ParentPosition
    {
        public Deferred Deferred { get; set; }
    }


}