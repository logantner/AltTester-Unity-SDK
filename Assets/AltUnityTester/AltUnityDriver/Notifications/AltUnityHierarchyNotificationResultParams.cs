namespace Altom.AltUnityDriver.Notifications
{
    public class AltUnityHierarchyNotificationResultParams
    {
        public string objectName;
        public AltUnityHierarchyMode hierarchyMode;

        public AltUnityHierarchyNotificationResultParams(string objectName, AltUnityHierarchyMode hierarchyMode)
        {
            this.objectName = objectName;
            this.hierarchyMode = hierarchyMode;
        }
    }
}