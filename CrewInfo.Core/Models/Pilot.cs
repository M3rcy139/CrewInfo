namespace CrewInfo.Core.Models
{
    public class Pilot
    {
        public Guid PilotId { get; set; }

        public string FullName { get; set; }
        public string ResidenceAddress { get; set; }
        public string MobileNumber { get; set; }

        public string PassportNumber { get; set; }
        public DateTime PassportIssueDate { get; set; }
        public string PassportIssuedBy { get; set; }
        public DateTime BirthDate { get; set; }
        public string RegistrationAddress { get; set; }

        public string InnNumber { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public string MaritalStatus { get; set; }

        public int FlightHours { get; set; }
        public string Qualification { get; set; }
        public string LastTrainingLocation { get; set; }
        public DateTime LastTrainingDate { get; set; }

        public int CrewNumber { get; set; }
    }
}
