namespace Conibear {
	using UnityEngine;


	[RequireComponent(typeof(BoxCollider))]
	public class BoxColliderGroundChecker : GrounderCheckerBase {
		protected override float DistanceToEdgeOfCollider() {
			return Collider.bounds.extents.y;
		}
	}
}