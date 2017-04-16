using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public GameObject playerTarget;
	public Vector3 focusAreaSize;

	public float verticalOffset;

	private FocusArea _focusArea;

	void Start()
	{
		_focusArea = new FocusArea (playerTarget.GetComponent<Collider>().bounds, focusAreaSize);
	}

	void LateUpdate()
	{
		_focusArea.Update(playerTarget.GetComponent<Collider>().bounds);
		Vector3 focusPosition = _focusArea.center + Vector3.up * verticalOffset;

		transform.position = (Vector3)focusPosition + Vector3.forward * -10;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1,2,0, .5f);
		Gizmos.DrawCube(_focusArea.center, focusAreaSize);
	}

	struct FocusArea
	{
		public Vector3 center;
		public Vector3 velocity;
		float left, right;
		float top, bottom;

		public FocusArea(Bounds pTargetBounds, Vector3 pSize)
		{
			left = pTargetBounds.center.x - pSize.x/2;
			right = pTargetBounds.center.x + pSize.x/2;
			bottom = pTargetBounds.min.y;
			top = pTargetBounds.min.y + pSize.y;

			velocity = Vector3.zero;
			center = new Vector3((left+right)/2,(top + bottom)/2);
		}

		public void Update(Bounds pTargetBounds)
		{
			float shiftX = 0;
			if(pTargetBounds.min.x < left)
			{
				shiftX = pTargetBounds.min.x - left;
			}
			else if (pTargetBounds.max.x > right)
			{
				shiftX = pTargetBounds.max.x - right;
			}
			left += shiftX;
			right += shiftX;

			float shiftY = 0;
			if(pTargetBounds.min.y < bottom)
			{
				shiftY = pTargetBounds.min.y - bottom;
			}
			else if (pTargetBounds.max.y > top)
			{
				shiftY = pTargetBounds.max.y - top;
			}
			top += shiftY;
			bottom += shiftY;

			center = new Vector3((left+right)/2,(top + bottom)/2);
			velocity = new Vector3(shiftX,shiftY);

		}
	}

}
