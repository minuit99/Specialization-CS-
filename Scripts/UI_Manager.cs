using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour {

//	public TextMesh threeDText;


	public Canvas startGame;
	public GameObject player;

	private MeshRenderer _renderer;
	private PlayerHealth _playerHealth;

	private bool isPaused;


	void Awake()
	{
		_renderer = GetComponent<MeshRenderer>();
		_playerHealth = player.GetComponent<PlayerHealth>();

	}

	void Start()
	{
		startGame.enabled = false;
	}

	void Update()
	{
		if(isPaused == false)
		{
			if (Input.GetKey(KeyCode.Q)) 
			{
				isPaused = true;
				Time.timeScale = 0;
				_renderer.enabled = true;
			}
		}
		if (isPaused == true) 
		{
			if (Input.GetKey(KeyCode.E)) 
			{
				_renderer.enabled = false;
				isPaused = false;
				Time.timeScale = 1;
			}
		}
		if (_playerHealth.isDead) 
		{
			startGame.enabled = true;
		}


	}
	public void LoadGame()
	{
		SceneManager.LoadScene(0);
	}

	public void ExitGame()
	{
		Application.Quit();
	}
}
