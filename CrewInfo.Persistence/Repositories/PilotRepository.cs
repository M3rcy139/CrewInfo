using AutoMapper;
using CrewInfo.Core.Models;
using CrewInfo.Persistence.Entities;
using CrewInfo.Persistence.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrewInfo.Persistence.Repositories
{
    public class PilotRepository : IPilotRepository
    {
        private readonly CrewInfoDbContext _context;
        private readonly IMapper _mapper;

        public PilotRepository(CrewInfoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Pilot>> GetAllPilots()
        {
            var pilots = await _context.Pilots.ToListAsync();

            if (!pilots.Any())
                throw new ArgumentException("Ни одного пилота не найдено");

            return _mapper.Map<List<Pilot>>(pilots);
        }

        public async Task<Pilot> GetPilot(string? fullName, string? passportNumber, string? mobileNumber)
        {
            var query = _context.Pilots.AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
            {
                query = query.Where(p => p.FullName == fullName);
            }

            if (!string.IsNullOrEmpty(passportNumber))
            {
                query = query.Where(p => p.PassportNumber == passportNumber);
            }

            if (!string.IsNullOrEmpty(mobileNumber))
            {
                query = query.Where(p => p.MobileNumber == mobileNumber);
            }

            var pilot = await query.FirstOrDefaultAsync();

            if (pilot == null)
            {
                throw new ArgumentException("Пилот не найден");
            }

            return _mapper.Map<Pilot>(pilot);
        }

        public async Task<Pilot> GetPilotByName(string fullName)
        {
            var pilot = await _context.Pilots.FirstOrDefaultAsync(p => p.FullName == fullName)
                ?? throw new ArgumentException("Пилот не найден");

            return _mapper.Map<Pilot>(pilot);
        }

        public async Task<Pilot> GetPilotByPassport(string passportNumber)
        {
            var pilot = await _context.Pilots.FirstOrDefaultAsync(p => p.PassportNumber == passportNumber)
                ?? throw new ArgumentException("Пилот не найден");

            return _mapper.Map<Pilot>(pilot);
        }

        public async Task<Pilot> GetPilotByNumber(string mobileNumber)
        {
            var pilot = await _context.Pilots.FirstOrDefaultAsync(p => p.MobileNumber == mobileNumber)
                ?? throw new ArgumentException("Пилот не найден");

            return _mapper.Map<Pilot>(pilot);
        }

        public async Task AddPilot(Pilot pilot)
        {
            var pilotEntity = _mapper.Map<PilotEntity>(pilot);

            _context.Pilots.Add(pilotEntity);
                await _context.SaveChangesAsync();
        }

        public async Task UpdatePilot(Guid pilotId, Pilot updatedPilot)
        {
            var pilotEntity = await _context.Pilots.FindAsync(pilotId);

            if (pilotEntity == null)
            {
                throw new ArgumentException("Пилот не найден");
            }

            pilotEntity.FullName = updatedPilot.FullName;
            pilotEntity.ResidenceAddress = updatedPilot.ResidenceAddress;
            pilotEntity.MobileNumber = updatedPilot.MobileNumber;
            pilotEntity.PassportNumber = updatedPilot.PassportNumber;
            pilotEntity.PassportIssueDate = updatedPilot.PassportIssueDate;
            pilotEntity.PassportIssuedBy = updatedPilot.PassportIssuedBy;
            pilotEntity.BirthDate = updatedPilot.BirthDate;
            pilotEntity.RegistrationAddress = updatedPilot.RegistrationAddress;
            pilotEntity.InnNumber = updatedPilot.InnNumber;
            pilotEntity.InsurancePolicyNumber = updatedPilot.InsurancePolicyNumber;
            pilotEntity.MaritalStatus = updatedPilot.MaritalStatus;
            pilotEntity.FlightHours = updatedPilot.FlightHours;
            pilotEntity.Qualification = updatedPilot.Qualification;
            pilotEntity.LastTrainingLocation = updatedPilot.LastTrainingLocation;
            pilotEntity.LastTrainingDate = updatedPilot.LastTrainingDate;
            pilotEntity.CrewNumber = updatedPilot.CrewNumber;

            _context.Pilots.Update(pilotEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePilot(Guid pilotId)
        {
            var pilotEntity = await _context.Pilots.FindAsync(pilotId);

            if (pilotEntity == null)
            {
                throw new ArgumentException($"Пилот с id {pilotId} не существует");
            }

            _context.Pilots.Remove(pilotEntity);
            await _context.SaveChangesAsync();
        }
    }
}
