using N5NowTestBrayanVente.Domain.Enums;

namespace N5NowTestBrayanVente.Infrastructure.Remotes
{
    public interface IKafkaProducer
    {
        Task<bool> ProduceKafkaMessage(KafkaMessageEnum kafkaMessageEnum);
    }
}
