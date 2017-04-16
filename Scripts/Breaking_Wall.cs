using UnityEngine;
using System.Collections;

public class Breaking_Wall : MonoBehaviour {

	private int _hP = 2;
	private int _currentHp;

	private Renderer _rend;



	void Start()
	{
		_rend = GetComponent<Renderer>();
		_currentHp = _hP;

	}

	void Update()
	{
		if (_currentHp <= 0) {
			Destroy(gameObject);
		}
		else if (_currentHp == 1)
		{
			_rend.material.color = Color.white;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Sword")
		{
			_currentHp--;
		}
	}

}
