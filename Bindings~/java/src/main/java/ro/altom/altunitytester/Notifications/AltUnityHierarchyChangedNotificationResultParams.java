package ro.altom.altunitytester.Notifications;

public class AltUnityHierarchyChangedNotificationResultParams {
    public String objectName;
    public HierarchyMode hierarchyMode;

    public AltUnityHierarchyChangedNotificationResultParams(String objectName, LoadSceneMode hierarchyMode) {
        this.objectName = objectName;
        this.hierarchyMode = hierarchyMode;
    }
}
