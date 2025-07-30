using Exiled.API.Features;

namespace Exiled.Example.Events;

internal sealed class ServerHandler
{
    public void OnWaitingForPlayers()
    {
        Log.Info("I'm waiting for players!"); // This is an example of information messages sent to your console!
    }

    public void OnRoundStarted()
    {
        Log.Info($"A round has started with {Player.Dictionary.Count} players!");
    }
}