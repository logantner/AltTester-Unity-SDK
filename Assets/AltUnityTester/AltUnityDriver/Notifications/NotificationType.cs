using System;
namespace Altom.AltUnityDriver.Notifications
{
    [Flags]
    public enum NotificationType
    {
        LOADSCENE = 0,
        UNLOADSCENE = 1,
        ADDOBJECT = 2,
        DESTROYOBJECT = 3,
        CHANGEOBJECTPARENT = 4

    }
}