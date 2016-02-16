using UnityEngine;
using System.Collections;

public class FollowPathScript : MonoBehaviour {

	public float m_Speed;
	//bool atpoint1, atpoint2;

	// Use this for initialization
	void Start()
	{
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("trap_path"), "looptype", iTween.LoopType.loop, "time", m_Speed, "easetype", iTween.EaseType.linear, "movetopath", false, "delay", 0));
	}
}
