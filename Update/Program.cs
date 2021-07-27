using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Update
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string process = args[0];

                Console.WriteLine(process);
                while (Process.GetProcessesByName(process).Length > 0)
                {
                    Process[] tmp = Process.GetProcessesByName(process);
                    foreach (Process p in tmp)
                    {
                        Console.WriteLine(p.ProcessName + " Killed");
                        p.Kill();
                    }
                    Thread.Sleep(300);
                }
                DateTime d = File.GetLastWriteTimeUtc(process + ".exe.part");
                Console.WriteLine(d);

                File.Move(process + ".exe.part", process + ".exe", true);
                File.SetLastWriteTimeUtc(process + ".exe", d);

                Process.Start(process + ".exe");
            } catch (Exception e) { Console.WriteLine(e.ToString()); }
            Console.ReadKey(true);
        }
    }
}
