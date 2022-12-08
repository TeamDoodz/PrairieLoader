using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

public sealed class AudioFromVelocity : MonoBehaviour {
	public Rigidbody ReferenceBody;
	public float MaxVel;

	private AudioSource aud;

	private void Awake() {
		aud = GetComponent<AudioSource>();
	}

	private void Update() {
		aud.volume = ReferenceBody.velocity.magnitude / MaxVel;
	}
}
