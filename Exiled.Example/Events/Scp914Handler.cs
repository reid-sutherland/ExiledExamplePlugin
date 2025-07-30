using Exiled.API.Features;
using Exiled.Events.EventArgs.Scp914;

namespace Exiled.Example.Events;

internal sealed class Scp914Handler
{
    public void OnUpgradingItem(UpgradingPickupEventArgs ev)
    {
        Log.Info($"Item being upgraded\n[Type]: {ev.Pickup.Type}\n[Weight]: {ev.Pickup.Weight}\n[Output Position]: {ev.OutputPosition}\n[Knob Setting]: {ev.KnobSetting}");
    }
}