using BankInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfChina
{
    [ExportCard(CardType = "BankOfChina")]
    public class ZHCard : ICard
    {
        public double Money { get; set; }

        public void CheckOutMoney(double money)
        {
            Money -= money;
        }

        public string GetCountInfo()
        {
            return "Bank Of China";
        }

        public void SaveMoney(double money)
        {
            Money += money;
        }
    }
}
