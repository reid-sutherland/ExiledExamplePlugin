using Exiled.API.Features;
using Exiled.Events.EventArgs.Map;
using System.Linq;

namespace Exiled.Example.Events;

internal sealed class MapHandler
{
    public void OnExplodingGrenade(ExplodingGrenadeEventArgs ev)
    {
        Log.Info($"A grenade thrown by {ev.Player.Nickname} is exploding: {ev.Projectile.Type}\n[Targets]\n\n{string.Join("\n", ev.TargetsToAffect.Select(player => $"[{player.Nickname}]"))}");
    }

    public void OnGeneratorActivated(GeneratorActivatingEventArgs ev)
    {
        Log.Info($"A generator has been activated in {ev.Generator.Room.Type}!");
    }
}