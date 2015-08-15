/*
* This singleton pattern can be used to have a particular object persist between scenes in Unity, 
* and to ensure that there are not any duplicates of the object in the scene.  
* Attach this script to any GameObject/Prefab that you need to be a singleton in all scenes.  
*/

public class Singleton : MonoBehaviour 
{
  public static Singleton instance = null;
  
  void Awake()
  {
    if (instance == null) 
    {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
    }
    
    DontDestroyOnLoad(gameObject); //this class persists between scenes
  {

}

//then in another class:
public class Loader : Monobehaviour
{
  void Awake() 
  {
    if (Singleton.instance == null) 
    {
      Instantiate(Resources.Load("SingletonPrefab")); //loads the prefab if its in a folder called Resources
    }
  }
}
