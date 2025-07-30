using CameraShaking;
using CustomPlayerEffects;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp106;
using Exiled.Events.EventArgs.Scp914;
using MEC;
using PlayerRoles;

namespace Exiled.Example.Events;

internal sealed class PlayerHandler
{
    public void OnDied(DiedEventArgs ev)
    {
        if (ev.Player is null)
        {
            return;
        }

        Log.Info($"{ev.Player.Nickname} ({ev.Player.Role}) died from {ev.DamageHandler.Type}! {ev.Attacker.Nickname} ({ev.Attacker.Role}) killed him!");
    }

    public void OnChangingRole(ChangingRoleEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} ({ev.Player.Role}) is changing his role! The new role will be {ev.NewRole}!");

        if (ev.NewRole == RoleTypeId.Tutorial)
        {
            ev.Items.Clear();
            ev.Items.Add(ItemType.Flashlight);
            ev.Items.Add(ItemType.Medkit);

            Timing.CallDelayed(0.5f, () => ev.Player.AddItem(ItemType.Radio));
        }
    }

    public void OnChangingItem(ChangingItemEventArgs ev)
    {
        Timing.CallDelayed(
            2f,
            () =>
            {
                if (ev.Player?.CurrentItem is Firearm firearm)
                {
                    Log.Info($"{ev.Player.Nickname} has a firearm!");
                    firearm.Recoil = new RecoilSettings(0, 0, 0, 0, 0);
                }
            });

        Log.Info($"{ev.Player.Nickname} is changing his {(ev.Player?.CurrentItem is null ? "NONE" : ev.Player?.CurrentItem?.Type.ToString())} item to {(ev.Item is null ? "NONE" : ev.Item.Type.ToString())}!");
    }

    public void OnTeleporting(TeleportingEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is teleporting to {ev.Position} as SCP-106!");
    }

    public void OnActivating(ActivatingEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is activating SCP-914!");
    }

    public void OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is failing to escape from the pocket dimension!");
    }

    public void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is escaping from the pocket dimension and will be teleported at {ev.TeleportPosition}");
    }

    public void OnChangingKnobSetting(ChangingKnobSettingEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is changing the knob setting of SCP-914 to {ev.KnobSetting}");
    }

    public void OnVerified(VerifiedEventArgs ev)
    {
        // You can put some code here to trigger when a player joins, e.g. welcome message/broadcast
        // Broadcasts changed a lot and it's gross so I didn't bother updating
    }

    public void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is trying to unlock a generator in {ev.Player.CurrentRoom} room");
    }

    public void OnDestroying(DestroyingEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} ({ev.Player.Role}) is leaving the server!");
    }

    public void OnDying(DyingEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} ({ev.Player.Role}) is getting killed by {ev.Attacker?.Nickname ?? "None"} ({ev.Attacker?.Role?.ToString() ?? "None"})!");
    }

    public void OnPreAuthenticating(PreAuthenticatingEventArgs ev)
    {
        Log.Info($"{ev.UserId} is pre-authenticating from {ev.Country} ({ev.Request.RemoteEndPoint}) with flags {ev.Flags}!");
    }

    public void OnPickingUpItem(PickingUpItemEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} has picked up a {ev.Pickup.Type}! Weight: {ev.Pickup.Weight} Serial: {ev.Pickup.Serial}.");
        Log.Warn($"{ev.Pickup.Base.Info.Serial} -- {ev.Pickup.Base.NetworkInfo.Serial}");
    }

    public void OnUsingItem(UsingItemEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is trying to use {ev.Item.Type}.");

        if (ev.Item.Type == ItemType.Adrenaline)
        {
            Log.Info($"{ev.Player.Nickname} was stopped from using their {ev.Item.Type}!");
            ev.IsAllowed = false;
        }
    }

    public void OnShooting(ShootingEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is shooting a {ev.Player.CurrentItem.Type}! Target Pos: {ev.ClaimedTarget?.Position} Direction: {ev.Direction} Allowed: {ev.IsAllowed}");
    }

    public void OnReloading(ReloadingWeaponEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is reloading their {ev.Firearm.Type}. They have {ev.Firearm.MagazineAmmo} ammo. Using ammo type {ev.Firearm.AmmoType}");
    }

    public void OnReceivingEffect(ReceivingEffectEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is receiving effect {ev.Effect}. Duration: {ev.Duration} New Intensity: {ev.Intensity} Old Intensity: {ev.CurrentIntensity}");

        if (ev.Effect is Invigorated)
        {
            Log.Info($"{ev.Player.Nickname} is being rejected the {nameof(Invigorated)} effect!");
            ev.IsAllowed = false;
        }
    }

    public void OnUpgradingPlayer(UpgradingPlayerEventArgs ev)
    {
        Log.Info($"SCP-914 is processing {ev.Player.Nickname} on {ev.KnobSetting}. Upgrade Items: {ev.UpgradeItems} Held Items only: {ev.HeldOnly}");
    }

    public void OnDroppingItem(DroppingItemEventArgs ev)
    {
        Log.Info($"{ev.Player.Nickname} is dropping {ev.Item.Type}!");

        if (ev.Item.Type == ItemType.Adrenaline)
        {
            ev.IsAllowed = false;
        }
    }

    public void OnSpawned(SpawnedEventArgs ev)
    {
        if (ev.Player.Role.Type == RoleTypeId.Scientist)
        {
            ev.Player.Position = RoleTypeId.Tutorial.GetRandomSpawnLocation().Position;
            ev.Player.ResetInventory(new ItemType[] { ItemType.Snowball, ItemType.Jailbird, ItemType.Snowball, ItemType.Snowball, ItemType.Snowball, ItemType.Radio, ItemType.Jailbird });
        }
    }

    public void OnEscaping(EscapingEventArgs ev)
    {
        if (ev.Player.Role == RoleTypeId.Scientist)
        {
            ev.NewRole = RoleTypeId.Tutorial;
        }

        Log.Info($"{ev.Player.Nickname} is trying to escape! Their new role will be {ev.NewRole}");
    }

    public void OnHurting(HurtingEventArgs ev)
    {
        Log.Info($"{ev.Player} is being hurt by {ev.DamageHandler.Type}");

        if (ev.Player.Role == RoleTypeId.Scientist)
        {
            Log.Info("Target is a nerd, setting damage to 1 because it's mean to bully nerds.");
            ev.Amount = 1f;
        }
    }
}