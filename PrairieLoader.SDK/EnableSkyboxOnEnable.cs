using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PrairieLoader.SDK;

public sealed class EnableSkyboxOnEnable : MonoBehaviour {
	public Material Skybox;

	private void OnEnable() {
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		RenderSettings.skybox = Skybox;
	}
	private void OnDisable() {
		Camera.main.clearFlags = CameraClearFlags.Color;
	}
}
