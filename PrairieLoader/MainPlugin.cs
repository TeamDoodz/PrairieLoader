using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BepInEx;
using BepInEx.Logging;
using PrairieLoader.Commands;

namespace PrairieLoader;

[BepInPlugin(GUID, NAME, VERSION)]
[BepInDependency("UMM")]
public sealed class MainPlugin : BaseUnityPlugin {
	const string NAME = nameof(PrairieLoader);
	const string GUID = "io.github.teamdoodz." + NAME;
	const string VERSION = "1.0.0";

	internal static ManualLogSource logger;

	internal static string MainPath = "rgwetewgtwfw";

	void Awake() {
		logger = Logger;
		MainPath = Path.GetDirectoryName(Info.Location);

		CustomLevelLoader.EnsureSDKAssemblyLoaded();
		InitCommands.Init();

		Logger.LogMessage($"{NAME} v{VERSION} Loaded!");
	}
}
