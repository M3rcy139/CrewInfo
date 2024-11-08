namespace CrewInfo.Dto
{
    public record StewardRequest
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

        int CrewNumber
    );
}
