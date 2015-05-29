using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AMPS.Client;

namespace ElasticDisplay
{
    public class AmpsService
    {
        private readonly string _ampsUrl;
        private readonly string _name;
        private Client _client;

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


        public string  Subscribe(string topic, string filter)
        {
            var guid = Guid.NewGuid().ToString();
            var command = new Command(Message.Commands.SOWAndSubscribe);
            command.setOptions(Message.Options.OOF);
            command.setOptions(Message.Options.SendKeys);
            command.setCorrelationId(guid);
            _client.sowAndSubscribe(new AmpsMessageListener(), topic, filter);

            return guid;
        }


    }
}
