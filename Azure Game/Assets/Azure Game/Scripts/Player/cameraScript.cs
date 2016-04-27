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
        //Make camera always face the wall and follow the player

        //Rotate camera 180 degrees in the y axis
        transform.rotation = Quaternion.Euler(0, 180, 0);

        var Player = GameObject.FindGameObjectWithTag("Player");

        //Set new camera positions based on player and wall position
        newPos.x = Player.transform.position.x + cameraOffset.x;
        newPos.y = Player.transform.position.y + cameraOffset.y;
        newPos.z = fourthWall.transform.position.z + cameraOffset.z + 10;

        transform.position = newPos;
    }
}
