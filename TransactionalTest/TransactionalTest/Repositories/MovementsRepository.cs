using Microsoft.AspNetCore.JsonPatch;
using TransactionalTest.Models;

namespace TransactionalTest.Repositories
{
    public interface IMovementRepository
    {
        Task<Movements?> GetMovementsByIdAsync(Guid id);
        Task<List<Movements>> GetMovementsByDateAsync(DateTime movementDate);
        Task<Movements> CreateMovementsAsync(Movements movements);
        void UpdateMovementsById(Movements movements);
        Task<Movements> UpdateMovementsPatchAsync(Guid id, JsonPatchDocument MovementsDocument);
        void DeleteMovementById(Movements movements);
    }
    public class MovementsRepository
    {
    }
}
