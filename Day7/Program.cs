using System.Runtime.InteropServices;

namespace Day7;

public static unsafe class Program
{
    public static void Main()
    {
        var raw = File.ReadAllText("../../../input").Split(',');
        // no more GC allocations past this point

        var arrayPtr = Marshal.AllocHGlobal(sizeof(int) * raw.Length);
        Span<int> crabArray = new (arrayPtr.ToPointer(), raw.Length);

        for (int i = 0; i < raw.Length; i++)
            crabArray[i] = int.Parse(raw[i]);

        // part 1
        int lowestFuel = int.MaxValue;
        for (int i = 0; i < 1000; i++)
        {
            int fuel = 0;
            foreach (var crab in crabArray)
                fuel += Math.Abs(crab - i);
            if (fuel < lowestFuel)
                lowestFuel = fuel;
        }
        
        Console.WriteLine(lowestFuel);

        // part 2
        lowestFuel = int.MaxValue;
        for (int i = 0; i < 1000; i++)
        {
            int fuel = 0;
            foreach (var crab in crabArray)
            {
                var some = Math.Abs(crab - i);
                fuel += some * (some + 1) / 2;
            }
            if (fuel < lowestFuel)
                lowestFuel = fuel;
        }
        
        Console.WriteLine(lowestFuel);
        
        Marshal.FreeHGlobal(arrayPtr);
    }
}
