// namespace Altom.AltUnityDriver.Commands
// {
//     public class AltUnityStopCommand : AltBaseCommand
//     {
//         public AltUnityStopCommand(IDriverCommunication commHandler) : base(commHandler)
//         {
//         }
//         public void Execute()
//         {
//             try
//             {
//                 SendCommand("closeConnection");
//                 System.Threading.Thread.Sleep(1000);
//                 SocketSettings.Socket.Close();
//             }
//             catch (System.Exception exception)
//             {
//                 System.Console.WriteLine(exception.Message);
//             }
//         }
//     }
// }