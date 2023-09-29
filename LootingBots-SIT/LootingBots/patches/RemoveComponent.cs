﻿using EFT;
using SIT.Core;
using LootingBots.Patch.Components;
using System.Reflection;
using UnityEngine;
using SIT.Tarkov.Core;

namespace LootingBots.Patch
{
    internal class RemoveComponent : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotControllerClass).GetMethod(
                "Init",
                BindingFlags.Public | BindingFlags.Instance
            );
        }

        [PatchPostfix]
        private static void PatchPostfix(BotControllerClass __instance)
        {
            __instance.BotSpawner.OnBotRemoved += botOwner =>
            {
                if (botOwner.GetPlayer.TryGetComponent<LootingBrain>(out var component))
                {
                    Object.Destroy(component);
                }
            };
        }
    }
}
