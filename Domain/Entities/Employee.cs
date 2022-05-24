namespace Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        
        public List<Project> LedProjects { get; set; }
        
        public List<EmployeeProject> Projects { get; set; }


        public string FullName() => FirstName + " " + LastName;
    }
}