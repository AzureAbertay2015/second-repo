using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour
{
    //private GameObject player;
    private Vector3 newPos;
    public Vector3 cameraOffset;
	public GameObject fourthWall;
    //public GameObject Player;

    private float m_flGoalZ;
    private float m_flCurrentZ;

    // Use this for initialization
    void Start()
    {
        newPos = transform.position;
        m_flCurrentZ = m_flGoalZ = 0;
    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        var Player = GameObject.FindGameObjectWithTag("Player");
        float velocity = Player.GetComponent<Rigidbody>().velocity.magnitude;
        m_flGoalZ = (velocity / 4);

        m_flCurrentZ = Mathf.Lerp(m_flCurrentZ, m_flGoalZ, Time.deltaTime);

        newPos.x = Player.transform.position.x + cameraOffset.x;
        newPos.y = Player.transform.position.y + cameraOffset.y;
        newPos.z = fourthWall.transform.position.z + cameraOffset.z + 0 + m_flCurrentZ;
        transform.position = newPos;
    }
}
