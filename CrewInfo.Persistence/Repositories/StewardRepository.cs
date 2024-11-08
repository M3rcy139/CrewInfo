using AutoMapper;
using CrewInfo.Core.Models;
using CrewInfo.Persistence.Entities;
using CrewInfo.Persistence.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CrewInfo.Persistence.Repositories
{
    public class StewardRepository : IStewardRepository
    {
        private readonly CrewInfoDbContext _context;
        private readonly IMapper _mapper;

        public StewardRepository(CrewInfoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Steward>> GetAllStewards()
        {
            var stewards = await _context.Stewards.ToListAsync();

            if (!stewards.Any())
                throw new ArgumentException("Ни одного стюарта не найдено");

            return _mapper.Map<List<Steward>>(stewards);
        }

        public async Task<Steward> GetSteward(string? fullName, string? passportNumber, string? mobileNumber)
        {
            var query = _context.Stewards.AsQueryable();

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

            var steward = await query.FirstOrDefaultAsync();

            if (steward == null)
            {
                throw new ArgumentException("Стюарт не найден");
            }

            return _mapper.Map<Steward>(steward);
        }

        public async Task<Steward> GetStewardByName(string fullName)
        {
            var steward = await _context.Stewards.FindAsync(fullName)
                ?? throw new ArgumentException("Стюарт не найден");

            return _mapper.Map<Steward>(steward);
        }

        public async Task<Steward> GetStewardByNumber(string mobileNumber)
        {
            var steward = await _context.Stewards.FindAsync(mobileNumber)
                 ?? throw new ArgumentException("Стюарт не найден");

            return _mapper.Map<Steward>(steward);
        }

        public async Task<Steward> GetStewardByPassport(string passportNumber)
        {
            var steward = await _context.Stewards.FindAsync(passportNumber)
                ?? throw new ArgumentException("Стюарт не найден");

            return _mapper.Map<Steward>(steward);
        }

        public async Task AddSteward(Steward steward)
        {
            var stewardEntity = _mapper.Map<StewardEntity>(steward);

            _context.Stewards.Add(stewardEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSteward(Guid stewardId, Steward updatedSteward)
        {
            var stewardEntity = await _context.Stewards.FindAsync(stewardId);

            if (stewardEntity == null)
            {
                throw new ArgumentException("Пилот не найден");
            }

            stewardEntity.FullName = updatedSteward.FullName;
            stewardEntity.ResidenceAddress = updatedSteward.ResidenceAddress;
            stewardEntity.MobileNumber = updatedSteward.MobileNumber;
            stewardEntity.PassportNumber = updatedSteward.PassportNumber;
            stewardEntity.PassportIssueDate = updatedSteward.PassportIssueDate;
            stewardEntity.PassportIssuedBy = updatedSteward.PassportIssuedBy;
            stewardEntity.BirthDate = updatedSteward.BirthDate;
            stewardEntity.RegistrationAddress = updatedSteward.RegistrationAddress;
            stewardEntity.InnNumber = updatedSteward.InnNumber;
            stewardEntity.InsurancePolicyNumber = updatedSteward.InsurancePolicyNumber;
            stewardEntity.MaritalStatus = updatedSteward.MaritalStatus;
            stewardEntity.CrewNumber = updatedSteward.CrewNumber;

            _context.Stewards.Update(stewardEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSteward(Guid stewardId)
        {
            var stewardEntity = await _context.Stewards.FindAsync(stewardId);

            if (stewardEntity == null)
            {
                throw new ArgumentException($"Пилот с id {stewardId} не существует");
            }

            _context.Stewards.Remove(stewardEntity);
            await _context.SaveChangesAsync();
        }
    }
}
