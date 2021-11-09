namespace Altom.AltUnityDriver.Commands
{
    public interface IDriverCommunication
    {
        void Send(CommandParams param);
        T Recvall<T>(CommandParams param);
        void Connect();
        void Close();
    }
}