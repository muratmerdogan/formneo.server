using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formneo.core.Models.FormEnums;
using formneo.core.Models.PCTracking;

namespace formneo.core.DTOs.PCTrack
{
    public class PCTrackDto
    {
        public string? PCname { get; set; }
        public DateTime? ProcessTime { get; set; }
        public ProcessTypes? ProcessType { get; set; }
        public string ProcessTypeText { get; set; }
        public LoginType? LoginType { get; set; }
        public string? LoginProcessName { get; set; }
        public string? LoginId { get; set; }
        public string? SubjectLoginId { get; set; }

    }

    public class UserPcInfoIntermediateDto
    {
        public UserAppDtoOnlyNameId User { get; set; }
        public string PCname { get; set; }
    }

    public class PcTrackGraphicDto
    {
        public string AdjustedProcessTime { get; set; }
        public string PCname { get; set; }
        public LoginType LoginType { get; set; }

        public UserAppDtoOnlyNameId User { get; set; }
    }
    public class PcDto
    {
        public int Id { get; set; }
        public string PCname { get; set; }
    }
}
