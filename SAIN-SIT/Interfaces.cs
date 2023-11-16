﻿using BepInEx.Logging;
using EFT;
using SAIN.Attributes;
using SAIN.SAINComponent;
using SAIN.SAINComponent.BaseClasses;
using System.Collections.Generic;

namespace SAIN
{
    public interface ISAINSubComponent
    {
        void Init(SAINComponentClass sain);
        BotOwner BotOwner { get; }
        Player Player { get; }
    }

    public interface IBotComponent
    {
        bool Init(SAINPersonClass person);
        SAINPersonClass Person { get; }
        BotOwner BotOwner { get; }
        Player Player { get; }
    }

    public interface IAIDetailsComponent
    {
        void Init(Player player);
        Player Player { get; }
    }

    public interface ISAINClass
    {
        void Init();
        void Update();
        void Dispose();
    }
}
