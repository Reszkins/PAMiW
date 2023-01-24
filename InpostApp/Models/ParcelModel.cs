namespace InpostApp.Models
{
    public class ParcelModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int FromLockerId { get; set; }
        public int ToLockerId { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
