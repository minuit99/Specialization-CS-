using UnityEngine;
using System.Collections;

public class playerHp_display : MonoBehaviour 
{
	private Rect _rect;
	private Texture _texture;
	private PlayerHealth _playerHealth;
	private Renderer _render;
	private UI_Manager _uiManager;

	public GameObject menuText; // this contain the menu script
	public GameObject player;

	void Awake()
	{
		_playerHealth = player.GetComponent<PlayerHealth>();
		_uiManager = menuText.GetComponent<UI_Manager>();
	
	}

	void Start()
	{
		_rect = new Rect(Screen.width * 0.06f, Screen.height * 0.90f, Screen.width * 0.03f, Screen.height * 30000f);
		_texture = Resources.Load ("Textures/bar") as Texture;
			
	}

	void OnGUI()
	{
		for (int i = 0; i < _playerHealth.currentHealth; i++) 
		{
			Rect newRect = new Rect(_rect.x, _rect.y - i * Screen.width * 0.035f, _rect.width, _rect.width);
			GUI.DrawTexture(newRect, _texture);
		}

	}

//	void Update()
//	{
//		if(_uiManager.startGame.enabled)
//		{
//			
//		}
//	}


}
