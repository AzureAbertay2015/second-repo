using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameRules m_GameRules;
	private bool m_GameRestarted;
	private float m_CurrentAlpha;
	private bool m_AlphaState;
	private Color m_FadeColor;
	private Canvas m_FadeCanvas;
	public float m_FadeSpeed;
	public float m_SmallFadeSpeed;
	public bool m_InitialFade;
	public float m_InitialFadeDelay;

    // Use this for initialization
    void Start()
    {
		///
		m_GameRules = GameManager.GetGameRules();
		m_CurrentAlpha = 0.00f;
		m_AlphaState = false;
		m_FadeSpeed = 10;
		m_SmallFadeSpeed = 0.1f;
		m_InitialFadeDelay = 0.2f;
		m_InitialFade = false;

		foreach (Canvas c in FindObjectsOfType<Canvas>())
		{
			if (c.tag == "Fade")
			{
				m_FadeCanvas = c;
			}
		}
		if (!m_InitialFade)
		{
			Color col = Color.black;
			m_FadeCanvas.GetComponentInChildren<Image>().color = col;
			StartCoroutine("WaitForFadeIn");
			m_AlphaState = false;
			m_InitialFade = true;
		}

		SetCanvasCameras();
	}

    // Update is called once per frame
    void Update()
    {
		
        ScanForKeyStroke();
		ScanForDeath();
    }

    void ScanForKeyStroke()
    {
        if (Input.GetKeyDown("escape")&& !Input.GetKeyDown("space"))
            m_GameRules.TogglePauseMenu();
		if (Input.GetKeyDown(KeyCode.C))
			if (m_AlphaState)
			{
				StopCoroutine("FadeToBlack");
				StartCoroutine("FadeFromBlack");
				m_AlphaState = false;
			}
			else
			{
				StopCoroutine("FadeFromBlack");
				StartCoroutine("FadeToBlack");
				m_AlphaState = true;
			}

    }
	
	void ScanForDeath()
	{
		if (!m_GameRules.IsPlayerAlive() && m_GameRestarted)
		{
			m_GameRules.ToggleDeathMenu();
			m_GameRestarted = false;
		}
		if (m_GameRules.IsPlayerAlive())
			m_GameRestarted = true;
	}
	
	public IEnumerator FadeToBlack()
	{
		for (float f = 0f; f <= 1; f += 0.03f)
		{
			Debug.Log("f: " + f + ". time: " + Time.time);
			Color c = m_FadeCanvas.GetComponentInChildren<Image>().color;
			c.a = f;
			m_FadeCanvas.GetComponentInChildren<Image>().color = c;
			if (c.a >= 0.9f)
			{
				c.a = 1.0f;
				m_AlphaState = true;
				Debug.Log("time: " + Time.time);
				StopCoroutine("FadeToBlack");
			}
            yield return null;
		}
	}

	public IEnumerator FadeFromBlack()
	{
		for (float f = 1f; f >= 0; f -= 0.03f)
		{
			Color c = m_FadeCanvas.GetComponentInChildren<Image>().color;
			c.a = f;
			m_FadeCanvas.GetComponentInChildren<Image>().color = c;
			if (c.a <= 0.1f)
			{
				c.a = 0.0f;
				m_AlphaState = false;
				StopCoroutine("FadeFromBlack");
			}
			yield return null;
		}
	}
	

	public IEnumerator WaitForFadeIn()
	{
		yield return new WaitForSeconds(m_InitialFadeDelay);
		StartCoroutine("FadeFromBlack");
	}

	private void SetCanvasCameras()
	{
		foreach (Canvas c in GetComponentsInChildren<Canvas>())
		{
			c.renderMode = RenderMode.ScreenSpaceCamera;
			c.worldCamera = Camera.main;
            c.sortingLayerName = "HUD";
			if (c.tag == "Menu")
			{
				
				c.sortingOrder = 2;
			}
			else if (c.tag == "Fade")
			{
				c.sortingOrder = 1;
			}
			else
			{
				c.sortingOrder = 0;
			}
			Debug.Log("Set Camera: " + Camera.main + " on " + c.name);
		}
	}
}