using InpostApp.Dtos;
using InpostApp.Models;

namespace InpostApp.DataAccess.Services
{
    public interface IParcelService
    {
        Task<List<ParcelModel>> GetUserParcels(string userId);
        Task<List<ParcelLockerModel>> GetParcelLockers();
        Task<List<UserModel>> GetUsers();
        Task AddParcel(SendParcelDto dto, string userId);
    }
    public class ParcelService : IParcelService
    {
        private readonly ISqlDataAccess _db;

        public ParcelService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<ParcelModel>> GetUserParcels(string userId)
        {
            var sql = "SELECT * FROM Parcels WHERE SenderId = @UserId OR ReceiverId = @UserId";
            var parameters = new Dictionary<string, object> {
                { "@UserId", userId }
            };

            var parcels = await _db.LoadData<ParcelModel>(sql, parameters);
            return parcels;
        }

        public async Task<List<ParcelLockerModel>> GetParcelLockers()
        {
            var sql = "SELECT * FROM ParcelLockers";

            var parcelLockers = await _db.LoadData<ParcelLockerModel>(sql, new Dictionary<string, object>());
            return parcelLockers;
        }
            
        public async Task<List<UserModel>> GetUsers()
        {
            var sql = "SELECT * FROM Users";

            var users = await _db.LoadData<UserModel>(sql, new Dictionary<string, object>());
            return users;
        }

        public async Task AddParcel(SendParcelDto dto, string userId)
        {
            var sql = "INSERT INTO Parcels (Name, FromLockerId, ToLockerId, SenderId, ReceiverId, State)" +
                " VALUES (@Name, @FromLockerId, @ToLockerId, @SenderId, @ReceiverId, @State)";

            var parameters = new Dictionary<string, object> {
                { "@Name", dto.ParcelName },
                { "@FromLockerId", dto.FromLocker },
                { "@ToLockerId", dto.ToLocker },
                { "@SenderId", userId },
                { "@ReceiverId", dto.Receiver },
                { "@State", "Posted" }
            };

            await _db.SaveData(sql, parameters);
        }
    }
}
