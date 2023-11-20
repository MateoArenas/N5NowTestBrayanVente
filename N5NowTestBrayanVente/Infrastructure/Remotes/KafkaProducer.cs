using Confluent.Kafka;
using N5NowTestBrayanVente.Domain.Aggregates.KafkaAggregate;
using N5NowTestBrayanVente.Domain.Enums;
using Newtonsoft.Json;

namespace N5NowTestBrayanVente.Infrastructure.Remotes
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly ProducerConfig _producerConfig;
        public KafkaProducer()
        {
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
            };
        }

        public async Task<bool> ProduceKafkaMessage(KafkaMessageEnum kafkaMessageEnum)
        {
            try
            {
                KafkaMessage message = new KafkaMessage();
                message.Id = Guid.NewGuid();

                switch (kafkaMessageEnum)
                {
                    case KafkaMessageEnum.Required:
                        message.NameOperation = "request";
                        break;
                    case KafkaMessageEnum.Modify:
                        message.NameOperation = "“modify”";
                        break;
                    case KafkaMessageEnum.Get:
                        message.NameOperation = "get";
                        break;
                }

                var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();

                var producedMessage = new Message<Null, string>
                {
                    Value = JsonConvert.SerializeObject(message)
                };

                var deliveryReport = await producer.ProduceAsync("N5NowTest", producedMessage);

                if (deliveryReport.Status != PersistenceStatus.Persisted)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
