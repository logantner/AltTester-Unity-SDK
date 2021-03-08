using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    private static TargetFrameRate _instance;
    public int targetFrameRate = 5;

    protected void Awake()
    {

        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }
    }
    protected void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

}
