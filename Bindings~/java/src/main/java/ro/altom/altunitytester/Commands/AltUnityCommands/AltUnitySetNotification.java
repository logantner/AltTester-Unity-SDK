package ro.altom.altunitytester.Commands.AltUnityCommands;

import ro.altom.altunitytester.IMessageHandler;
import ro.altom.altunitytester.Commands.AltBaseCommand;

public class AltUnitySetNotification extends AltBaseCommand {
    private AltUnitySetNotificationParams cmdParams;

    public AltUnitySetNotification(IMessageHandler messageHandler,
            AltUnitySetNotificationParams altUnitySetNotificationParams) {
        super(messageHandler);
        this.cmdParams = altUnitySetNotificationParams;
    }

    public void Execute() {
        cmdParams.setCommandName("setNotification");
        SendCommand(cmdParams);
        recvall(cmdParams, String.class);
    }
}
