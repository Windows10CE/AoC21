using System.Runtime.InteropServices;

namespace Day5;

public static class Program
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Line
    {
        public Coord Start;
        public Coord End;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public int X;
        public int Y;
    }
    
    public static unsafe void Main()
    {
        var raw = File.ReadAllLines("../../../input");

        List<Line> lines = new();
        
        foreach (var line in raw.Where(x => !string.IsNullOrWhiteSpace(x)))
            fixed (int* linePtr = line.Split(new [] { ",", "->" }, StringSplitOptions.TrimEntries).Select(int.Parse).ToArray())
                lines.Add(*(Line*)linePtr);

        // part 1
        var grid = new int[1000, 1000];

        foreach (var vertical in lines.Where(x => x.Start.X == x.End.X))
        {
            int y = vertical.Start.Y;
            int x = vertical.Start.X;
            int increment = vertical.Start.Y < vertical.End.Y ? 1 : -1;
            while (y != vertical.End.Y)
            {
                grid[x, y]++;
                y += increment;
            }
            grid[x, y]++;
        }
        foreach (var horizonal in lines.Where(x => x.Start.Y == x.End.Y))
        {
            int y = horizonal.Start.Y;
            int x = horizonal.Start.X;
            int increment = horizonal.Start.X < horizonal.End.X ? 1 : -1;
            while (x != horizonal.End.X)
            {
                grid[x, y]++;
                x += increment;
            }
            grid[x, y]++;
        }

        var count = 0;
        for (var i = 0; i < 1000; i++)
            for (var j = 0; j < 1000; j++)
                if (grid[i, j] > 1)
                    count++;
        
        Console.WriteLine(count);
        
        // part 2
        grid = new int[1000, 1000];
        
        foreach (var line in lines)
        {
            int x = line.Start.X;
            int y = line.Start.Y;
            
            int xinc;
            if (line.Start.X == line.End.X)
                xinc = 0;
            else if (line.Start.X < line.End.X)
                xinc = 1;
            else
                xinc = -1;
            
            int yinc;
            if (line.Start.Y == line.End.Y)
                yinc = 0;
            else if (line.Start.Y < line.End.Y)
                yinc = 1;
            else
                yinc = -1;

            while (x != line.End.X || y != line.End.Y)
            {
                grid[x, y]++;

                x += xinc;
                y += yinc;
            }
            grid[x, y]++;
        }

        count = 0;
        for (var i = 0; i < 1000; i++)
          for (var j = 0; j < 1000; j++)
            if (grid[i, j] > 1)
                count++;
        
        Console.WriteLine(count);
    }
}
