using ATMApp.Domain.DTOs;

namespace ATMApp.Domain.Interfaces
{
    public interface IUserAccountActions
    {
        void CreateUserAccount(CreateUserAccountDto request);
        void UserAppLogin();
        void CheckBalance();
        void PlaceDeposit();
        void MakeWithdrawal();
    }
}
