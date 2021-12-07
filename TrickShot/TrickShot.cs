namespace AdventOfCode2021.TrickShot;

public class TrickShot
{
    public static void Main()
    {
        using var reader = new StreamReader(File.OpenRead("input.txt"));
        var trickShot = new TrickShot(reader);
        int? result = trickShot.GetMaxYPosition();
        Console.WriteLine(result);
    }

    private readonly TextReader _reader;

    public TrickShot(TextReader reader)
    {
        _reader = reader;
    }

    public int GetMaxYPosition()
    {
        string[] targetArea = _reader.ReadLine()!.Substring(13).Split(", ");
        string[] targetX = targetArea[0].Substring(2).Split("..");
        string[] targetY = targetArea[1].Substring(2).Split("..");
        int targetXMin = int.Parse(targetX[0]);
        int targetXMax = int.Parse(targetX[1]);
        int targetYMin = int.Parse(targetY[0]);
        int targetYMax = int.Parse(targetY[1]);

        int result = int.MinValue;

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
                int positionYMax = 0;
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
                    positionYMax = Math.Max(positionYMax, positionY);
                } while (positionX <= targetXMax && positionY >= targetYMin);

                if (reachedTarget)
                {
                    result = Math.Max(result, positionYMax);
                }
            }
        }

        return result;
    }
}
