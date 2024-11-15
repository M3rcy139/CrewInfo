﻿using CrewInfo.Core.Models;

namespace CrewInfo.Persistence.Interfaces.Repositories
{
    public interface IPilotRepository
    {
        Task<List<Pilot>> GetAllPilots();
        Task<Pilot> GetPilot(string? fullName, string? passportNumber, string? mobileNumber);
        Task AddPilot(Pilot pilot);
        Task UpdatePilot(Guid pilotId, Pilot updatedPilot);
        Task DeletePilot(Guid pilotId);
    }
}
