using UnityEngine;
using System.Collections;

public class PistonPathScript : MonoBehaviour {

	public float m_Speed;
	//bool atpoint1, atpoint2;

	// Use this for initialization
	void Start()
	{
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("path"), "looptype", iTween.LoopType.none, "time", m_Speed, "easetype", iTween.EaseType.easeOutBounce, "movetopath", false, "delay", 0));
	}
}
