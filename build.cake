using System;

const string PROJECT_NAME = "PrairieLoader";
const string SOLUTION_NAME = "PrairieLoader";

void copy(string source, string dest) {
	System.IO.File.Copy(source, dest);
	Information($"{source} -> {dest}");
}

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");
var gamePath = Argument("gamePath", System.IO.Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"), "Steam", "steamapps", "common", "ULTRAKILL"));

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("RefreshLibs")
.Does(() => {

	string libFolder = $"./{PROJECT_NAME}/lib";

	CleanDirectory(libFolder);

	System.IO.Directory.CreateDirectory(libFolder);

	// Copy UK files
	foreach(var file in System.IO.Directory.GetFiles(System.IO.Path.Combine(gamePath, "ULTRAKILL_Data", "Managed"), "*.dll")) {
		if(file.Contains("mscorlib") || file.Contains("netstandard")) continue; // ignore .net standrad libraries
		if(file.Contains("System")) continue; // ignore system libraries
		if(file.Contains("TwitchLib")) continue; // ignore twitch API libraries because they cause werid issues

		string fileName = System.IO.Path.GetFileName(file);
		string dest = System.IO.Path.Combine(libFolder, fileName);
		copy(file, dest);
	}

	// Copy BepInEx files
	string bepinexCore = System.IO.Path.Combine(gamePath, "BepInEx", "core");
	copy(System.IO.Path.Combine(bepinexCore, "BepInEx.dll"), System.IO.Path.Combine(libFolder, "BepInEx.dll"));
	copy(System.IO.Path.Combine(bepinexCore, "BepInEx.xml"), System.IO.Path.Combine(libFolder, "BepInEx.xml"));
	copy(System.IO.Path.Combine(bepinexCore, "0Harmony.dll"), System.IO.Path.Combine(libFolder, "0Harmony.dll"));
});

Task("Clean")
.Does(() => {
	CleanDirectory($"./{PROJECT_NAME}/bin/{configuration}/netstandard2.0");
});

Task("Build")
.IsDependentOn("Clean")
.Does(() => {
	DotNetBuild($"./{SOLUTION_NAME}.sln", new DotNetBuildSettings {
		Configuration = configuration,
	});
});

Task("CopyToGame")
.Does(() => {
	string pluginFolder = System.IO.Path.Combine(gamePath, "BepInEx", "plugins", $"{PROJECT_NAME}");
	CleanDirectory(pluginFolder);
	System.IO.Directory.CreateDirectory(pluginFolder);

	copy($"./{PROJECT_NAME}/bin/{configuration}/netstandard2.0/{PROJECT_NAME}.dll", System.IO.Path.Combine(pluginFolder, $"{PROJECT_NAME}.dll"));
	copy($"./{PROJECT_NAME}.SDK/bin/{configuration}/netstandard2.0/{PROJECT_NAME}.SDK.dll", System.IO.Path.Combine(pluginFolder, $"{PROJECT_NAME}.SDK.dll"));

	copy($"./PrairieSDK/Built Bundles/levelcontent", System.IO.Path.Combine(pluginFolder, $"levelcontent"));
});

Task("CopyAndRun")
.IsDependentOn("CopyToGame")
.Does(() => {
	// copy-pasted from UMM
	var psi = new System.Diagnostics.ProcessStartInfo {
				FileName = @"steam://run/1229490",
				UseShellExecute = true,
				WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized
	};
	System.Diagnostics.Process.Start(psi);
});

Task("BuildCopyAndRun")
.IsDependentOn("Build")
.IsDependentOn("CopyAndRun")
.Does(() => {
	// nothing
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);