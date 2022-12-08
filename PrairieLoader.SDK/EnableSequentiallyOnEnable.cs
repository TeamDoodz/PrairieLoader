using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

public sealed class EnableSequentiallyOnEnable : MonoBehaviour {
	public GameObject[] ToEnable;
	public float Delay = 0.25f;
	public bool DestroyAfter = true;

	private void OnEnable() {
		StartCoroutine(EnableStuff());
	}

	private IEnumerator EnableStuff() {
		foreach(GameObject obj in ToEnable) {
			obj.SetActive(true);
			yield return new WaitForSeconds(Delay);
		}
		if(DestroyAfter) {
			Destroy(gameObject);
		}
	}
}
