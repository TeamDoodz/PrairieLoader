using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

public sealed class DisableOnAwake : MonoBehaviour {
	private void Awake() {
		gameObject.SetActive(false);
	}
}
