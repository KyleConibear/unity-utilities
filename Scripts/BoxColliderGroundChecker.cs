using System;

namespace Conibear {
	using UnityEngine;


	[RequireComponent(typeof(BoxCollider))]
	public class BoxColliderGroundChecker : MonoBehaviour {
		#region ShowOnly

		[Header("Result")]
		[ShowOnly] [SerializeField]
		private bool m_IsGrounded;

		#endregion
		
		#region SerializeFields

		[Header("Fields")]
		[SerializeField]
		private LayerMask m_TargetLayer;

		[SerializeField]
		[Range(MinimumGroundDistanceThreshold, 1)]
		private float m_GroundDistanceThreshold = 0.025f;

		#endregion


		#region Internal Consts

		private const float MinimumGroundDistanceThreshold = 0.01f;

		#endregion


		#region Internal Fields

		private BoxCollider m_BoxCollider = null;

		#endregion


		#region Internal Properties

		private BoxCollider BoxCollider {
			get {
				if (m_BoxCollider == null) {
					m_BoxCollider = transform.gameObject.GetComponent<BoxCollider>();
				}

				return m_BoxCollider;
			}
		}

		private float GroundDistanceThreshold {
			get {
				if (m_GroundDistanceThreshold < MinimumGroundDistanceThreshold) {
					Print.Warning($"GroundDistanceThreshold cannot be less than {MinimumGroundDistanceThreshold}\nm_GroundDistanceThreshold: <{m_GroundDistanceThreshold}>", name);
					m_GroundDistanceThreshold = MinimumGroundDistanceThreshold;
				}

				return m_GroundDistanceThreshold;
			}
		}

		private float DistanceToGround => this.BoxCollider.bounds.extents.y + this.GroundDistanceThreshold;


		private LayerMask TargetLayer {
			get {
				if (m_TargetLayer == 0) {
					m_TargetLayer = 1 << 8; // Default layers
				}

				return m_TargetLayer;
			}
		}

		#endregion


		#region Public Properties

		public bool IsGrounded {
			get {
				m_IsGrounded = Physics.Raycast(transform.position, -transform.up, out var hit, this.DistanceToGround, this.TargetLayer);
				return m_IsGrounded;
			}
		}

		#endregion
	}
}