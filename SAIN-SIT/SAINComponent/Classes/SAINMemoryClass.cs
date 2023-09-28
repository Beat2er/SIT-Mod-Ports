﻿using EFT;
using SAIN.SAINComponent.Classes.Decision;
using System.Collections.Generic;
using UnityEngine;

namespace SAIN.SAINComponent.Classes
{
    public class SAINMemoryClass : SAINBase, ISAINClass
    {
        public SAINMemoryClass(SAINComponentClass sain) : base(sain)
        {
            Decisions = new DecisionWrapper(sain);
        }

        public void Init() 
        {
        }

        public void Update()
        {
            if (UpdateHealthTimer < Time.time)
            {
                UpdateHealthTimer = Time.time + 0.33f;
                HealthStatus = Player.HealthStatus;
            }
        }

        public void Dispose()
        {
        }

        public Collider BotZoneCollider => BotZone?.Collider;
        public AIPlaceInfo BotZone => BotOwner.AIData.PlaceInfo;

        public List<Player> VisiblePlayers = new List<Player>();

        public List<string> VisiblePlayerIds = new List<string>();

        private float UpdateHealthTimer = 0f;

        public Vector3? ExfilPosition { get; set; }
        public bool CannotExfil { get; set; }

        public bool Healthy => HealthStatus == ETagStatus.Healthy;
        public bool Injured => HealthStatus == ETagStatus.Injured;
        public bool BadlyInjured => HealthStatus == ETagStatus.BadlyInjured;
        public bool Dying => HealthStatus == ETagStatus.Dying;

        public ETagStatus HealthStatus { get; private set; }

        public Vector3 UnderFireFromPosition { get; set; }

        public DecisionWrapper Decisions { get; private set; }
    }
}