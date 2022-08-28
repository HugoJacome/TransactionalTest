using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using TransactionalTest.Interfaces;
using TransactionalTest.Models;

namespace TransactionalTest.Repositories
{
    public interface IMovementRepository
    {
        Task<Movements?> GetMovementsByIdAsync(Guid id);
        Task<List<Movements>> GetMovementsByDateAsync(DateTime movementDate);
        Task<Movements> CreateMovementsAsync(Movements movement);
        Task<bool> UpdateMovementsById(Movements movement);
        Task<Movements> UpdateMovementsPatchAsync(Guid id, JsonPatchDocument MovementDocument);
        Task<bool> DeleteMovementById(Movements movements);
    }
    public class MovementsRepository : IMovementRepository
    {
        private IAppDBContext _context;

        public MovementsRepository(IAppDBContext context)
        {
            _context = context;
        }
        public async Task<Movements> CreateMovementsAsync(Movements movement)
        {
            _context.Movements.Add(movement);
            await _context.SaveChangesAsync();
            return movement;
        }

        public async Task<bool> DeleteMovementById(Movements movement)
        {
            try
            {
                _context.Movements.Remove(movement);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<List<Movements>> GetMovementsByDateAsync(DateTime movementDate)
        {
            throw new NotImplementedException();
        }

        public async Task<Movements?> GetMovementsByIdAsync(Guid id)
        {
            return await _context.Movements.FirstOrDefaultAsync(mv => mv.MovementId == id);
        }

        public async Task<bool> UpdateMovementsById(Movements movement)
        {
            try
            {
                var entity = await _context.Movements.FirstOrDefaultAsync(c => c.MovementId == movement.MovementId);
                if (entity != null)
                {
                    entity.MovementAccount = movement.MovementAccount;
                    entity.MovementType = movement.MovementType;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Movements> UpdateMovementsPatchAsync(Guid id, JsonPatchDocument MovementDocument)
        {
            var movementQuery = await GetMovementsByIdAsync(id);
            if (movementQuery == null)
            {
                return movementQuery;
            }
            MovementDocument.ApplyTo(movementQuery);
            await _context.SaveChangesAsync();

            return movementQuery;
        }
    }
}
