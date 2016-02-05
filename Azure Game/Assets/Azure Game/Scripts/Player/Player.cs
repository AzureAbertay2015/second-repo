﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameManager m_GameManager;

    [SerializeField]
    private float m_MovePower = 10; // The force added to the player to move it.
    [SerializeField]
    private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
    
    public float m_JumpPower = 20; // The force added to the ball when it jumps.

    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    private Rigidbody m_Rigidbody;


    private void Start()
    {
		m_Rigidbody = GetComponent<Rigidbody>();
        // Set the maximum angular velocity.
        GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
    }


    public void Move(Vector3 moveDirection, bool jump)
    {
        // Otherwise add force in the move direction.
        m_Rigidbody.AddForce(moveDirection * m_MovePower);

        // If on the ground and jump is pressed...
        if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump && (int)m_Rigidbody.velocity.y == 0)
        {
            // ... add force in upwards.
            m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
        }
    }

	public void ChangeState(int state)
	{
		this.GetComponent<PlayerControls>().ChangeState(state);
		m_GameManager.ChangeState(state);
	}
}
