using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace PrairieLoader.Commands;

internal static class InitCommands {
	public static void Init() {
		GameConsole.Console.Instance.RegisterCommands(new GameConsole.ICommand[] { 
			new LoadLevelCommand()
		});
	}
}
