namespace Day6;

public static unsafe class Program
{
    public static void Main()
    {
        List<int> fishesStart = File.ReadAllText("../../../input").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
        DoFish(fishesStart, 80);
        DoFish(fishesStart, 256);
    }

    public static void DoFish(List<int> fishes, int days)
    {
        Span<ulong> fishArray = stackalloc ulong[9];
        Span<ulong> fishArray2 = stackalloc ulong[9];
        foreach (var fish in fishes) fishArray[fish]++;

        for (int i = 0; i < days; i++)
        {
            fishArray.CopyTo(fishArray2);
            for (int j = 0; j < 8; j++)
            {
                fishArray[j] = fishArray2[j + 1];
            }
            fishArray[8] = fishArray2[0];
            fishArray[6] += fishArray2[0];
        }

        var sum = 0ul;
        foreach (var fish in fishArray)
        {
            sum += fish;
        }
        
        Console.WriteLine(sum);
    }
}
