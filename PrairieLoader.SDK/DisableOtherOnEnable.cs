using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

public sealed class DisableOtherOnEnable : MonoBehaviour {
	public GameObject ToDisable;

	private void OnEnable() {
		ToDisable.SetActive(false);
	}
}
