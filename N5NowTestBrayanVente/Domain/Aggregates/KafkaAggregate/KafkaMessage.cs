namespace N5NowTestBrayanVente.Domain.Aggregates.KafkaAggregate
{
    public class KafkaMessage
    {
        public Guid Id { get; set; }
        public string NameOperation { get; set; }
    }
}
