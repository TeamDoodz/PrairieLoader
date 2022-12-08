using System;
using UnityEngine;

namespace PrairieLoader.SDK;

[AddComponentMenu("PrairieSDK/Special/Level Objects Parent")]
public sealed class LevelObjectsParent : MonoBehaviour {
	private void Awake() {
		transform.position = new Vector3(0, -10.5f, 300);
	}

	private void OnDrawGizmos() {
		DrawArrow.ForGizmo(transform.position, new Vector3(0f, 0f, 15f), 3f);
	}
}
