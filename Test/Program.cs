using NAudio.Wave;
using System;
using System.IO;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var load_res = LithUSS.Native.initLUSS(Path.Combine(Directory.GetCurrentDirectory(), "SynthData/"), Path.Combine(Directory.GetCurrentDirectory(), "Regina/"));
            if (load_res != 0)
                throw new Exception();
            
                while (true)
                {
                    string ts = Console.ReadLine();
                    if (String.IsNullOrEmpty(ts))
                        continue;
                    var res = LithUSS.Native.synthesizeWholeText(ts, out byte[] b, 130, 100);
                    Console.WriteLine(res);
                    if (res == 0)
                    {
                        using (var wo = new WasapiOut())
                        {
                            //var wp = new BufferedWaveProvider(new WaveFormat(22000, 1));
                            //wp.AddSamples(b, 0, b.Length);
                            wo.Init(new LithUSSWaveProvider(b));
                            wo.Play();
                            while (wo.PlaybackState == PlaybackState.Playing)
                            {
                                Console.WriteLine(wo.GetPosition());
                                Thread.Sleep(1000);
                            }
                        }
                    }
                    Console.WriteLine(b);
                }
        }
    }
}
