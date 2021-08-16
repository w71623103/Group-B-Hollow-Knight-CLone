using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;
    /*
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    */
    public void ChangToScene(string sceneToChangeTo)
    {
        SceneManager.LoadScene(sceneToChangeTo, LoadSceneMode.Single);
    }

}