using System;
using System.Collections.Concurrent;
using AMPS.Client;

namespace ElasticDisplay
{
    public class AmpsService
    {
        private readonly string _ampsUrl;
        private readonly string _name;
        private Client _client;
        private readonly ConcurrentDictionary<string, AmpsMessageListener>  _topicMessageListeners = new ConcurrentDictionary<string, AmpsMessageListener>();

        public AmpsService(string url,string name)
        {
            _ampsUrl = url;
            _name = name;
        }


        public void Start()
        {
            _client = new Client(_name);
            _client.connect(_ampsUrl);
            _client.logon();
        }


        private static string CreateCorrelationId(string topic)
        {
            var guid = Guid.NewGuid().ToString();
            
            return string.Format("{0}-{1}-{2}-{3}", topic, Environment.MachineName, Environment.UserName, guid);


        }


        public string  Subscribe(string topic, string filter)
        {
            var id = CreateCorrelationId(topic);
            var command = new Command(Message.Commands.SOWAndSubscribe);

            command.setOptions(Message.Options.OOF);
            command.setOptions(Message.Options.SendKeys);
            

            command.setCorrelationId(id);
            var msgListener = new AmpsMessageListener();
            _client.sowAndSubscribe(msgListener, topic, filter);
            _topicMessageListeners[topic] = msgListener;

            return id;
        }


    }
}
