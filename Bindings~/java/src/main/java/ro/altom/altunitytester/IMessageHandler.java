package ro.altom.altunitytester;

import ro.altom.altunitytester.Notifications.INotificationCallbacks;

public interface IMessageHandler {

    public <T> T receive(AltMessage altMessage, Class<T> type);

    public void send(AltMessage altMessage);

    public void onMessage(String message);

    public void setNotificationCallbacks(INotificationCallbacks notificationCallbacks);
}