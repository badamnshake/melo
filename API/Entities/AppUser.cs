namespace API.Entities
{
    public class AppUser
    {
        // very conventional to use Id as it will help with even db to set primary key Entity framework 
        public int Id { get; set; }
        public string? Username { get; set; }

        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

    }
}