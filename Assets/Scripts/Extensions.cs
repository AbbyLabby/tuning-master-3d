
public static class Extensions
{
    public static float Remap(this float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }

    public static float CalculatePercentage(float min, float max, float percentage)
    {
        return (max - min) / 100 * percentage + min;
    }
}
