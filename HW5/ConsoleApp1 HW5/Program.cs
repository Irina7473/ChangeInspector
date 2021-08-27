using System;
using System.IO;

namespace ConsoleApp1_HW5
{
    class Program
    {
        static void Main(string[] args)
        {
            DriveInfo[] dis = DriveInfo.GetDrives();
            try
            {
                foreach (DriveInfo di in dis)
                {
                    //Console.WriteLine("Диск {0} имеется в системе.", di.Name);                    
                    //Console.WriteLine("Диск {0} имеется в системе и его тип {1}.", di.Name, di.DriveType);
                    if (di.IsReady)
                    {
                        Console.WriteLine($"Диск {di.Name} имеется в системе с меткой тома {di.VolumeLabel}.");
                        Console.WriteLine($"общий объем пространства, измеряемый в байтах, который доступен на устройстве - { di.TotalSize}");
                        Console.WriteLine($"свободное пространство, измеряемое в байтах, доступное для использования на устройстве - {di.AvailableFreeSpace}");
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.GetType().Name);
            }
            Console.ReadKey();
        }
    }
}
