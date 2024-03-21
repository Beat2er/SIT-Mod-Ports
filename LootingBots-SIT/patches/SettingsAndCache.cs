using System.Reflection;
using Comfort.Common;
using StayInTarkov;
using EFT;
using EFT.InventoryLogic;
using EFT.UI;
using LootingBots.Patch.Components;
using LootingBots.Patch.Util;

namespace LootingBots.Patch
{
    public class SettingsAndCachePatch
    {
        public void Enable()
        {
            new CleanCacheOnRaidEnd().Enable();
            new EnableWeaponSwitching().Enable();
            new InteractPatch().Enable();
            new InventoryClosePatch().Enable();
        }
    }

    public class CleanCacheOnRaidEnd : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod(
                "Dispose",
                BindingFlags.Public | BindingFlags.Instance
            );
        }

        [PatchPrefix]
        private static void PatchPrefix()
        {
            LootingBots.LootLog.LogDebug($"Resetting Loot Cache");
            ActiveLootCache.Reset();
        }
    }

    /* Patch that enables all bots to be able to switch weapons. Values based on Usec/Bear bot values */
    public class EnableWeaponSwitching : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Settings9).GetMethod("ApplyPresetLocation");
        }

        [PatchPostfix]
        private static void PatchPostfix(
            BotLocationModifier modifier,
            ref Settings9 __instance,
            ref WildSpawnType ___wildSpawnType_0
        )
        {
            bool corpseLootEnabled = LootingBots.CorpseLootingEnabled.Value.IsBotEnabled(
                ___wildSpawnType_0
            );
            bool containerLootEnabled = LootingBots.ContainerLootingEnabled.Value.IsBotEnabled(
                ___wildSpawnType_0
            );
            bool itemLootEnabled = LootingBots.LooseItemLootingEnabled.Value.IsBotEnabled(
                ___wildSpawnType_0
            );

            if (corpseLootEnabled || containerLootEnabled || itemLootEnabled)
            {
                __instance.FileSettings.Shoot.CHANCE_TO_CHANGE_WEAPON = 80;
                __instance.FileSettings.Shoot.CHANCE_TO_CHANGE_WEAPON_WITH_HELMET = 40;
            }
        }
    }
    
    public class InventoryClosePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(InventoryScreen).GetMethod(
                "Close",
                BindingFlags.Public | BindingFlags.Instance
            );
        }

        [PatchPrefix]
        private static void PatchPrefix()
        {
            LootingBots.LootLog.LogWarning($"Clearing any active player loot");
            ActiveLootCache.PlayerLootId = "";
        }
    }

    /** Patch to mark any lootable interacted with by the player as active loot. Any bot that is currently pathing to that lootable should have their looting brain reset and will ignore the lootable until the player stops looting */
    public class InteractPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(Player).GetMethod(
                "Interact",
                BindingFlags.Public | BindingFlags.Instance
            );
        }

        [PatchPrefix]
        private static void PatchPrefix(IItemOwner loot, Callback callback, ref Player __instance)
        {
            // If the item we are looting is marked as active by a bot, cleanup its looting brain to stop it from looting the same object
            if (ActiveLootCache.ActiveLoot.TryGetValue(loot.RootItem.Id, out BotOwner botOwner) && !botOwner.BotsGroup.IsPlayerEnemy(__instance))
            {
                LootingBots.LootLog.LogError("Cleanup on bot brain");
                LootingBrain brain = botOwner.GetComponent<LootingBrain>();
                brain?.DisableTransactions();
            }

            ActiveLootCache.PlayerLootId = loot.RootItem.Id;
        }
    }
}
