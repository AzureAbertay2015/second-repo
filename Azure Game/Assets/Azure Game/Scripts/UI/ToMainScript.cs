using UnityEngine;
//using UnityEngine.SceneManagement;

public class ToMainScript : MonoBehaviour {

    public void nextScene()
    {
        Time.timeScale = 1.0f;
		Application.LoadLevel("Menu Scene");
    }
}
