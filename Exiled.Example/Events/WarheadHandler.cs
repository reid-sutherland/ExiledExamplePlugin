using Exiled.API.Features;
using Exiled.Events.EventArgs.Warhead;

namespace Exiled.Example.Events;

internal sealed class WarheadHandler
{
    public void OnStopping(StoppingEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} stopped the warhead!");
    }

    public void OnStarting(StartingEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} started the warhead!");
    }
}