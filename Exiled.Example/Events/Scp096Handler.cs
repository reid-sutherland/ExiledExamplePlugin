using Exiled.API.Features;
using Exiled.Events.EventArgs.Scp096;

namespace Exiled.Example.Events;

internal sealed class Scp096Handler
{
    public void OnAddingTarget(AddingTargetEventArgs ev)
    {
        Log.Info($"{ev.Target.Nickname} is being added to {ev.Player.Nickname} targets!");
    }
}