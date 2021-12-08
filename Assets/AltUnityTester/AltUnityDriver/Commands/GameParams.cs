namespace Altom.AltUnityDriver.Commands
{
    public class GameParams : IGameParams
    {
        private string _game;
        private string _token;
        public string Game
        {
            get => _game;
            set => _game = value;
        }
        public string Token
        {
            get => _token;
            set => _token = value;
        }

        public GameParams(string game, string token)
        {
            Game = game;
            Token = token;
        }

    }
}