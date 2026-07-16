#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

using AuthService.Domain.Enums;
using AuthService.Domain.SeedWork;

namespace AuthService.Domain.Entities
{
    public class Token : EntityBase<Guid>
    {
        public ETokenPurpose Purpose { get; set; }
        public string ValueHashed { get; set; }
        public DateTime ExpirationDate { get; set; }

        // Relationships
        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        // Reserved for EF Core
        private Token() : base()
        {
        }

        public Token(
            Guid accountId,
            ETokenPurpose purpose,
            string valueHashed,
            DateTime expirationDate)
            : base(Guid.CreateVersion7())
        {
            AccountId = accountId;

            Purpose = purpose;
            ValueHashed = valueHashed;
            ExpirationDate = expirationDate;
        }
    }
}