using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

[AddComponentMenu("PrairieSDK/Triggers/Toggle Zone")]
public sealed class ToggleZone : AbstractTriggerZone {
	protected override void OnTriggerAny(Collider other) {
		foreach(var obj in Toggle) {
			Debug.Log(obj.name);
			obj.SetActive(!obj.activeSelf);
		}
	}

	public GameObject[] Toggle;
}
