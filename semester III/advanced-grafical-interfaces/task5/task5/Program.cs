using _5;

List<ITicket> tickets = new List<ITicket>();
bool running = true;

while (running)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Utwórz nowy bilet");
    Console.WriteLine("2. Wyświetl wszystkie bilety");
    Console.WriteLine("3. Skasuj bilet");
    Console.WriteLine("4. Skasuj wszystkie bilety");
    Console.WriteLine("5. Wyjdź");
    Console.Write("Wybierz opcję: ");

    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            Console.WriteLine("Wybierz typ biletu:");
            Console.WriteLine("a. Jednorazowy");
            Console.WriteLine("b. Godzinny");
            Console.WriteLine("c. Dzienny");
            Console.WriteLine("d. Miesięczny");
            Console.Write("Wybierz opcję: ");
            string ticketType = Console.ReadLine();

            ITicket ticket = ticketType switch
            {
                "a" => new SingleTicket(),
                "b" => new HourlyTicket(),
                "c" => new DailyTicket(),
                "d" => new MonthlyTicket(),
                _ => null
            };

            if (ticket != null)
            {
                tickets.Add(ticket);
                Console.WriteLine($"Dodano bilet: {ticket.TicketType}");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór.");
            }
            break;

        case "2":
            if (tickets.Count == 0)
            {
                Console.WriteLine("Brak biletów.");
            }
            else
            {
                Console.WriteLine("Twoje bilety:");
                for (int i = 0; i < tickets.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tickets[i].TicketType} - {(tickets[i].IsValidated ? "Skasowany" : "Nie skasowany")}");
                }
            }
            break;

        case "3":
            Console.Write("Podaj numer biletu do skasowania: ");
            if (int.TryParse(Console.ReadLine(), out int ticketNumber) && ticketNumber > 0 && ticketNumber <= tickets.Count)
            {
                tickets[ticketNumber - 1].Validate();
            }
            else
            {
                Console.WriteLine("Nieprawidłowy numer biletu.");
            }
            break;

        case "4":
            Console.WriteLine("Kasowanie wszystkich biletów...");
            foreach (var t in tickets)
            {
                t.Validate();
            }
            break;

        case "5":
            running = false;
            Console.WriteLine("Zamykam aplikację...");
            break;

        default:
            Console.WriteLine("Nieprawidłowy wybór.");
            break;
    }
}
