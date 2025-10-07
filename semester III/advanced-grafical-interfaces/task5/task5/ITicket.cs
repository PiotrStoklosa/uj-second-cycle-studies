public interface ITicket
{
    void Validate();
    bool IsValidated { get; set; }
    string TicketType { get; }
}
