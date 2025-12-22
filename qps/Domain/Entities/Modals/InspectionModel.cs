using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class InspectionModel
    {
        public long InspectId { get; set; }          // 0 = Insert, >0 = Update
        public long InspectionId { get; set; }
        public string InspectorCode { get; set; } = string.Empty;
        public string GptNo { get; set; } = string.Empty;
        public int GptStatus { get; set; }
        public string? GptApprovedBy { get; set; } = string.Empty;
        public DateTime? GptApprovedDate { get; set; }

        public int NoOfBagsCartonsQty { get; set; }
        public int NoOfBagsCartonsUnit { get; set; }
        public int PackingStatus { get; set; }
        public int InlineDone { get; set; }
        public int PackingList { get; set; }

        public DateTime? PpsReleaseDate { get; set; }
        public DateTime? PpsExpiryDate { get; set; }

        public string FiberContentOnPo { get; set; } = string.Empty;
        public string FiberContentOnTestReport { get; set; } = string.Empty;
        public string FiberContentOnWc { get; set; } = string.Empty;

        public string GsmOnPo { get; set; } = string.Empty;
        public string GsmOnTestReport { get; set; } = string.Empty;
        public string? GsmOnBulk { get; set; } = string.Empty;

        public int MeasurementSampleSize { get; set; }
        public string? MeasurementAttachment { get; set; } = string.Empty;

        public int NoOfCheck { get; set; }
        public int NoOfCheckUnit { get; set; }
        public int? NoOfPrepackCheck { get; set; }

        public string? Workmanship { get; set; } = string.Empty;
        public string? Aql { get; set; } = string.Empty;
        public string? AqlSampleSize { get; set; } = string.Empty;

        public int DefectsAllowed { get; set; }
        public int DefectsStatus { get; set; }
        public int InspectionResult { get; set; }

        public string? RemarkComment { get; set; } = string.Empty;
        public string? Note { get; set; } = string.Empty;
        public string NameOfFactoryRepresentative { get; set; } = string.Empty;

        public string? TestReportAttachment { get; set; } = string.Empty;
        public string? GenAttachmentFirst { get; set; } = string.Empty;
        public string? GenAttachmentSecond { get; set; } = string.Empty;

        public bool IsComplete { get; set; }
        public int UserId { get; set; }

        public List<InspectionDefectDto> Defects { get; set; } = new();

        public ICollection<MasterData> PassFailGPTStatus { get; set; } = new List<MasterData>();
        public ICollection<MasterData> YesNoPackingStatus { get; set; } = new List<MasterData>();
        public ICollection<MasterData> YesNoInlineDone { get; set; } = new List<MasterData>();
        public ICollection<MasterData> YesNoPackingList { get; set; } = new List<MasterData>();
        public ICollection<MasterData> PassFailMeasurementSampleSize { get; set; } = new List<MasterData>();
        public ICollection<MasterData> UnitNoOfBagCarton { get; set; } = new List<MasterData>();
        public ICollection<MasterData> UnitNoOfCheck { get; set; } = new List<MasterData>();
        public ICollection<MasterData> PassFailDefectsStatus { get; set; } = new List<MasterData>();
        public ICollection<MasterData> PassFailInspectionResult { get; set; } = new List<MasterData>();

    }

    public class InspectionDefectDto
    {
        public int Defects_Id { get; set; }
        public string Defects { get; set; } = string.Empty;
        public string? Dimension { get; set; } = string.Empty;
        public int Major { get; set; }
        public int Minor { get; set; }
        public Guid DefectId { get; set; } = new Guid();
    }
}
