using System;
using System.Collections.Generic;
using System.Text;
using GameConsole;

namespace PrairieLoader.Commands;

public sealed class LoadLevelCommand : ICommand {
	public string Name => Command;
	public string Description => "Loads a level.";
	public string Command => "prairie_load";

	public void Execute(GameConsole.Console con, string[] args) {
		if(args.Length != 1) throw new ArgumentException("There should only be one argument.");
		CustomLevelLoader.Instance.LoadLevel(args[0]);
	}
}
