// Enable Q and E to instantly change state for debugging peterm
#define ENABLE_STATE_DEBUG_KEYS

using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControls : MonoBehaviour {

    private Player m_pPlayer; // Reference to the ball controller.

    private Vector3 move;
    // the world-relative desired move direction, calculated from the camForward and user input.

    private Transform cam; // A reference to the main camera in the scenes transform
    private Vector3 camForward; // The current forward direction of the camera
    private bool jump; // whether the jump button is currently pressed

#if ENABLE_STATE_DEBUG_KEYS
    private bool m_bQPressed;
    private bool m_bEPressed;
    private bool m_bQDebounce;
    private bool m_bEDebounce;
#endif

    private void Awake()
    {
            
        // Set up the reference.
        m_pPlayer = GetComponent<Player>();
		

        // get the transform of the main camera
        if (Camera.main != null)
        {
            cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
        }
    }

    private void Update()
    {
        // Get the axis and jump input.

        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        jump = CrossPlatformInputManager.GetButtonDown("Jump");

        // calculate move direction
        if (cam != null)
        {
            // calculate camera relative direction to move:
            camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
            move = (v * camForward + h * cam.right).normalized;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            move = (v * Vector3.forward + h * Vector3.right).normalized;
        }

#if ENABLE_STATE_DEBUG_KEYS

        m_bEPressed = Input.GetKey(KeyCode.E);
        m_bQPressed = Input.GetKey(KeyCode.Q);

        if ( m_bEPressed  )
        {
            if (!m_bEDebounce)
            {
                m_pPlayer.RaiseState();
            }

            m_bEDebounce = true;
        }
        else
        {
            m_bEDebounce = false;
        }

        if (m_bQPressed)
        {
            if (!m_bQDebounce)
            {
                m_pPlayer.LowerState();
            }

            m_bQDebounce = true;
        }
        else
        {
            m_bQDebounce = false;
        }
#endif

    }


    private void FixedUpdate()
    {
        // Call the Move function of the ball controller
        m_pPlayer.Move(move, jump);
        jump = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            other.SendMessage("Consume");
            other.gameObject.SetActive(false);
        }
    }
	
}
