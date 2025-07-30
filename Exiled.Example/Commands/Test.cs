using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using System;

namespace Exiled.Example.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class Test : ICommand
{
    public string Command { get; } = "test";

    public string[] Aliases { get; } = new[] { "t" };

    public string Description { get; } = "A simple test command.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Player player = Player.Get(sender);

        Log.Warn($"{player.Items.Count} -- {player.Inventory.UserInventory.Items.Count}");

        foreach (Player item in Player.List)
        {
            Log.Warn(item);
        }

        foreach (Pickup pickup in Pickup.List)
        {
            Log.Warn($"{pickup.Type} ({pickup.Serial}) -- {pickup.Position}");
        }

        foreach (PocketDimensionTeleport teleport in Map.PocketDimensionTeleports)
        {
            Log.Warn($"{teleport._type}");
        }

        player.ClearInventory();
        response = $"{player.Nickname} sent the command!";

        // Return true if the command was executed successfully; otherwise, false.
        return true;
    }
}