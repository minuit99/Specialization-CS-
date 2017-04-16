using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	private int _startingHealth = 5;  //Starting value for health, set to 7 when ready with everything

	public int currentHealth;
	public int maxHealth = 8;
	public int specialHealth = 7;
	public bool isDead = false;
	public bool canDoSpecial;

	private PlayerMovement _playerMovement; //Use when player dead to detach playerMovement script
	private Renderer _rend;
	public GameObject Hp;
	public GameObject bullet;

	void Awake ()
	{
		_playerMovement = GetComponent <PlayerMovement> ();
		_rend = GetComponent<Renderer>();
		currentHealth = _startingHealth;
	}

	void Start()
	{
		_rend.material.color = Color.white;
	}
	void Update()
	{
//		Debug.Log("Current Hp: " + currentHealth + "; Special Status: " + canDoSpecial);

	}

	void AddHealth()
	{
		currentHealth ++;
		if(currentHealth >= specialHealth && currentHealth <= maxHealth)
		{
			canDoSpecial = true;
			_rend.material.color = Color.yellow;
		} 
	}

	public void TakeHealth (int pDamage) //pass value for damage done
	{
		StartCoroutine(PlayerFlashing(2, 0.1f));
		currentHealth -= pDamage;

		if (currentHealth <= 0)		
		{
			isDead = true;
//			
			_playerMovement.enabled = false;
			StartCoroutine(PlayerFlashing(4, 0.2f));
//			Destroy(gameObject, 0.7f);

		}
		if(currentHealth < specialHealth)
		{
			canDoSpecial = false;
			_rend.material.color = Color.white;
		}
//		isDead = true ? _rend.enabled = false : _rend.enabled = true;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Hp")
		{
			if(currentHealth <= maxHealth)
			{
				AddHealth();
				Destroy(col.gameObject);
			}
		}
		if(col.gameObject.tag == "Bullet")
		{
			TakeHealth(1);
			Destroy(col.gameObject);
		}
	}

	IEnumerator PlayerFlashing (int pNumFlashes, float pSeconds)
	{
		for(int i = 0; i < pNumFlashes * 2; i++)
		{
			_rend.enabled = !_rend.enabled;
			yield return new WaitForSeconds(pSeconds);
		}
		if (!isDead) 
		{
			_rend.enabled = true;
		}
		else if (isDead)
		{
			Color color = _rend.material.color;
			color.a -= 0.9f;
			_rend.material.color = color;
		}
	}
}
