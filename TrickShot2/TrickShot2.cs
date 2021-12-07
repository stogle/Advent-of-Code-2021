namespace AdventOfCode2021.TrickShot;

public class TrickShot2
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var trickShot = new TrickShot2(reader);
        int? result = trickShot.GetTotalDistinctVelocities();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public TrickShot2(TextReader reader)
    {
        _reader = reader;
    }

    public int GetTotalDistinctVelocities()
    {
        string[] targetArea = _reader.ReadLine()!.Substring(13).Split(", ");
        string[] targetX = targetArea[0].Substring(2).Split("..");
        string[] targetY = targetArea[1].Substring(2).Split("..");
        int targetXMin = int.Parse(targetX[0]);
        int targetXMax = int.Parse(targetX[1]);
        int targetYMin = int.Parse(targetY[0]);
        int targetYMax = int.Parse(targetY[1]);

        int result = 0;

        for (int velocityX = 1; velocityX <= targetXMax; velocityX++)
        {
            if (velocityX * (velocityX + 1) / 2 < targetXMin)
            {
                // Horizontal velocity too low to reach target area
                continue;
            }

            for (int velocityY = targetYMin; velocityY <= Math.Abs(targetYMin); velocityY++)
            {
                bool reachedTarget = false;
                int positionX = 0;
                int positionY = 0;
                int currentVelocityX = velocityX;
                int currentVelocityY = velocityY;
                do
                {
                    if (positionX >= targetXMin && positionY <= targetYMax)
                    {
                        reachedTarget = true;
                        break;
                    }

                    positionX += currentVelocityX;
                    positionY += currentVelocityY;
                    currentVelocityX = Math.Max(0, currentVelocityX - 1);
                    currentVelocityY--;
                } while (positionX <= targetXMax && positionY >= targetYMin);

                if (reachedTarget)
                {
                    result++;
                }
            }
        }

        return result;
    }
}
