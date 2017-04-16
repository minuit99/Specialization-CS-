using UnityEngine;
using System.Collections;

public class Enemy_Ninja: MonoBehaviour
{
	private float _distanceFrom;
	private bool _isAttacking;
	private bool _isJumping;
	private float _jumpXSpeed = 3f;
	private float _jumpYSpeed = 6.5f;
	private bool _grounded = true;

//	public bool hitByBomb;
	private MeshRenderer _daggerRenderer;
	private BoxCollider _daggerCollider;
	private Vector3 _distance;
	private PlayerHealth _playerHealth;
	private Rigidbody _rb;
	private Enemy_Bomb _eBomb;

	public GameObject dagger;
	public GameObject player;

	void Awake()
	{
		_playerHealth = player.GetComponent<PlayerHealth>();
		_rb = GetComponent<Rigidbody>();
		_daggerRenderer = dagger.GetComponent<MeshRenderer>();
		_daggerCollider = dagger.GetComponent<BoxCollider>();
	}

	void Update()
	{
		_distance = (transform.position - player.transform.position);
		_distance.y = 0;
		_distance.z = 0;
		_distanceFrom = _distance.magnitude;
		_distance /= _distanceFrom;
		float dist = Vector3.Distance(transform.position, player.transform.position);

		if(dist <= 7.0f)
		{
			//TODO change _distance with dist for neat
			if(_distance == new Vector3(1,0,0))
			{
				transform.localEulerAngles = new Vector3(0,0,0);
				if(_distanceFrom <= 5.0f)
				{
					StartCoroutine(JumpAround(2));
				}
			}
			else
			{
				transform.localEulerAngles = new Vector3(0,180,0);
				if(_distanceFrom <= 5.0f)
				{
					StartCoroutine(JumpAround(2));
				}
			}
		}
	}
		
	IEnumerator JumpAround(float seconds)
	{
		yield return null;
		if (_grounded && !_isJumping) 
		{
			_rb.AddRelativeForce(-_jumpXSpeed, _jumpYSpeed, 0, ForceMode.VelocityChange);

			_isJumping = true;
			yield return new WaitForSeconds(seconds);
			_isAttacking = true;
			if(_isAttacking)
			{
				StartCoroutine(Attacking(0.4f));
			}
		}
		yield return null;
	}

	IEnumerator Attacking(float pSeconds)
	{
		_daggerRenderer.enabled = true;
		_daggerCollider.enabled = true;
		yield return new WaitForSeconds (pSeconds);
		_isAttacking = false;
		_daggerRenderer.enabled = false;
		_daggerCollider.enabled = false;
		_isJumping = false;

	}

	void HittingPlayer()
	{
		_playerHealth.TakeHealth(1);
	}
		
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Ground")
		{
			_grounded = true;
		}
		//Uncomment when ready cuz its overkill
//		if (col.gameObject.tag == "Player") 
//		{
//			HittingPlayer();
//
//		}
	}
		
	void OnTriggerEnter(Collider trigger)
	{
		if(trigger.gameObject.tag == "Player")
		{
			HittingPlayer();
		}

		if(trigger.gameObject.tag == "Sword")
		{
			Destroy(gameObject, 0.2f);
			_rb.AddRelativeForce(+3, +3, 0, ForceMode.Impulse);
		}
	}
}
