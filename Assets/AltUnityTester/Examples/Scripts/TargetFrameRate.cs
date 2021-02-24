using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    public int targetFrameRate = 15;

    protected void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    protected void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

}
