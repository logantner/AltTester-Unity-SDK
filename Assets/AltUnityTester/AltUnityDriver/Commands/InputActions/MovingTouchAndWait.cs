using Assets.AltUnityTester.AltUnityDriver.UnityStruct;

namespace Assets.AltUnityTester.AltUnityDriver.Commands.InputActions
{
    public class MovingTouchAndWait : AltBaseCommand
    {
        Vector2[] positions;
        float duration;
        
        public MovingTouchAndWait(SocketSettings socketSettings, Vector2[] positions, float duration) : base(socketSettings)
        {
            this.positions = positions;
            this.duration = duration;
        }

        public void Execute()
        {
            new MovingTouch(SocketSettings, positions, duration).Execute();
            System.Threading.Thread.Sleep((int)duration * 1000);
            string data;
            do
            {
                Socket.Client.Send(toBytes(CreateCommand("actionFinished")));
                data = Recvall();
            } while (data == "No");
            if (data.Equals("Yes"))
                return;
            HandleErrors(data);
        }
    }
}