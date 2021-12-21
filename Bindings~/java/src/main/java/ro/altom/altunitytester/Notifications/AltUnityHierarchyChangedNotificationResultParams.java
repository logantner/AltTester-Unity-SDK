package ro.altom.altunitytester.Notifications;

public class AltUnityHierarchyChangedNotificationResultParams {
    public HierarchyMode hierarchyMode;

    public AltUnityHierarchyChangedNotificationResultParams(String objectName, LoadSceneMode hierarchyMode) {
        this.hierarchyMode = hierarchyMode;
    }
}
