using System;
using System.IO;
using System.Reflection;
using System.Threading;
using ElasticDisplay;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElasticDisplayTests
{
    [TestClass]
    public class TestAmpsSubscribe
    {

        private AmpsService _ampsService;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        [TestInitialize]
        public void Setup()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            _ampsService = new AmpsService("tcp://ubuntuamdsymphony:9005/nvfix","TestAmps");
            _ampsService.Start();
        }


        [TestMethod]
        public void TestSubscribe()
        {

           Log.Info("test");

            _ampsService.Subscribe("/mm/Instruments");

            Thread.Sleep(1000);

        }
    }
}
