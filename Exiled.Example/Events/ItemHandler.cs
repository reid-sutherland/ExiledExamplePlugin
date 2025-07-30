using Exiled.API.Features;
using Exiled.Events.EventArgs.Item;
using System.Linq;

namespace Exiled.Example.Events;

internal sealed class ItemHandler
{
    public void OnChangingAmmo(ChangingAmmoEventArgs ev)
    {
        Log.Info($"Durability of {ev.Firearm.Type} ({ev.OldAmmo}) is changing. New durability: {ev.NewAmmo}");
    }

    public void OnChangingAttachments(ChangingAttachmentsEventArgs ev)
    {
        string oldAttachments = ev.CurrentAttachmentIdentifiers.Aggregate(string.Empty, (current, attachmentIdentifier) => current + $"{attachmentIdentifier.Name}\n");
        string newAttachments = ev.NewAttachmentIdentifiers.Aggregate(string.Empty, (current, attachmentIdentifier) => current + $"{attachmentIdentifier.Name}\n");

        Log.Info($"Item {ev.Firearm.Type} attachments are changing. Old attachments:\n{oldAttachments} - New Attachments:\n{newAttachments}");
    }

    public void OnReceivingPreference(ReceivingPreferenceEventArgs ev)
    {
        Log.Info($"Receiving a preference from {ev.Player.Nickname} - Item: {ev.Item}");
    }
}