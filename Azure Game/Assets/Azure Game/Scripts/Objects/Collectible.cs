using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

    public enum CollectibleType {Null, SpeedUp, SpeedDown, Time, PointsUp, PointsDown, Charge, Puzzle};
    public CollectibleType m_Collectible;
    private PlayerState m_PlayerState;
    
	// Use this for initialization
	void Start () {
        if (m_Collectible == CollectibleType.Null)
        {
            Debug.LogError(name + " is " + m_Collectible);
        }
        m_PlayerState = GameManager.GetPlayer().GetComponent<PlayerState>();
        
	}
	
    public void Consume()
    {
        Debug.Log("Collectible consumed");
        switch (m_Collectible)
        {
            case CollectibleType.SpeedUp:
                m_PlayerState.SpeedUp();
                break;

            case CollectibleType.SpeedDown:
                m_PlayerState.SpeedDown();
                break;

            case CollectibleType.Time:
                m_PlayerState.Time();
                break;

            case CollectibleType.PointsUp:
                m_PlayerState.PointsUp();
                break;

            case CollectibleType.PointsDown:
                m_PlayerState.PointsDown();
                break;

            case CollectibleType.Charge:
                m_PlayerState.Charge();
                break;

            case CollectibleType.Puzzle:
                m_PlayerState.Puzzle();
                break;

            default:
                Debug.Log(m_Collectible + "has gone awry");
                break;
        }

    }


   public CollectibleType GetCollectibleType()
    {
        return m_Collectible;
    }

    private void SetCanvasValues()
    {
    }
}
