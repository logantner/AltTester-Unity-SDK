using UnityEngine.SceneManagement;

public class AltUnityLoadSceneNotification{

    public AltUnityLoadSceneNotification(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
}