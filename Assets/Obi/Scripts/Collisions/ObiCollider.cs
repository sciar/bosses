using UnityEngine;
using System.Collections;

namespace Obi{

	/**
	 * Small helper class that lets you assign an ObiCollisionMaterial to a regular Collider to control how
	 * it interacts with Obi particles.
	 */
	[RequireComponent(typeof(Collider))]
	public class ObiCollider : MonoBehaviour
	{
		public ObiCollisionMaterial material;
		public Oni.TriangleMeshShape.MeshColliderType meshColliderType = Oni.TriangleMeshShape.MeshColliderType.ThinTwoSided;
		public float contactOffset = 0.01f;
		[HideInInspector] public int materialIndex = 0;

		// Use this for initialization
		void Start () {
			Collider col = GetComponent<Collider>();
			if (col != null)
				col.contactOffset = contactOffset;
		}
	}
}

