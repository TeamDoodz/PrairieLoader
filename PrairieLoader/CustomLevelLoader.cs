using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityObject = UnityEngine.Object;

namespace PrairieLoader;

[ConfigureSingleton(SingletonFlags.PersistAutoInstance)]
public sealed class CustomLevelLoader : MonoSingleton<CustomLevelLoader> {
	static string[] DoNotDestroy = new string[] {
		"NAVMESH",
		"BeamDirectionSetter",
		"FirstRoom",
		"StatsManager",
		"Pit (2)",
		"Canvas",
		"EventSystem",
		"GameController",
		"Player",
		"CheatBinds",
		"PlatformerController(Clone)",
		"CheckPointsController",
		"OutOfBounds"
	};

	AssetBundle? levelToLoad = null;

	public override void Awake() {
		base.Awake();

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if(scene.name != "DebugRoom") {
			return;
		}
		if(levelToLoad == null) {
			return;
		}

		foreach(GameObject go in SceneManager.GetActiveScene().GetRootGameObjects()) {
			if(!DoNotDestroy.Contains(go.name)) {
				Destroy(go);
			}
		}

		string sceneName = levelToLoad.GetAllScenePaths()[0];
		MainPlugin.logger.LogDebug(sceneName);
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += (op) => {
			InitLevelScene();
		};
	}

	private void InitLevelScene() {
		MainPlugin.logger.LogInfo("Regenerating Navmesh");
		GameObject.Find("NAVMESH").GetComponent<NavMeshSurface>().BuildNavMesh();
	}

	public void LoadLevel(string name) {
		levelToLoad = AssetBundle.LoadFromFile(Path.Combine(MainPlugin.MainPath, "levelcontent"));
		// Load debug room (empty enough scene)
		SceneManager.LoadScene("DebugRoom");
	}

	internal static void EnsureSDKAssemblyLoaded() {
		string path = Path.Combine(MainPlugin.MainPath, "PrairieLoader.SDK.dll");
		MainPlugin.logger.LogDebug($"Loading assembly from {path}");
		Assembly.LoadFile(path);
	}
}
