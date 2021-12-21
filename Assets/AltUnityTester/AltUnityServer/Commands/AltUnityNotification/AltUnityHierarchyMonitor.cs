using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace Altom.AltUnityTester.Notification
{
    // [ExecuteAlways]
    public class AltUnityHierarchyMonitor : MonoBehaviour
    {
        List<GameObject> hierarchyObjects = new List<GameObject>();
        List<GameObject> hierarchyChildrenObjects = new List<GameObject>();
        List<GameObject> hierarchyObjectsUpdated = new List<GameObject>();
        public int hierarchyCount = 0;

        private void Start() 
        {
            AddElementsToHierarchy();
        }
        public void Update()
        {
            Stopwatch watch = Stopwatch.StartNew();
            AddElementsToHierarchy();
            CheckHierarchyObjects();
            UnityEngine.Debug.Log("Time elapsed: "+ watch.Elapsed.TotalSeconds);
        }
        public void CheckHierarchyObjects()
        {
            if(hierarchyObjects.Count!=0)
            {
                if(hierarchyObjects.Count > hierarchyObjectsUpdated.Count)
                {
                    AltUnityHierarchyChangedNotification.onHierarchyChanged("destroy"); 
                    UnityEngine.Debug.Log("hierarchy has changed: object was destroyed");
                }
                else if(hierarchyObjects.Count < hierarchyObjectsUpdated.Count)
                {
                    AltUnityHierarchyChangedNotification.onHierarchyChanged("added"); 
                    UnityEngine.Debug.Log("hierarchy has changed: object was added");
                }
                else if(hierarchyObjects != hierarchyObjectsUpdated)
                {
                    for(int i=0; i < hierarchyObjects.Count; i++)
                    {
                        if(hierarchyObjects[i] != hierarchyObjectsUpdated[i])
                        {
                            AltUnityHierarchyChangedNotification.onHierarchyChanged("parent changed"); 
                            UnityEngine.Debug.Log("a parent was changed");
                            break;
                        }
                    }
                }
            }
            hierarchyObjects = new List<GameObject>(hierarchyObjectsUpdated);

        }
        public void AddElementsToHierarchy()
        {
            hierarchyObjectsUpdated.Clear();
            for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
            {
                foreach (UnityEngine.GameObject rootGameObject in UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).GetRootGameObjects())
                {
                    hierarchyObjectsUpdated.Add(rootGameObject);
                }
            }
            foreach (var destroyOnLoadObject in AltUnityRunner.GetDontDestroyOnLoadObjects())
            {
                hierarchyObjectsUpdated.Add(destroyOnLoadObject);

            }
            foreach (UnityEngine.GameObject gameObject in hierarchyObjectsUpdated)
            {
                AddChildrenToHierarchy(gameObject);
            }
            hierarchyObjectsUpdated.AddRange(hierarchyChildrenObjects);
            hierarchyChildrenObjects.Clear();
            
        }

        public void AddChildrenToHierarchy(GameObject gameObject) 
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                hierarchyChildrenObjects.Add(gameObject.transform.GetChild(i).gameObject);
                AddChildrenToHierarchy(gameObject.transform.GetChild(i).gameObject);
            }
        }
        

    }
    

}