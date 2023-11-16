﻿using BepInEx.Logging;
using EFT;
using SAIN.Preset.GlobalSettings;

namespace SAIN.SAINComponent
{
    public abstract class SAINBase : SAINComponentAbstract
    {
        public SAINBase(SAINComponentClass sain) : base (sain)
        {
        }

        public BotOwner BotOwner => SAIN.BotOwner;
        public Player Player => SAIN.Player;
        public GlobalSettingsClass GlobalSettings => SAINPlugin.LoadedPreset?.GlobalSettings;
    }

    public class SAINComponentAbstract
    {
        public SAINComponentAbstract(SAINComponentClass sain)
        {
            SAIN = sain;
        }

        public SAINComponentClass SAIN { get; private set; }
    }
}