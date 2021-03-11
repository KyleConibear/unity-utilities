namespace Conibear {
	using System;
	using UnityEngine;

	[RequireComponent(typeof(SphereCollider))]
	public class SphereColliderGroundChecker : GrounderCheckerBase {
		private float SphereRadius => ((SphereCollider) base.Collider).radius;

		// https://answers.unity.com/questions/1543673/spherecast-for-ground-check-glitch.html
		public override bool IsTransformGrounded(out RaycastHit hit) {
			var rayDirection = Vector3.down;
			m_IsTransformGrounded = Physics.SphereCast(transform.position, SphereRadius - Physics.defaultContactOffset, -transform.up, out hit, GroundedBufferDistance, this.TargetLayer, QueryTriggerInteraction.UseGlobal);
			Debug.DrawRay(transform.position, rayDirection, Color.blue, this.DistanceToGround);
			return m_IsTransformGrounded;
		}
		
		public override bool IsRigidbodyGrounded(Rigidbody rigidbody, out RaycastHit hit) {
			var rbPos = rigidbody.position;
			var rayDirection = Vector3.down;
			m_IsRigidbodyGrounded = Physics.SphereCast(rbPos, SphereRadius - Physics.defaultContactOffset, rayDirection, out hit, GroundedBufferDistance, this.TargetLayer, QueryTriggerInteraction.UseGlobal);
			Debug.DrawRay(rbPos, rayDirection, Color.red, this.DistanceToGround);
			return m_IsRigidbodyGrounded;
		}

		protected override float DistanceToEdgeOfCollider() {
			return SphereRadius;
		}

		private void OnDrawGizmosSelected() {
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireSphere(transform.position, this.SphereRadius);
		}
	}
}