using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour
{
    //private GameObject player;
    private Vector3 newPos;
    public Vector3 cameraOffset;
	public GameObject fourthWall;
	//public GameObject Player;
    
    // Use this for initialization
    void Start()
    {
        newPos = transform.position;
    }


    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        var Player = GameObject.FindGameObjectWithTag("Player");
        newPos.x = Player.transform.position.x + cameraOffset.x;
        newPos.y = Player.transform.position.y + cameraOffset.y;
        newPos.z = Player.transform.position.z + 5; //fourthWall.transform.position.z + cameraOffset.z + 2;
        transform.position = newPos;
    }
}
