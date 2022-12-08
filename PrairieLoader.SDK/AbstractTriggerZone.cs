using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

public abstract class AbstractTriggerZone : MonoBehaviour {
	private void OnTriggerEnter(Collider other) {
		if(other.tag == "Player" && AffectPlayer) {
			OnTriggerAny(other);
			Component other1 = other.GetComponent("NewMovement");
			if(other1 == null) return;
			Debug.Log(other1.GetType());
			OnTriggerPlayer(other1);
			DestroyIfNotRepeatable();
		} else if((other.gameObject.tag == "Enemy" || other.gameObject.layer == 10) && AffectEnemy) {
			OnTriggerAny(other);
			Component other1 = other.GetComponent("EnemyIdentifier");
			if(other1 == null) return;
			Debug.Log(other1.GetType());
			OnTriggerEnemy(other1);
			DestroyIfNotRepeatable();
		}
	}

	private void DestroyIfNotRepeatable() {
		if(!Repeatable) {
			Destroy(gameObject);
		}
	}

	// The reason why dynamic is used here is because referencing the base ULTRAKILL code directly causes Unity to freak out.
	// It's jank, but it has to be done.
	protected virtual void OnTriggerAny(Collider other) { }
	protected virtual void OnTriggerPlayer(dynamic other) { }
	protected virtual void OnTriggerEnemy(dynamic other) { }

	public bool AffectPlayer;
	public bool AffectEnemy;
	public bool Repeatable;
}
