namespace Conibear {
	using System;
	using UnityEngine;

	[RequireComponent(typeof(SphereCollider))]
	public class SphereColliderGroundChecker : GrounderCheckerBase {
		private float SphereRadius => ((SphereCollider) base.Collider).radius;

		// https://answers.unity.com/questions/1543673/spherecast-for-ground-check-glitch.html
		public override bool IsGrounded(out RaycastHit hit) {
			var rayDirection = Vector3.down;
			m_IsGrounded = Physics.SphereCast(transform.position, SphereRadius - Physics.defaultContactOffset, -transform.up, out hit, GroundedBufferDistance, this.TargetLayer, QueryTriggerInteraction.UseGlobal);
			Debug.DrawRay(transform.position, rayDirection, Color.red, this.DistanceToGround);
			return m_IsGrounded;
		}

		protected override float DistanceToEdgeOfCollider() {
			return SphereRadius;
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, this.SphereRadius);
		}
	}
}