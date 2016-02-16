using UnityEngine;
using System.Collections;

public class FollowPathScript : MonoBehaviour {

	private Hashtable m_PathData = new Hashtable();
	public string m_PathName;
	public iTween.LoopType m_LoopType;
	public iTween.EaseType m_EaseType;
	public bool m_MoveToPath;
	public float m_Time;
	public float m_Delay;
	public bool m_IsRunning;
	public bool m_isPaused;

	void Awake()
	{
	}

	// Use this for initialization
	void Start()
	{
		//iTween.MoveTo(gameObject, m_PathData);
		iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(m_PathName), "looptype", m_LoopType, "easetype", m_EaseType, "movetopath", m_MoveToPath, "time", m_Time,
			"delay", m_Delay, "isrunning", m_IsRunning, "ispaused", m_isPaused));
	}
}
