using System.ComponentModel.DataAnnotations;

namespace AmsHelpdeskApi.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Description { get; set; }

        public enum TicketStatus
        {
            Open,
            InProgress,
            Closed
        }
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public int? AssignedToUserId { get; set; }
    }
}
