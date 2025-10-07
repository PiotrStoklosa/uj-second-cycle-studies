using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    public class MonthlyTicket : ITicket
    {
        public bool IsValidated { get; set; } = false;
        public string TicketType => "Miesięczny";

        public void Validate()
        {
            if (!IsValidated)
            {
                IsValidated = true;
                Console.WriteLine("Bilet miesięczny został skasowany.");
            }
            else
            {
                Console.WriteLine("Bilet miesięczny jest już skasowany.");
            }
        }
    }
}
