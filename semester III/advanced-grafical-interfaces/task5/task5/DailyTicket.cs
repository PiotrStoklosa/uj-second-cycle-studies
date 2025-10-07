using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    public class DailyTicket : ITicket
    {
        public bool IsValidated { get; set; } = false;
        public string TicketType => "Dzienny";

        public void Validate()
        {
            if (!IsValidated)
            {
                IsValidated = true;
                Console.WriteLine("Bilet dzienny został skasowany.");
            }
            else
            {
                Console.WriteLine("Bilet dzienny jest już skasowany.");
            }
        }
    }
}
