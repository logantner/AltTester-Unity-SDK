using System;
namespace Altom.AltUnityDriver.Notifications
{
    [Flags]
    public enum NotificationType
    {
        //Each flag except for ALL should have as value a number that is a power of 2
        None = 0,
        LOADSCENE = 1,
        UNLOADSCENE = 2,
        ADDOBJECT = 3,
        DESTROYOBJECT = 4,
        CHANGEOBJECTPARENT = 5,

        ALL = 6 //modify every time when a flag is added so the value for ALL is equal to the sum of other flag(to have all 1 in binary)
    }
}