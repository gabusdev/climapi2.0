namespace Climapi.Core.Entities
{
    public record QueryRecord
    {
        public int Id { get; set; }

        public string Query { get; init; } = null!;

        public DateTime Time { get; set; }

        public string UserId { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
