using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	private float _speed = 5f;
	private float _jumpSpeed = 400f;
	private bool _grounded = true;

	public GameObject menuText; // this contain the menu script
	private UI_Manager _uiManager;
	private Rigidbody _rb;
	public GameObject canonTop; 

	void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_uiManager = menuText.GetComponent<UI_Manager>();
		Time.timeScale = 1; //set the time back to normal after reseting the game
	}

	void Update ()
	{
		//leftside down
		//transform.position + new Vector3(-0.5f,-1,0)
		//transform.position + new Vector3(0.5f,-1,0)
		Movement();
		if (Input.GetKeyDown (KeyCode.C))
		{
			Jump();
		}
			
		if ( Input.GetKey(key: KeyCode.LeftArrow) )
		{ 
			transform.localEulerAngles = new Vector3(0,180,0);
		}

		if ( Input.GetKey(key: KeyCode.RightArrow))
		{
			transform.localEulerAngles = new Vector3(0,0,0);
		}
	
	}

	void Movement ()
	{

		Vector3 moveDirection = Input.GetAxisRaw ("Horizontal") * transform.right 
			* Time.deltaTime * _speed;

		transform.Translate (moveDirection);
	}

	void Jump()
	{
		if (_grounded == true)
		{
			_rb.AddForce (Vector3.up * _jumpSpeed);
			_grounded = false;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		_grounded = true;

		if(Vector3.Dot(col.contacts[0].normal, Vector3.up) >= 0.5f)
		{
			if(col.gameObject.tag == "CanonTop")
			{
				transform.parent = col.gameObject.transform;

			}
		}

//		if(Vector3.Dot(other.contacts[0].normal, Vector3.left) >= 1.0f)
//		{
//			if (other.gameObject.tag == "Ground") 
//			{
//				_grounded = false;
//			}
//		}
//		else if(Vector3.Dot(other.contacts[0].normal, Vector3.right) >= 1.0f)
//		{
//			if (other.gameObject.tag == "Ground") 
//			{
//				_grounded = false;
//			}
//		}

//		RaycastHit hitInfo;
//		if (Physics.Raycast (transform.position + new Vector3(0,-0.5f,0), Vector3.right,out hitInfo, 0.1f)) {
////			Debug.Log ("Hitting from right");
//
//
//		} else if (Physics.Raycast(transform.position + new Vector3(0,0.5f,0), Vector3.left, out hitInfo, 0.1f)) {
//			Debug.Log ("Hitting from right");
//		}
	
	}

	void OnCollisionExit(Collision col)
	{
		transform.parent = null;	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "EndGame")
		{
			_uiManager.startGame.enabled = true;
			Time.timeScale = 0;
		}
	}
}
