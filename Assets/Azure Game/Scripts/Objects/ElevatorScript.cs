using UnityEngine;
using System.Collections;

public class ElevatorScript : MonoBehaviour
{
    public float delta = 0.05f;
    public Vector3 OpenPosition;
    public Vector3 ClosePosition;

    private bool open;

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
            while (transform.position != OpenPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, OpenPosition, delta);
                if (Vector3.Distance(transform.position, OpenPosition) <= delta)
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
            while (transform.position != ClosePosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, ClosePosition, delta);
                if (Vector3.Distance(transform.position, ClosePosition) <= delta)
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
