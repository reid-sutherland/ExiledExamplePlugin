using CommandSystem;
using Exiled.Permissions.Extensions; // Use this if you want to add perms
using System;

namespace Exiled.Example.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))] // You can change the command handler
public class ParentCommandExample : ParentCommand
{
    public ParentCommandExample()
    {
        LoadGeneratedCommands();

        // Use this to load commands for the parent command
    }

    public override string Command { get; } = "yourcommand";   // COMMAND

    public override string[] Aliases { get; } = { "yc" };   // ALIASES, is dont necessary to add aliases, if you want to add a aliase just put = null;

    public override string Description { get; } = "YOUR DESC";   // PARENT COMMAND DESC

    public override void LoadGeneratedCommands() // Put here your commands (the other commands dont need the [CommandHandler(typeof())]
    {
        // to register commands put that in your parent command, change the Parent Test to your command class name
        RegisterCommand(new ParentTest());
    }

    // Here starts your command code
    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        // If you want to add permissions you need to use that
        if (!sender.CheckPermission("exiled.parenttest"))
        {
            response = "You dont have perms";
            return false;
        }

        // Put here your code
        // Make sure to put return and response here
        response = "Done!";
        return true;
    }
}

// Example Command for the parent, in a normal command isnt necessary to put override in the command name, desc etc
public class ParentTest : ICommand
{
    /// <inheritdoc />
    public string Command { get; } = "subcommand";

    /// <inheritdoc />
    public string[] Aliases { get; } = { "sbc" };

    /// <inheritdoc />
    public string Description { get; } = "YOUR DESC";

    /// <inheritdoc />
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        // YOUR CODE
        response = "Done!";
        return true;
    }
}