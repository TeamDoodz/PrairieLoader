using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

[AddComponentMenu("PrairieSDK/Triggers/Insta Death Zone")]
public sealed class InstaDeathZone : AbstractTriggerZone {
	protected override void OnTriggerPlayer(dynamic other) {
		other.GetHurt(9999999, false);
	}
	protected override void OnTriggerEnemy(dynamic other) {
		other.InstaKill();
	}
}
