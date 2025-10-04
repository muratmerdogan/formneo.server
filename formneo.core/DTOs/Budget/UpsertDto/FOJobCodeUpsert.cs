using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formneo.core.DTOs.Budget.UpsertDto
{
    using System;
    using Newtonsoft.Json;

    public class JobCode
    {
        [JsonProperty("__metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("externalCode")]
        public string ExternalCode { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_ru_RU")]
        public string NameRuRu { get; set; }

        [JsonProperty("name_en_DEBUG")]
        public string NameEnDebug { get; set; }

        [JsonProperty("name_tr_TR")]
        public string NameTrTr { get; set; }

        [JsonProperty("name_en_US")]
        public string NameEnUs { get; set; }

        [JsonProperty("description_defaultValue")]
        public string DescriptionDefaultValue { get; set; }

        [JsonProperty("description_en_DEBUG")]
        public string DescriptionEnDebug { get; set; }

        [JsonProperty("description_en_US")]
        public string DescriptionEnUs { get; set; }

        [JsonProperty("description_ru_RU")]
        public string DescriptionRuRu { get; set; }

        [JsonProperty("description_tr_TR")]
        public string DescriptionTrTr { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("regularTemporary")]
        public string RegularTemporary { get; set; }

        [JsonProperty("defaultEmployeeClass")]
        public string DefaultEmployeeClass { get; set; }

        [JsonProperty("isFulltimeEmployee")]
        public bool IsFulltimeEmployee { get; set; }

        [JsonProperty("grade")]
        public string Grade { get; set; }

        [JsonProperty("jobFunction")]
        public string JobFunction { get; set; }

        [JsonProperty("cust_positionLevel")]
        public string CustPositionLevel { get; set; }

        [JsonProperty("cust_joblevelgroup")]
        public string CustJobLevelGroup { get; set; }

        [JsonProperty("cust_metin")]
        public string CustMetin { get; set; }

        [JsonProperty("cust_jobcode")]
        public string CustJobCode { get; set; }

        [JsonProperty("cust_AdinesStatus")]
        public string CustAdinesStatus { get; set; }

        [JsonProperty("cust_EmploymentType")]
        public string CustEmploymentType { get; set; }

        [JsonProperty("cust_GorevBirimTipi")]
        public string CustGorevBirimTipi { get; set; }

        [JsonProperty("cust_IsManager")]
        public bool CustIsManager { get; set; }

        [JsonProperty("cust_bolum")]
        public string CustBolum { get; set; }

        [JsonProperty("cust_ronesanskademe")]
        public string CustRonesansKademe { get; set; }

        [JsonProperty("cust_haykademe")]
        public string CustHayKademe { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

}
