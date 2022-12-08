using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

public sealed class NextWave : MonoBehaviour {
	bool doingIt = false;

	private void Update() {
		if(transform.childCount <= 1 && !doingIt) {
			/*
			foreach(Transform child in transform) {
				if(child.GetComponent<GoreZone>() is GoreZone gz) {
					foreach(Transform gzchild in gz.transform) {
						EnemyIdentifier eid = gzchild.GetComponent<EnemyIdentifier>();
						EnemyIdentifierIdentifier eidid = gzchild.GetComponent<EnemyIdentifierIdentifier>();
						if(eid != null || eidid != null) {
							if(eidid == null) continue;
							if(eid == null) eid = eidid.eid;
							if(eid == null) continue;
							if(eid.dead) return;
						}
					}
				}
				else if(child.GetComponent<EnemyIdentifier>() != null || child.GetComponent<EnemyIdentifierIdentifier>() != null) {
					return;
				}
			}
			*/
			if(Input.GetKeyDown(KeyCode.P)) {
				doingIt = true;
				StartCoroutine(EnableAfterDelay());
			}
		}
	}

	private IEnumerator EnableAfterDelay() {
		yield return new WaitForSeconds(1.5f);
		ToActivate.SetActive(true);
		Destroy(this);
	}

	public GameObject ToActivate;
}
