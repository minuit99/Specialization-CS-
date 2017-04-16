using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
	private float _attackTimer = 0;		
	private float _attackCd = 0.2f;		//Cool Down for the attack animation
	private bool _attacking = false;
	private bool damageSwitch;

	public GameObject sword;			
	private MeshRenderer _swordRenderer;
	private BoxCollider _swordCollider;
	private PlayerHealth _playerHealth;
	private Rigidbody _rb;

	void Awake()
	{
		_swordRenderer = sword.GetComponent<MeshRenderer>();
		_swordCollider = sword.GetComponent<BoxCollider>();
		_playerHealth = GetComponent<PlayerHealth>();
		_rb = GetComponent<Rigidbody>();
	}

	void Update ()
	{
		Attack();
	
	}

	void Attack()
	{

		if(Input.GetKeyDown ("x") && !_attacking)
		{
			_attacking = true;					
			_attackTimer = _attackCd;

		}
		int val;
		SwordAttack(out val);

		if(_playerHealth.canDoSpecial)
		{
			SpecialAttack();
			damageSwitch = true;
		}
		else
		{
			damageSwitch = false;
		}

	}

	void SpecialAttack()
	{
		if(Input.GetKeyDown ("z") && !_attacking)
		{
			_attacking = true;					
			_attackTimer = _attackCd;
			_rb.AddRelativeForce(3,0,0, ForceMode.VelocityChange);

		}
		int val;
		SwordAttack(out val);

	}

	public void SwordAttack(out int pDamage)
	{
		pDamage = 0;
		if(_attacking == true)
		{
			if(_attackTimer > 0)
			{
				_swordRenderer.enabled = true;	
				_swordCollider.enabled = true;		
				_attackTimer -= Time.deltaTime;

			}
			else
			{															
				_attacking = false;										
				_swordRenderer.enabled = false;
				_swordCollider.enabled = false;

			}
			//TODO needs more testing, might be that both 'x' and 'z' deal the same damage
			if(damageSwitch)
			{
				pDamage = 2;
			}
			else
			{
				pDamage = 1;
			}
		}
	
	}
}