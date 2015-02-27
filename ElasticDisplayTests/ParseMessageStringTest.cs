using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElasticDisplayTests
{
    [TestClass]
    public class ParseMessageStringTest
    {
        [TestMethod]
        public void ParseAmpsString()
        {


            var data =
                @"35=CONTAINER_DEPTH1=USD.CM_DEPTH.INSIGHT_OUTRIGHT_FUTURE_EMEA.GE_BF_U9_H0_U02=INSIGHT_OUTRIGHT_FUTURE_EMEA352=1127=1518=0499=0516=0497=0513=0508=0506=0505=0503=0502=0501=0500=0476=0475=0474=0473=0470=0468=0460=0452=0437=0435=0496=0495=0494=0493=0489=0486=0485=0481=0432=0430=0428=0426=0425=0424=0423=0422=0421=0354=0344=346=347=348=350=355=356=357=360=361=362=363=364=365=366=367=368=369=370=371=373=374=375=376=377=380=381=382=383=384=386=387=390=391=392=394=395=396=397=399=102=0325=0319=0331=0326=0311=328=0351=0329=0478=0353=0337=338=0339=0";


            for (int i = 0; i < 10; i++)
            {
                var d = new Dictionary<string, string>();


                var stopWatch = new Stopwatch();

                stopWatch.Start();
                FillFromString(data, d);
                stopWatch.Stop();

                var optimized = stopWatch.Elapsed;
                Console.WriteLine("RUN #:{0} Method 1: {1} ",i, stopWatch.Elapsed);
               

                stopWatch.Start();
                var tokens = data.TrimEnd((char) 1).Split((char) 1)
                    .Select(s => s.Split('='))
                    .ToDictionary(key => key[0].Trim(), value => value.Count() == 2 ? value[1].Trim() : string.Empty);
                stopWatch.Stop();

                var split = stopWatch.Elapsed;

                Console.WriteLine("RUN #:{0} Method 2: {1} ", i,stopWatch.Elapsed);


                var diff = split.Ticks/ optimized.Ticks;

              

                Console.WriteLine("RUN #:{0} ======:  {1}",i,diff);
                Console.WriteLine("");

            }

        }



        private static void FillFromString(String s,IDictionary<string,string> nameValueDictionary )
        {
            int l = (s != null) ? s.Length : 0;
            int i = 0;

            while (i < l)
            {

           

                int si = i;
                int ti = -1;

                while (i < l)
                {
                    char ch = s[i];

                    if (ch == '=')
                    {
                        if (ti < 0)
                            ti = i;
                    }
                    else if (ch == (char)1)
                    {
                        break;
                    }

                    i++;
                }

                // extract the name / value pair

                String name = null;
                String value = null;

                if (ti >= 0)
                {
                    name = s.Substring(si, ti - si);
                    value = s.Substring(ti + 1, i - ti - 1);
                }
                else
                {
                    value = s.Substring(si, i - si);
                }

           


                nameValueDictionary[name] = value;

              

                i++;
            }
        }

    }
}
