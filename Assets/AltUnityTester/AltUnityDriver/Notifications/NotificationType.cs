using System;
namespace Altom.AltUnityDriver.Notifications
{
    [Flags]
    public enum NotificationType
    {
        None = 0,
        LOADSCENE = 1,
        UNLOADSCENE = 2,
    }
}