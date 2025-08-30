namespace vesa.api.Controllers.PostDto
{
    using System;

    namespace PositionApi.Models
    {
        public class Metadata_Post
        {
            public string uri { get; set; }
        }

        public class ParentPosition_Post
        {
            public Metadata_Post __metadata { get; set; }
        }

        public class Position_Post
        {
            public Metadata_Post __metadata { get; set; }
            public string code { get; set; }
            public DateTime effectiveStartDate { get; set; }
            public DateTime? cust_IseBaslamaTarihi { get; set; }
            public DateTime? cust_PlanlananIseGiris { get; set; }
            public DateTime? cust_plannedEndDate { get; set; }
            public DateTime? cust_actualhiredate { get; set; }
            public string effectiveStatus { get; set; }
            public bool vacant { get; set; }
            public string changeReason { get; set; }
            public string cust_GeoZone { get; set; }
            public string company { get; set; }
            public string externalName_tr_TR { get; set; }
            public string externalName_defaultValue { get; set; }
            public string externalName_ru_RU { get; set; }
            public string externalName_en_US { get; set; }
            public string externalName_en_DEBUG { get; set; }
            public bool multipleIncumbentsAllowed { get; set; }
            public string type { get; set; }
            public string targetFTE { get; set; }
            public string standardHours { get; set; }
            public string jobCode { get; set; }
            public string cust_jobfunction { get; set; }
            public string cust_ronesansjoblevel { get; set; }
            public string cust_ronesansKademe { get; set; }
            public string payGrade { get; set; }
            public string jobTitle { get; set; }
            public string employeeClass { get; set; }
            public string cust_empSubGroup { get; set; }
            public string cust_EmpGroup { get; set; }
            public string cust_companyGroup { get; set; }
            public string cust_customlegalEntity { get; set; }
            public string businessUnit { get; set; }
            public string division { get; set; }
            public string cust_sub_division { get; set; }
            public string department { get; set; }
            public string cust_parentDepartment2 { get; set; }
            public string cust_parentDepartment { get; set; }
            public string costCenter { get; set; }
            public string cust_locationGroup { get; set; }
            public string location { get; set; }
            public string cust_calismaYeriTuru { get; set; }
            public string comment { get; set; }
            public string cust_payGroup { get; set; }
            public string cust_isAlani { get; set; }
            public string cust_phisicalLocation { get; set; }
            public string cust_ticket { get; set; }
            public string cust_HayKademe { get; set; }
            public bool cust_ChiefPosition { get; set; }
            public ParentPosition_Post   parentPosition { get; set; }
        }
    }

}
