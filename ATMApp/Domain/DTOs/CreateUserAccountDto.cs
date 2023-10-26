using ATMApp.Domain.Enums;

namespace ATMApp.Domain.DTOs
{
    public record CreateUserAccountDto(string FullName, int CardPin, GenderType GenderType, AccountType AccountType);
}