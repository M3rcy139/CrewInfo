namespace CrewInfo.Dto
{
    public record PilotRequest
    (
        string FullName,
        string ResidenceAddress,
        string MobileNumber,

        string PassportNumber,
        DateTime PassportIssueDate,
        string PassportIssuedBy,
        DateTime BirthDate, 
        string RegistrationAddress, 

        string InnNumber, 
        string InsurancePolicyNumber, 
        string MaritalStatus, 

        int FlightHours, 
        string Qualification, 
        string LastTrainingLocation, 
        DateTime LastTrainingDate, 

        int CrewNumber 
    );
}
