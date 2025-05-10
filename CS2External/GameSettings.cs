namespace CS2External
{
    public class GameSettings : IGameSettings
    {
        public bool IsBunnyHopEnabled { get; set; }
        public bool IsAntiFlashEnabled { get; set; }
        public int Fov { get; set; } = 60;
    }

    public interface IGameSettings
    {
        bool IsBunnyHopEnabled { get; set; }
        bool IsAntiFlashEnabled { get; set; }
        int Fov { get; set; }
    }
}