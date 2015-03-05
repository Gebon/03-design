namespace battleships
{
    public class GameFactory
    {
        public Game Create(Map map, Ai ai)
        {
            return new Game(map, ai);
        }
    }
}