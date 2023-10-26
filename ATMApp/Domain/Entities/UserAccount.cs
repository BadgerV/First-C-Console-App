using ATMApp.Domain.Enums;

namespace ATMApp.Domain.Entities
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string CardNumber { get; set; } = default!;
        public int CardPin { get; set; }
        public string AccountNumber { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public decimal AccoutnBalance { get; set; }
        public int TotalLogin { get; set; }
        public bool IsLocked { get; set; }
        public GenderType Gender { get; set; }
        public AccountType AccountType { get; set; }
    }
}
