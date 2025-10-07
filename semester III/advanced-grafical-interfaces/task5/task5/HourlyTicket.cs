using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    public class HourlyTicket : ITicket
    {
        public bool IsValidated { get; set; } = false;
        public string TicketType => "Godzinny";

        public void Validate()
        {
            if (!IsValidated)
            {
                IsValidated = true;
                Console.WriteLine("Bilet godzinny został skasowany.");
            }
            else
            {
                Console.WriteLine("Bilet godzinny jest już skasowany.");
            }
        }
    }
}
