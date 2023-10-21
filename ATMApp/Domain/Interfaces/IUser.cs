using ATMApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.Domain.Interfaces
{
    public interface IUser
    {
        public int Id { get; set; }
        public long CardNumber { get; set; }
        public int CardPin { get; set; }
        public long AccountNumber { get; set; }
        public string FullName { get; set; }
        public decimal AccoutnBalance { get; set; }
        public int TotalLogin { get; set; }
        public bool IsLocked { get; set; }
        public GenderType Gender { get; set; }
        public AccountType AccountType { get; set; }
    }
}
