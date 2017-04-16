using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Kanon : MonoBehaviour 
{
	private int   _cannCurrentHp;
	private int   _enemyStartingHp = 2;
	private float _bulletSpeed = 170f;
	private float _distanceFrom;
	private float _movementSpeed = 1.1f;
	private bool  _isShooting;

//	private Vector3      _distance;
	private float 		 _playerDistance = 7.9f;
	private PlayerHealth _playerHealth;
	private PlayerAttack _playerAttack;

	public GameObject    player;
	public Rigidbody     bullet;
	public Transform     canonPosition;



	void Awake()
	{
		_playerHealth = player.GetComponent<PlayerHealth>();
		_playerAttack = player.GetComponent<PlayerAttack>();
		_cannCurrentHp = _enemyStartingHp;

	}

	void Update()
	{

		float dist = Vector3.Distance(transform.position, player.transform.position);
		if (!_isShooting && dist <= _playerDistance)
		{
			_isShooting = true;
			StartCoroutine(Shoot());
		}

	}


	IEnumerator Shoot()
	{
		int numberOfBullets = 2;
		for (int i = 0; i < numberOfBullets; i++) {
			Rigidbody clone = Instantiate (
				bullet, 
				transform.position, 
				Quaternion.identity) as Rigidbody;
			clone.AddForce(-clone.transform.right * _bulletSpeed);
			Destroy (clone.gameObject, 3f); 
			if (i < numberOfBullets - 1) yield return new WaitForSeconds(1f);}
		float movingTime = 0;
		while (movingTime < 2) {
			transform.Translate(Vector3.left * Time.deltaTime * _movementSpeed); 
			movingTime = movingTime + Time.deltaTime;
			yield return null;}
		_isShooting = false;
	}
		

	void OnTriggerEnter(Collider trig)
	{
		if(trig.gameObject.tag == "Player")
		{
			InvokeRepeating("HittingPlayer",  2.0f,  2.0f);
		}

		if(trig.gameObject.tag == "Sword")
		{
			int damage;
			_playerAttack.SwordAttack(out damage);
			_cannCurrentHp -= damage;
			if(_cannCurrentHp <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
		
	void OnTriggerExit(Collider col)
	{
		CancelInvoke("HittingPlayer");
	}



	void HittingPlayer()
	{
		_playerHealth.TakeHealth(1);
	}



}
