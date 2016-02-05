using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class PlayerControls : MonoBehaviour {

    public const string SOLID_MODEL = "CubePrototype02x02x02";
    public const string LIQUID_MODEL = "CubePrototype02x02x02";
    public const string GAS_MODEL = "CubePrototype02x02x02";

    const string SOLID_MATERIAL = "Black Grid";
    const string LIQUID_MATERIAL = "Blue";
    const string GAS_MATERIAL = "Green";

    
    

    private Player m_pPlayer; // Reference to the ball controller.

    private Vector3 move;
    // the world-relative desired move direction, calculated from the camForward and user input.

    private Transform cam; // A reference to the main camera in the scenes transform
    private Vector3 camForward; // The current forward direction of the camera
    private bool jump; // whether the jump button is currently pressed

    private bool e; // whether the 'e' key is pressed
    private bool q; // whether the 'q' key is pressed
    private bool e_up;// is 'e' key released
    private bool q_up;//is 'q' key released


   // private int m_iState;

    private Mesh m_pSolidMesh;
    private Mesh m_pLiquidMesh;
    private Mesh m_pGasMesh;

    private Material m_SolidMaterial;
    private Material m_LiquidMaterial;
    private Material m_GasMaterial;

    private bool jump_debounce = false;

    public enum State { Solid, Liquid, Gas };
    public State m_State;
    private State m_PreviousState;

    private void SetMesh( Mesh target_mesh )
    {
        GetComponent<MeshFilter>().mesh = target_mesh;
        // switch the collider
        if (target_mesh == m_pSolidMesh)
        {            
            GetComponents<BoxCollider>()[0].enabled = true;
            GetComponents<BoxCollider>()[1].enabled = false;
            GetComponents<BoxCollider>()[2].enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
        if (target_mesh == m_pLiquidMesh)
        {            
            GetComponents<BoxCollider>()[0].enabled = false;
            GetComponents<BoxCollider>()[1].enabled = true;
            GetComponents<BoxCollider>()[2].enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
        if (target_mesh == m_pGasMesh)
        {
            GetComponents<BoxCollider>()[0].enabled = false;
            GetComponents<BoxCollider>()[1].enabled = false;
            GetComponents<BoxCollider>()[2].enabled = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void SetMaterial(Material target_material)
    {
        GetComponent<MeshRenderer>().material = target_material;
    }
    
    private void Awake()
    {

        GameObject o;
        m_State = State.Solid;
        m_PreviousState = State.Solid;

        o = Instantiate(Resources.Load(SOLID_MODEL)) as GameObject;
        m_pSolidMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);

		o = Instantiate(Resources.Load(LIQUID_MODEL)) as GameObject;
        m_pLiquidMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);

		o = Instantiate(Resources.Load(GAS_MODEL)) as GameObject;
        m_pGasMesh = o.GetComponent<MeshFilter>().mesh;
        o.SetActive(false);

		m_SolidMaterial = Resources.Load(SOLID_MATERIAL) as Material;
		m_LiquidMaterial = Resources.Load(LIQUID_MATERIAL) as Material;
		m_GasMaterial = Resources.Load(GAS_MATERIAL) as Material;

		SetMesh(m_pSolidMesh);
		SetMaterial(m_SolidMaterial);
            
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
        jump = CrossPlatformInputManager.GetButton("Jump");
        bool e = Input.GetKey(KeyCode.E);
        bool q = Input.GetKey(KeyCode.Q);


        if (!jump)
            jump_debounce = false; 

        if (!e)
            e_up = false;

        if (!q)
            q_up = false;

        if ( jump && !jump_debounce )
            jump_debounce = true;
        
        if (e && !e_up)
        {
            e_up = true;
			RaiseState();
        }

        if (q && !q_up)
        {
            q_up = true;
			LowerState();
        }
        
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
    }


    private void FixedUpdate()
    {
        // Call the Move function of the ball controller
        m_pPlayer.Move(move, jump);
        jump = false;
    }

	public int GetState()
	{
		if (m_State == State.Solid) return 0;
		else if (m_State == State.Liquid) return 1;
		else if (m_State == State.Gas) return 2;
		else return -1;
	}

	public void RaiseState()
	{
		switch (m_State)
		{
			case State.Solid:
				SetMesh(m_pLiquidMesh);
				SetMaterial(m_LiquidMaterial);
				m_PreviousState = m_State;
				m_State = State.Liquid;
				m_pPlayer.ChangeState(1);
				break;
			case State.Liquid:
				SetMesh(m_pGasMesh);
				SetMaterial(m_GasMaterial);
				m_PreviousState = m_State;
				m_State = State.Gas;
				m_pPlayer.ChangeState(2);
				break;
		}
	}

	public void LowerState()
	{
		switch (m_State)
		{
			case State.Gas:
				SetMesh(m_pLiquidMesh);
				SetMaterial(m_LiquidMaterial);
				m_PreviousState = m_State;
				m_State = State.Liquid;
				m_pPlayer.ChangeState(1);
				break;
			case State.Liquid:
				SetMesh(m_pSolidMesh);
				SetMaterial(m_SolidMaterial);
				m_PreviousState = m_State;
				m_State = State.Solid;
				m_pPlayer.ChangeState(0);
				break;
		}
	}

	public void ChangeState(int state)
	{
		switch (state)
		{
			case 0:
				SetMesh(m_pSolidMesh);
				SetMaterial(m_SolidMaterial);
				m_State = State.Solid;
				//m_pPlayer.ChangeState(0);
				break;
			case 1:
				SetMesh(m_pLiquidMesh);
				SetMaterial(m_LiquidMaterial);
				m_State = State.Liquid;
				//m_pPlayer.ChangeState(1);
				break;
			case 2:
				SetMesh(m_pGasMesh);
				SetMaterial(m_GasMaterial);
				m_State = State.Gas;
				//m_pPlayer.ChangeState(2);
				break;
			default:
				break;
		}
	}
}
