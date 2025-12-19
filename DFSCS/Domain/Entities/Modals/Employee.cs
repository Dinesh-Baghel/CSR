using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class Employee
    {
        public string? empCode { get; set; }
        public string? empName { get; set; }
        public string? empLocation { get; set; }
        public string? empLocCode { get; set; }
        public string? empDeptName { get; set; }
        public string? empDesig { get; set; }
        public string? empContactNo { get; set; } = "";
        public string? empMailID { get; set; }
        public string? empRptMng { get; set; }
        public string? roleFlag { get; set; }
        public bool isResign { get; set; }
        public int empGrade { get; set; }
        public string? empGradeDesc { get; set; }
        public bool empStatus { get; set; }
        public string? lastLoginDate { get; set; }
        public string? ipAddress { get; set; }
    }
}
