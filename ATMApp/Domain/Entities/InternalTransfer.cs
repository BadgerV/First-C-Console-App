namespace ATMApp.Domain.Entities
{
    public class InternalTransfer
    {
        public decimal TransferAmount { get; set; }
        public string RecipientBankAccountNumber { get; set; } = default!;
        // public string RecipientBankAccountName { get; set; } = default!;
    }
}
