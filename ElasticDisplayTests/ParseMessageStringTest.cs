using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElasticDisplayTests
{
    [TestClass]
    public class ParseMessageStringTest
    {

        [TestMethod]
        public void TestFilterString()
        {
            

        }

        [TestMethod]
        public void ParseAmpsString()
        {


       
            var l = new List<string>();
            for (int z = 0; z < 200; z++)
            {
                var s = string.Format("Key{0}=Value{0}", z);
               l.Add(s);
            }

            var data = String.Join(new string((char) 1, 1), l);

            for (int i = 0; i < 10; i++)
            {
               

                
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                
                var nv = FillFromString(data);
                stopWatch.Stop();
                

              

                var optimized = stopWatch.Elapsed;
                Console.WriteLine("RUN #:{0} Method 1: {1} Num:{2}",i, stopWatch.Elapsed,nv.Count);
               

                stopWatch.Start();
                var tokens = data.TrimEnd((char) 1).Split((char) 1)
                    .Select(s => s.Split('='))
                    .ToDictionary(key => key[0].Trim(), value => value.Count() == 2 ? value[1].Trim() : string.Empty);
                stopWatch.Stop();

                var split = stopWatch.Elapsed;

                Console.WriteLine("RUN #:{0} Method 2: {1} Num:{2}", i,stopWatch.Elapsed,tokens.Count);


                var diff = split.Ticks/ optimized.Ticks;

              

                Console.WriteLine("RUN #:{0} ======:  {1}",i,diff);
                Console.WriteLine("");

            }

        }



        private static IDictionary<string,string> FillFromString(String data)
        {

            var nv = new Dictionary<string, string>();
            
            var length = !string.IsNullOrEmpty(data) ? data.Length : 0;
            int index = 0;

            while (index < length)
            {

           

                int startIndex = index;
                int i = -1;

                while (index < length)
                {
                    var ch = data[index];

                    if (ch == '=')
                    {
                        if (i < 0)
                            i = index;
                    }
                    else if (ch == (char)1)
                    {
                        break;
                    }

                    index++;
                }

                // extract the name / value pair

                String name = null;
                String value = null;

                if (i >= 0)
                {
                    name = data.Substring(startIndex, i - startIndex);
                    value = data.Substring(i + 1, index - i - 1);
                }
                else
                {
                    value = data.Substring(startIndex, index - startIndex);
                }

                nv[name] = value;

                index++;
            }
            return nv;

        }

    }
}
