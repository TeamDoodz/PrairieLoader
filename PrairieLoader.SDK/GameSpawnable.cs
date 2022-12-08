using System;
using System.Collections.Generic;
using System.Text;
using UMM;
using UnityEngine;

namespace PrairieLoader.SDK;

[AddComponentMenu("PrairieSDK/Game Spawnable")]
public sealed class GameSpawnable : MonoBehaviour {
	public string PrefabName;

	private void Start() {
		GameObject obj = Instantiate((GameObject)UKAPI.LoadCommonAsset(PrefabName), transform.position, transform.rotation, transform.parent);
		Destroy(gameObject);
	}

	private void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(2f, 4f, 2f));
	}
}
