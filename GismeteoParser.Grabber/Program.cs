namespace GismeteoParser.Grabber
{
    class Program
    {
        static void Main(string[] args)
        {
            var gp = new GismeteoParser();
            var p = gp.Get();
        }
    }
}
