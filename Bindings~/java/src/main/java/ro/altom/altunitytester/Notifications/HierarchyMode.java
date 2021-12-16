package ro.altom.altunitytester.Notifications;

import com.google.gson.annotations.SerializedName;

public enum HierarchyMode {
    @SerializedName("0")
    ADD,

    @SerializedName("1")
    DESTROY,

    @SerializedName("2")
    PARENTCHANGED,
}
