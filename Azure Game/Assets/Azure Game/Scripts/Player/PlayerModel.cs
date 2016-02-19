using UnityEngine;
using System.Collections;

public class PlayerModel : MonoBehaviour {

    private Player m_pHostPlayer;

    public void SetHostPlayer( Player pPlayer )
    {
        m_pHostPlayer = pPlayer;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if ( m_pHostPlayer != null )
        {
            transform.position = m_pHostPlayer.transform.position;
        }

	}
}
