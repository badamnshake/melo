using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        // very conventional to use Id as it will help with even db to set primary key Entity framework 
        public int Id { get; set; }
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }

        // get in front is pretty important >> for automapper
        // public int GetAge()
        // {
        //     return DateOfBirth.CalculateAge();
        // }
    }
}