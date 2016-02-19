using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {

	public float m_RotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAroundLocal(transform.forward, m_RotationSpeed * Time.deltaTime);
	}
}
