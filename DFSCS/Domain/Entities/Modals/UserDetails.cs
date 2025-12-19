namespace Domain.Entities.Modals
{
    public class UserDetails
    {
        public int Id { get; set; } = 0;
        public string? Emp_Code { get; set; }
        public string? Emp_Name { get; set; }
        public string? Emp_Location { get; set; }
        public string? Emp_Loc_Code { get; set; }
        public string? Emp_Dept_Name { get; set; }
        public string? Emp_Desig { get; set; }
        public string? Emp_Contact_No { get; set; }
        public string? Emp_Mail_Id { get; set; }
        public string? Emp_Rpt_Mng { get; set; }
        public bool Is_Resign { get; set; }
        public int Emp_Grade { get; set; }
        public string? Emp_Grade_Desc { get; set; }
        public bool Emp_Status { get; set; }
        public string? Ip_Address { get; set; }
        public int Role_Id { get; set; } = 0;
        public string? Role_Name { get; set; } = "";
        public int Inserted_Updated_By { get; set; }
        public List<RoleDetails> RoleDetailsList { get; set; } = new();
    }
}
