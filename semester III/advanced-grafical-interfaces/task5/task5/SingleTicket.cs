using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    public class SingleTicket : ITicket
    {
        public bool IsValidated { get; set; } = false;
        public string TicketType => "Jednorazowy";

        public void Validate()
        {
            if (!IsValidated)
            {
                IsValidated = true;
                Console.WriteLine("Bilet jednorazowy został skasowany.");
            }
            else
            {
                Console.WriteLine("Bilet jednorazowy jest już skasowany.");
            }
        }
    }
}
