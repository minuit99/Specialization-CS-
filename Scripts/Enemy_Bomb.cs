using UnityEngine;
using System.Collections;

public class Enemy_Bomb : MonoBehaviour 
{
	private float _timeToBoom = 2.0f;
	private float _damaged = 0;

	private PlayerHealth _playerHealth;
//	private Enemy_Ninja _enemyNinja;
	private ParticleSystem _ps;
	private MeshRenderer _mRend;
//	private BoxCollider _bCol;

	public GameObject player;
	public GameObject ninja;
	public Transform playerTransform;

	void Start()
	{
		_playerHealth = player.GetComponent<PlayerHealth>();
//		_enemyNinja = ninja.GetComponent<Enemy_Ninja>();
		_ps = GetComponent<ParticleSystem>();
		_mRend = GetComponent<MeshRenderer>();
//		_bCol = GetComponent<BoxCollider>();

	}

	void Update()
	{
		if(_damaged == 1.0f)
		{
			_mRend.material.color = Color.red;
			if(!_ps.isPlaying) _ps.Play();
			_timeToBoom -= Time.deltaTime; //delay counter if player hits the bomb only once
			if(_timeToBoom < 0)
			{
				Boom();
			}

		}
		else if (_damaged == 2.0f)
		{
			Boom();

		}
	}
		
	void OnTriggerEnter(Collider trig)
	{
		if(trig.gameObject.tag == "Sword")
		{
			_damaged++;
		}
		if(trig.gameObject.tag == "Bullet")
		{
			Destroy(trig.gameObject);
			_damaged += 0.5f;
		}
	}
		
	public void Boom()
	{
		Destroy();
		float playerDistance = Vector3.Distance(playerTransform.position, transform.position);
		if(playerDistance < 2.3f)
		{
			HittingPlayer();
		}

//		float dist = Vector3.Distance(ninja.transform.position, transform.position);
//		if(dist < 2.3f)
//		{
//			_enemyNinja.hitByBomb = true;
//			Destroy(ninja.gameObject);
//			Debug.Log (dist);
//		}
//		else 
//			_enemyNinja.hitByBomb = false;
	}

	void HittingPlayer()
	{
		_playerHealth.TakeHealth(1);
	}

	void Destroy()
	{
		/*
		_mRend.enabled = false;
		_bCol.enabled = false;
		Destroy(gameObject, 0.39f); 
		This does humongous damage to the player 
		waiting for particle onDeath to Play
		*/
		Destroy(gameObject);
	}

}
