namespace Conibear {
	using UnityEngine;

	[RequireComponent(typeof(Collider))]
	public abstract class GrounderCheckerBase : MonoBehaviour {
		#region Internal Consts

		[Tooltip("Distance in addition to the edge of the Collider")]
		protected const float GroundedBufferDistance = 0.1f;

		#endregion


		#region ShowOnly

		[Header("Result")]
		[ShowOnly] [SerializeField]
		protected bool m_IsTransformGrounded;

		[ShowOnly] [SerializeField]
		protected bool m_IsRigidbodyGrounded;
		
		#endregion


		#region Internal Fields

		protected Collider m_Collider = null;

		#endregion


		#region Public Properties

		public virtual bool IsTransformGrounded(out RaycastHit hit) {
			var rayDirection = -transform.up;
			m_IsTransformGrounded = Physics.Raycast(transform.position, rayDirection, out hit, this.DistanceToGround, this.TargetLayer);
			Debug.DrawRay(transform.position, rayDirection, Color.blue, this.DistanceToGround);
			return m_IsTransformGrounded;
		}

		public virtual bool IsRigidbodyGrounded(Rigidbody rigidbody, out RaycastHit hit) {
			var rbPos = rigidbody.position;
			var rayDirection = new Vector3(rbPos.x, -rbPos.y, rbPos.z);
			m_IsRigidbodyGrounded = Physics.Raycast(rbPos, rayDirection, out hit, this.DistanceToGround, this.TargetLayer);
			Debug.DrawRay(rbPos, rayDirection, Color.red, this.DistanceToGround);
			return m_IsRigidbodyGrounded;
		}
		
		#endregion


		#region SerializeFields

		[Header("Fields")]
		[SerializeField]
		private LayerMask m_TargetLayer;

		[SerializeField]
		//[Range(GroundedBufferDistance, 1)]
		private float m_GroundDistanceThreshold = 0.025f;

		#endregion


		#region Internal Properties

		protected Collider Collider {
			get {
				if (m_Collider == null) {
					m_Collider = transform.gameObject.GetComponent<Collider>();
				}

				return m_Collider;
			}
		}

		protected abstract float DistanceToEdgeOfCollider();
		protected float DistanceToGround => DistanceToEdgeOfCollider() + GroundedBufferDistance;


		protected LayerMask TargetLayer => m_TargetLayer;

		#endregion
	}
}