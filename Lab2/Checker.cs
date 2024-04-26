namespace Lab2;

public class Checker
{
    public static bool IfInLimit(int dimensionality)
    {
        if (dimensionality < 2)
        {
            Console.WriteLine("Dimensionality can not be less then 2");
            return true;
        }
        if (dimensionality > 100)
        {
            Console.WriteLine("Dimensionality can not be more then 100");
            return true;
        }
        return false;
    }
}