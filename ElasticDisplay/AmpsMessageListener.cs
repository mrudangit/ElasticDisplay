using System.Collections.Concurrent;
using AMPS.Client;

namespace ElasticDisplay
{
    public class AmpsMessageListener:MessageHandler
    {

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, Message>> _ampsMessages = new ConcurrentDictionary<string, ConcurrentDictionary<string, Message>>();

        public void invoke(Message message)
        {
            var topic = message.Topic;

            
            ConcurrentDictionary<string, Message> topicDict=null;
            if (_ampsMessages.TryGetValue(topic, out topicDict))
            {
                topicDict[message.SowKey] = message;
            }
            else
            {
                var d= new ConcurrentDictionary<string, Message>();
                d[message.SowKey] = message;
                _ampsMessages[topic] = d;
            }
        }
    }
}
