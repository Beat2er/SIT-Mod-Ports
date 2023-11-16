﻿using BepInEx.Logging;
using Comfort.Common;
using DrakiaXYZ.BigBrain.Brains;
using EFT;
using UnityEngine;
using SAIN.SAINComponent.Classes;
using SAIN.SAINComponent.SubComponents;
using SAIN.SAINComponent;
using Systems.Effects;

namespace SAIN.Layers
{
    internal class ExtractAction : SAINAction
    {
        private static readonly string Name = typeof(ExtractAction).Name;
        public ExtractAction(BotOwner bot) : base(bot, Name)
        {
        }

        private Vector3? Exfil => SAIN.Memory.ExfilPosition;

        public override void Start()
        {
            BotOwner.PatrollingData.Pause();
        }

        public override void Stop()
        {
            BotOwner.PatrollingData.Unpause();
        }

        public override void Update()
        {
            float stamina = SAIN.Player.Physical.Stamina.NormalValue;
            if (SAIN.Enemy != null && SAIN.Enemy.Seen && (SAIN.Enemy.PathDistance < 50f || SAIN.Enemy.IsVisible))
            {
                NoSprint = true;
            }
            else if (stamina > 0.5f)
            {
                NoSprint = false;
            }
            else if (stamina < 0.1f)
            {
                NoSprint = true;
            }

            Vector3 point = Exfil.Value;
            float distance = (point - BotOwner.Position).sqrMagnitude;

            if (distance < 1f)
            {
                SAIN.Mover.SetTargetPose(0f);
                NoSprint = true;
            }
            else
            {
                SAIN.Mover.SetTargetPose(1f);
                SAIN.Mover.SetTargetMoveSpeed(1f);
            }

            if (ExtractStarted)
            {
                StartExtract(point);
            }
            else
            {
                MoveToExtract(distance, point);
            }

            if (BotOwner.BotState == EBotState.Active)
            {
                if (NoSprint)
                {
                    SAIN.Mover.Sprint(false);
                    SAIN.Steering.SteerByPriority();
                    Shoot.Update();
                }
                else
                {
                    SAIN.Steering.LookToMovingDirection();
                }
            }
        }

        private bool NoSprint;

        private void MoveToExtract(float distance, Vector3 point)
        {
            if (distance > 12f)
            {
                ExtractStarted = false;
            }
            if (distance < 6f)
            {
                ExtractStarted = true;
            }

            if (!ExtractStarted)
            {
                if (ReCalcPathTimer < Time.time)
                {
                    ExtractTimer = -1f;
                    ReCalcPathTimer = Time.time + 4f;
                    if (NoSprint)
                    {
                        BotOwner.Mover?.GoToPoint(point, true, 0.5f, false, false);
                    }
                    else
                    {
                        BotOwner.BotRun.Run(point, false);
                    }
                }
            }
        }

        private void StartExtract(Vector3 point)
        {
            if (ExtractTimer == -1f)
            {
                float timer = 5f * Random.Range(0.75f, 1.5f);
                ExtractTimer = Time.time + timer;

                Logger.LogInfo($"{BotOwner.name} Starting Extract Timer of {timer}");
            }

            if (ExtractTimer < Time.time)
            {
                Logger.LogInfo($"{BotOwner.name} Extracted at {point} at {System.DateTime.UtcNow}");

                var botgame = Singleton<IBotGame>.Instance;
                Player player = SAIN.Player;
                Singleton<Effects>.Instance.EffectsCommutator.StopBleedingForPlayer(player);
                BotOwner.Deactivate();
                BotOwner.Dispose();
                botgame.BotsController.BotDied(BotOwner);
                botgame.BotsController.DestroyInfo(player);
                Object.DestroyImmediate(BotOwner.gameObject);
                Object.Destroy(BotOwner);
            }
        }

        private bool ExtractStarted = false;
        private float ReCalcPathTimer = 0f;
        private float ExtractTimer = -1f;
    }
}