using UnityEngine;
using System.Collections;

public class ResumeScript : MonoBehaviour {

	public void ResumeGame()
	{
		GameManager.GetGameRules().TogglePauseMenu();
	}
}
