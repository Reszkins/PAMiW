namespace InpostApp.Dtos
{
    public class SendParcelDto
    {
        public string ParcelName { get; set; } = string.Empty;
        public int FromLocker { get; set; }
        public int ToLocker { get; set; }
        public string Receiver { get; set; } = string.Empty; 
    }
}
