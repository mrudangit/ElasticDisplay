using System.Collections.Concurrent;
using AMPS.Client;
using log4net;

namespace ElasticDisplay
{
    public class AmpsMessageListener:MessageHandler
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(AmpsMessageListener));


        // Per topic - > [ sowKey ] -> messsage
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, Message>> _ampsMessages = new ConcurrentDictionary<string, ConcurrentDictionary<string, Message>>();

        public void invoke(Message message)
        {

            Log.Info(string.Format("Amps Message: {0}",message.Topic));
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
