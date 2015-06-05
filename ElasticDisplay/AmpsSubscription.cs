using System;
using System.Collections.Generic;
using System.Reactive;
using AMPS.Client;

namespace ElasticDisplay
{
    public class AmpsSubscription:ObservableBase<KeyValueMessage>
    {

        private readonly string _topic;
        private readonly string _filter;
        private readonly object _thisLock = new object();
        private readonly HashSet<IObserver<KeyValueMessage>> _subscribers = new HashSet<IObserver<KeyValueMessage>>();

        public AmpsSubscription(string topic,string filter)
        {
            _topic = topic;
            _filter = filter;
        }


        public string Topic { get { return _topic; } }
        public string Filter { get { return _filter; } }



        public void StartSubscription(AmpsService ampsService)
        {
            ampsService.Subscribe(Topic, OnMessage, Filter);
        }

        private void OnMessage(Message obj)
        {
            
            lock (_thisLock)
            {
                var msg = TransformToKeyValueMessage(obj);
                foreach (var subscriber in _subscribers)
                {
                    subscriber.OnNext(msg);
                }
            }
        }

        private KeyValueMessage TransformToKeyValueMessage(Message message)
        {
            return default(KeyValueMessage);
        }

        protected override IDisposable SubscribeCore(IObserver<KeyValueMessage> observer)
        {
            lock (_thisLock)
            {
                _subscribers.Add(observer);
            }
            return new AnonymousDisposable(() =>
            {
                lock (_thisLock)
                {
                    _subscribers.Remove(observer);
                }
            });
        }
    }
}
