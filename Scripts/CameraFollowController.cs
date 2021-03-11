using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour {
	#region SerializeFields

	[SerializeField]
	private bool m_IsSceneCameraFollowing = false;
	
	[SerializeField]
	private Transform objectToFollow = null;

	[SerializeField]
	private Vector3 offset = Vector3.zero;

	[SerializeField]
	private float followSpeed = 10;

	[SerializeField]
	private float lookSpeed = 10;

	#endregion


	#region MonoBehaviour Methods

	private void FixedUpdate() {
		LookAtTarget();
		MoveToTarget();
	}

	#endregion


	#region Internal Fields

	public void LookAtTarget() {
		Vector3 lookDirection = objectToFollow.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, lookSpeed * Time.deltaTime);
	}

	public void MoveToTarget() {
		var targetPosition = objectToFollow.position +
		                     objectToFollow.forward * offset.z +
		                     objectToFollow.right * offset.x +
		                     objectToFollow.up * offset.y;
		transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

		if (m_IsSceneCameraFollowing) {
			var sceneView = (UnityEditor.SceneView) UnityEditor.SceneView.sceneViews[0];
			if (sceneView != null) {
				sceneView.pivot = objectToFollow.position;
			}
		}
	}

	#endregion
}