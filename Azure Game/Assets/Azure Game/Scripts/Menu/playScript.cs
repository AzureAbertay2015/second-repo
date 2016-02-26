using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;

public class playScript : MonoBehaviour
{
    public void NextScene(string nextScene)
    {
		Application.LoadLevel(nextScene);
    }
}
