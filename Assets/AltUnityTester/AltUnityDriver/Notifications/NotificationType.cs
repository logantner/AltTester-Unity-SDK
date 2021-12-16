using System;
namespace Altom.AltUnityDriver.Notifications
{
    [Flags]
    public enum NotificationType
    {
        LOADSCENE = 0,
        UNLOADSCENE = 1,
        HIERARCHYCHANGED =2

    }
}