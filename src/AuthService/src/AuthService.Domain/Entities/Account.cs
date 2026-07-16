using AuthService.Domain.SeedWork;

namespace AuthService.Domain.Entities
{
    public class Account : EntityBase<Guid>, ISoftDelete, IUpdateable
    {
        public string? Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string? NewEmail { get; set; }
        public string? PasswordHashed { get; set; }

        public int FailedLoginAttempts { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? UnlockDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

        public void SoftDelete()
        {
            Email = null;
            IsEmailVerified = false;
            NewEmail = null;
            PasswordHashed = null;
            FailedLoginAttempts = 0;
            IsLocked = false;
            UnlockDate = null;
            UpdatedDate = null;
        }

        // Reserved for EF Core
        private Account() : base()
        {
        }

        public Account(
            string email,
            string passwordHashed)
            : base(Guid.CreateVersion7())
        {
            Email = email;
            PasswordHashed = passwordHashed;
        }
    }
}