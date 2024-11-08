using CrewInfo.Core.Models;

namespace CrewInfo.Persistence.Interfaces.Repositories
{
    public interface IStewardRepository
    {
        Task<List<Steward>> GetAllStewards();
        Task<Steward> GetSteward(string? fullName, string? passportNumber, string? mobileNumber);
        Task<Steward> GetStewardByName(string fullName);
        Task<Steward> GetStewardByPassport(string passportNumber);
        Task<Steward> GetStewardByNumber(string mobileNumber);
        Task AddSteward(Steward steward);
        Task UpdateSteward(Guid stewardId, Steward updatedSteward);
        Task DeleteSteward(Guid stewardId);
    }
}
