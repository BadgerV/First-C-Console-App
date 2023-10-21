using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ATMApp.UI
{
    public class GeneratorClass
    {

        private List<long> CardNumberList = new List<long>();
        private List<long> AccountNumberlist = new List<long>();
        private List<int> IdList = new List<int>();



        Random random = new Random();


        public long GenerateAccountNumber()
        {
            var ran = GenerateUniqueNumber(AccountNumberlist);
            AccountNumberlist.Add(ran);
            return ran;
        }

        public long GenerateCardNumber()
        {
            var ran = GenerateUniqueNumber(CardNumberList);
            CardNumberList.Add(ran);
            return ran;
        }

        public int GenerateId()
        {
            var ran = GenerateUniqueId(IdList);
            IdList.Add(ran);
            return ran;
        }




        private long GenerateUniqueNumber (List<long> list)
        {
            long randomNumber;

            do
            {
                randomNumber = random.Next(100000, 999999);
            } while (list.Contains(randomNumber));

            return randomNumber;
        }

        private int GenerateUniqueId (List<int> list)
        {
            int randomNumber;

            do
            {
                randomNumber = (int)random.Next(00, 1000);
            } while (list.Contains(randomNumber));
            return randomNumber;
        }

    }
}
