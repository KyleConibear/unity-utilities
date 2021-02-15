namespace Conibear {
	using UnityEngine;

	[RequireComponent(typeof(SphereCollider))]
	public class SphereColliderGroundChecker : GrounderCheckerBase {
		protected override float DistanceToEdgeOfCollider() {
			return ((SphereCollider) base.Collider).radius;
		}
	}
}