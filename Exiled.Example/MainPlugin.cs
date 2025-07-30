using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Example.Events;
using System;
using Random = System.Random;

namespace Exiled.Example;

public class MainPlugin : Plugin<Config>
{
    public override string Author { get; } = "DeadServer Team";

    public override string Name { get; } = "Example";

    public override string Prefix { get; } = "Example";

    public override Version Version { get; } = new(0, 0, 0);

    public override Version RequiredExiledVersion { get; } = new(9, 6, 1);

    public override PluginPriority Priority { get; } = PluginPriority.Last;

    // Always use these to get "MainPlugin" data or yaml config data
    public static MainPlugin Singleton { get; private set; }

    public static Config Configs => Singleton.Config;

    public static Random Random { get; private set; }

    // Personally I prefer to just register/manage events where they are logically needed in the code but this makes sense for some plugins
    private ServerHandler serverHandler;
    private PlayerHandler playerHandler;
    private WarheadHandler warheadHandler;
    private MapHandler mapHandler;
    private ItemHandler itemHandler;
    private Scp914Handler scp914Handler;
    private Scp096Handler scp096Handler;

    public override void OnEnabled()
    {
        RegisterEvents();

        Log.Warn($"I correctly read the string config, its value is: {Config.String}");
        Log.Warn($"I correctly read the int config, its value is: {Config.Int}");
        Log.Warn($"I correctly read the float config, its value is: {Config.Float}");

        base.OnEnabled();
    }

    public override void OnDisabled()
    {
        UnregisterEvents();
        base.OnDisabled();
    }

    private void RegisterEvents()
    {
        serverHandler = new ServerHandler();
        playerHandler = new PlayerHandler();
        warheadHandler = new WarheadHandler();
        mapHandler = new MapHandler();
        itemHandler = new ItemHandler();
        scp914Handler = new Scp914Handler();
        scp096Handler = new Scp096Handler();

        Exiled.Events.Handlers.Server.WaitingForPlayers += serverHandler.OnWaitingForPlayers;
        Exiled.Events.Handlers.Server.RoundStarted += serverHandler.OnRoundStarted;

        Exiled.Events.Handlers.Player.Destroying += playerHandler.OnDestroying;
        Exiled.Events.Handlers.Player.Spawned += playerHandler.OnSpawned;
        Exiled.Events.Handlers.Player.Escaping += playerHandler.OnEscaping;
        Exiled.Events.Handlers.Player.Hurting += playerHandler.OnHurting;
        Exiled.Events.Handlers.Player.Dying += playerHandler.OnDying;
        Exiled.Events.Handlers.Player.Died += playerHandler.OnDied;
        Exiled.Events.Handlers.Player.ChangingRole += playerHandler.OnChangingRole;
        Exiled.Events.Handlers.Player.ChangingItem += playerHandler.OnChangingItem;
        Exiled.Events.Handlers.Player.UsingItem += playerHandler.OnUsingItem;
        Exiled.Events.Handlers.Player.PickingUpItem += playerHandler.OnPickingUpItem;
        Exiled.Events.Handlers.Player.DroppingItem += playerHandler.OnDroppingItem;
        Exiled.Events.Handlers.Player.Verified += playerHandler.OnVerified;
        Exiled.Events.Handlers.Player.FailingEscapePocketDimension += playerHandler.OnFailingEscapePocketDimension;
        Exiled.Events.Handlers.Player.EscapingPocketDimension += playerHandler.OnEscapingPocketDimension;
        Exiled.Events.Handlers.Player.UnlockingGenerator += playerHandler.OnUnlockingGenerator;
        Exiled.Events.Handlers.Player.PreAuthenticating += playerHandler.OnPreAuthenticating;
        Exiled.Events.Handlers.Player.Shooting += playerHandler.OnShooting;
        Exiled.Events.Handlers.Player.ReloadingWeapon += playerHandler.OnReloading;
        Exiled.Events.Handlers.Player.ReceivingEffect += playerHandler.OnReceivingEffect;

        Exiled.Events.Handlers.Warhead.Stopping += warheadHandler.OnStopping;
        Exiled.Events.Handlers.Warhead.Starting += warheadHandler.OnStarting;

        Exiled.Events.Handlers.Scp106.Teleporting += playerHandler.OnTeleporting;

        Exiled.Events.Handlers.Scp914.Activating += playerHandler.OnActivating;
        Exiled.Events.Handlers.Scp914.ChangingKnobSetting += playerHandler.OnChangingKnobSetting;
        Exiled.Events.Handlers.Scp914.UpgradingPlayer += playerHandler.OnUpgradingPlayer;

        Exiled.Events.Handlers.Map.ExplodingGrenade += mapHandler.OnExplodingGrenade;
        Exiled.Events.Handlers.Map.GeneratorActivating += mapHandler.OnGeneratorActivated;

        Exiled.Events.Handlers.Item.ChangingAmmo += itemHandler.OnChangingAmmo;
        Exiled.Events.Handlers.Item.ChangingAttachments += itemHandler.OnChangingAttachments;
        Exiled.Events.Handlers.Item.ReceivingPreference += itemHandler.OnReceivingPreference;

        Exiled.Events.Handlers.Scp914.UpgradingPickup += scp914Handler.OnUpgradingItem;

        Exiled.Events.Handlers.Scp096.AddingTarget += scp096Handler.OnAddingTarget;
    }

    private void UnregisterEvents()
    {
        Exiled.Events.Handlers.Server.WaitingForPlayers -= serverHandler.OnWaitingForPlayers;
        Exiled.Events.Handlers.Server.RoundStarted -= serverHandler.OnRoundStarted;

        Exiled.Events.Handlers.Player.Destroying -= playerHandler.OnDestroying;
        Exiled.Events.Handlers.Player.Dying -= playerHandler.OnDying;
        Exiled.Events.Handlers.Player.Died -= playerHandler.OnDied;
        Exiled.Events.Handlers.Player.ChangingRole -= playerHandler.OnChangingRole;
        Exiled.Events.Handlers.Player.ChangingItem -= playerHandler.OnChangingItem;
        Exiled.Events.Handlers.Player.PickingUpItem += playerHandler.OnPickingUpItem;
        Exiled.Events.Handlers.Player.Verified -= playerHandler.OnVerified;
        Exiled.Events.Handlers.Player.FailingEscapePocketDimension -= playerHandler.OnFailingEscapePocketDimension;
        Exiled.Events.Handlers.Player.EscapingPocketDimension -= playerHandler.OnEscapingPocketDimension;
        Exiled.Events.Handlers.Player.UnlockingGenerator -= playerHandler.OnUnlockingGenerator;
        Exiled.Events.Handlers.Player.PreAuthenticating -= playerHandler.OnPreAuthenticating;

        Exiled.Events.Handlers.Warhead.Stopping -= warheadHandler.OnStopping;
        Exiled.Events.Handlers.Warhead.Starting -= warheadHandler.OnStarting;

        Exiled.Events.Handlers.Scp106.Teleporting -= playerHandler.OnTeleporting;

        Exiled.Events.Handlers.Scp914.Activating -= playerHandler.OnActivating;
        Exiled.Events.Handlers.Scp914.ChangingKnobSetting -= playerHandler.OnChangingKnobSetting;

        Exiled.Events.Handlers.Map.ExplodingGrenade -= mapHandler.OnExplodingGrenade;
        Exiled.Events.Handlers.Map.GeneratorActivating -= mapHandler.OnGeneratorActivated;

        Exiled.Events.Handlers.Item.ChangingAmmo -= itemHandler.OnChangingAmmo;
        Exiled.Events.Handlers.Item.ChangingAttachments -= itemHandler.OnChangingAttachments;
        Exiled.Events.Handlers.Item.ReceivingPreference -= itemHandler.OnReceivingPreference;

        Exiled.Events.Handlers.Scp914.UpgradingPickup -= scp914Handler.OnUpgradingItem;

        Exiled.Events.Handlers.Scp096.AddingTarget -= scp096Handler.OnAddingTarget;

        serverHandler = null;
        playerHandler = null;
        warheadHandler = null;
        mapHandler = null;
        itemHandler = null;
        scp914Handler = null;
        scp096Handler = null;
    }
}