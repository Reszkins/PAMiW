namespace InpostApp.DataAccess.Services
{
    public interface IUserService 
    {
        Task HandleUser(string username, string userId);
    }

    public class UserService : IUserService
    {
        private readonly ISqlDataAccess _db;

        public UserService(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task HandleUser(string username, string userId)
        {
            var sql = "SELECT UserId FROM Users WHERE UserId = @UserId";
            var parameters = new Dictionary<string, object> { 
                { "@UserId", userId }
            };

            var result = await _db.LoadData<string>(sql, parameters);

            if(result.Count == 0)
            {
                sql = "INSERT INTO Users (UserId, Username) VALUES (@UserId, @Username)";
                parameters = new Dictionary<string, object> {
                    { "@UserId", userId },
                    { "@Username", username }
                };

                await _db.SaveData(sql, parameters);
            }
        }
    }
}
