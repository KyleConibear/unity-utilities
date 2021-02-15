namespace Conibear {
	using UnityEngine;

	[RequireComponent(typeof(Collider))]
	public abstract class GrounderCheckerBase : MonoBehaviour {
		#region Internal Consts

		private const float MinimumGroundDistanceThreshold = 0.01f;

		#endregion


		#region ShowOnly

		[Header("Result")]
		[ShowOnly] [SerializeField]
		private bool m_IsGrounded;

		#endregion


		#region Internal Fields

		protected Collider m_Collider = null;

		#endregion


		#region Public Properties

		public bool IsGrounded(out RaycastHit hit) {
			m_IsGrounded = Physics.Raycast(transform.position, -transform.up, out hit, this.DistanceToGround, this.TargetLayer);
			return m_IsGrounded;
		}

		#endregion


		#region SerializeFields

		[Header("Fields")]
		[SerializeField]
		private LayerMask m_TargetLayer;

		[SerializeField]
		[Range(MinimumGroundDistanceThreshold, 1)]
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

		private float GroundDistanceThreshold {
			get {
				if (m_GroundDistanceThreshold < MinimumGroundDistanceThreshold) {
					Print.Warning($"GroundDistanceThreshold cannot be less than {MinimumGroundDistanceThreshold}\nm_GroundDistanceThreshold: <{m_GroundDistanceThreshold}>", this);
					m_GroundDistanceThreshold = MinimumGroundDistanceThreshold;
				}

				return m_GroundDistanceThreshold;
			}
		}

		protected abstract float DistanceToEdgeOfCollider();
		private float DistanceToGround => DistanceToEdgeOfCollider() + this.GroundDistanceThreshold;


		private LayerMask TargetLayer {
			get {
				if (m_TargetLayer == 0) {
					m_TargetLayer = 1 << 8; // Default layers
				}

				return m_TargetLayer;
			}
		}

		#endregion
	}
}