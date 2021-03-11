namespace Conibear {
	using UnityEngine;

	public static class Math {

		/// <summary>
		/// The maximum obtainable velocity at a given force value
		/// </summary>
		/// <param name="rigidbody">The rigidbody the force is applied to</param>
		/// <param name="force">The force value applied to the rigidbody</param>
		/// <returns></returns>
		public static float TerminalVelocity(Rigidbody rigidbody, Vector3 force) {
			return ((force.magnitude / rigidbody.drag) - Time.fixedDeltaTime * force.magnitude) / rigidbody.mass;
		}
		
		/// <summary>
		/// simple function to add a curved bias towards 1 for a value in the 0-1 range
		/// </summary>
		public static float CurveFactor(float factor) {
			return 1 - (1 - factor) * (1 - factor);
		}


		/// <summary>
		/// unclamped version of Lerp, to allow value to exceed the from-to range
		/// </summary>
		public static float ULerp(float from, float to, float value) {
			return (1.0f - value) * from + value * to;
		}

		public static Vector3 PositionLerp(Vector3 startPosition, Vector3 endPosition, AnimationCurve animationCurve, float normalizedTime) {
			return Vector3.Lerp(startPosition, endPosition, animationCurve.Evaluate(normalizedTime));
		}

		public static Vector3 PositionLerp(Vector3 startPosition, Vector3 endPosition, Vector3 direction, AnimationCurve animationCurve, float normalizedTime, float time) {
			return Vector3.Lerp(startPosition, endPosition, normalizedTime) + (animationCurve.Evaluate(time) * direction);
		}

		/// <summary>
		/// Determine if an object is on the left or right based on there relative angle
		/// </summary>
		/// <returns>0=left, 1=right</returns>
		public static byte BinaryPositionFromRelativeAngle(Vector3 target) {
			Vector3 perp = Vector3.Cross(Vector3.forward, target);
			float direction = Vector3.Dot(perp, Vector3.up);

			if (direction > 0f) {
				return 1;
			} else {
				return 0;
			}
		}

		/// <summary>
		/// Check where a object is locations relative to origin
		/// </summary>
		/// <param name="origin">The position in which to check if an object is on either side of</param>
		/// <param name="target">The position your checking against the origin</param>
		/// <returns>
		/// [0]=x, [1]=y, [2]=z axis
		/// 0=left, 1=right
		/// </returns>
		public static byte[] BinaryPositionFromOrigin(Vector3 origin, Vector3 target) {
			byte[] binaryPos = new byte[3];

			if (target.x < origin.x) {
				binaryPos[0] = 0;
			} else if (target.x > origin.x) {
				binaryPos[0] = 1;
			}

			if (target.y < origin.y) {
				binaryPos[1] = 0;
			} else if (target.y > origin.y) {
				binaryPos[1] = 1;
			}

			if (target.z < origin.z) {
				binaryPos[2] = 0;
			} else if (target.z > origin.z) {
				binaryPos[2] = 1;
			}

			return binaryPos;
		}
	}
}