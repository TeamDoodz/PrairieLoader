using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

[RequireComponent(typeof(SpringJoint))]
public sealed class PhysFloatingPlatform : MonoBehaviour {
	public float RaiseFromYOffset = -5f;
	private void Start() {
		transform.Translate(new	Vector3(0f, RaiseFromYOffset, 0f));
	}
}
