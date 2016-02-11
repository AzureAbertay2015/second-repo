using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public Vector3 MoveAmount;
	private Vector3 OpenPosition;
	private Vector3 ClosePosition;
	//private Vector3 StartPosition;
	private bool open;

	// Use this for initialization
	void Start () {
		// open position start position + moveamount
		ClosePosition = this.transform.position;
		OpenPosition = ClosePosition + MoveAmount;
	}
	
	// Update is called once per frame
	/*
	void Update () {
	

	}
    */
    

    public void DoActivateTrigger()
    {
		if (!open)
		{
			StartCoroutine(OpenUp());
		}
		
    }

    public void DoDeactivateTrigger()
    {
		
		if (open)
		{
			StartCoroutine(Close());
		}
		
    }

    public IEnumerator OpenUp()
    {
        if (!open)
        {
            while(transform.position!=OpenPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, OpenPosition, 0.1f);
                if (Vector3.Distance(transform.position, OpenPosition) <= 0.01f)
                {
                    transform.position = OpenPosition;
                    open = true;
                }
                yield return null;
            }
        }
    }

    public IEnumerator Close()
    {
        if (open)
        {
            while(transform.position!=ClosePosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, ClosePosition, 0.1f);
                if (Vector3.Distance(transform.position, ClosePosition) <= 0.01f)
                {
                    transform.position = ClosePosition;
                    open = false;
                }
                yield return null;
            }
        }
    }

    public bool IsOpen()
    {
        return open;
    }
}
