using UnityEngine;
using System.Collections;

public class LeverScript : MonoBehaviour {
    
    public Vector3 UpTranslation;
    public Vector3 DownTranslation;

    private bool down;

    public void DoActivateTrigger()
    {
        if (!down)
        {
            StartCoroutine(SwitchDown());
        }
        else
        {
            StartCoroutine(SwitchUp());
        }

    }
    
    public void DoDeactivateTrigger()
    {
        
    }

    public IEnumerator SwitchDown()
    {
        if (!down)
        {
            while (transform.position != DownTranslation)
            {
                transform.position = Vector3.MoveTowards(transform.position, DownTranslation, 0.1f);
                if (Vector3.Distance(transform.position, DownTranslation) <= 0.1f)
                {
                    transform.position = DownTranslation;
                    down = true;
                }
                yield return null;
            }
        }
    }

    public IEnumerator SwitchUp()
    {
        if (down)
        {
            while (transform.position != UpTranslation)
            {
                transform.position = Vector3.MoveTowards(transform.position, UpTranslation, 0.1f);
                if (Vector3.Distance(transform.position, UpTranslation) <= 0.1f)
                {
                    transform.position = UpTranslation;
                    down = false;
                }
                yield return null;
            }
        }
    }

    public bool IsDown()
    {
        return down;
    }
}
