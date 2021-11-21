package ro.altom.altunitytester.Commands.AltUnityCommands;

import ro.altom.altunitytester.AltMessage;

public class AltUnitySetNotificationParams extends AltMessage {
    private int notificationType;

    public static class Builder {
        int notificationType = 0;

        public Builder() {

        }

        public Builder addNotificationForLoadScene() {
            notificationType = (notificationType | NotificationType.LOADSCENE);
            return this;
        }

        public Builder addNotificationForUnloadScene() {
            notificationType = notificationType | NotificationType.UNLOADSCENE;
            return this;
        }

        public Builder addNotificationForEverything() {
            notificationType = NotificationType.ALL;
            return this;
        }

        public Builder setAllNotificationOff() {
            notificationType = NotificationType.NONE;
            return this;
        }

        public AltUnitySetNotificationParams build() {
            return new AltUnitySetNotificationParams(notificationType);
        }

    }

    AltUnitySetNotificationParams(int notificationType) {
        this.notificationType = notificationType;
    }

    public int GetNotificationType() {
        return notificationType;
    }

}