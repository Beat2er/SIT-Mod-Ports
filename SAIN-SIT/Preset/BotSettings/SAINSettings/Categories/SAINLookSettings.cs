﻿using SAIN.Attributes;
using System.ComponentModel;

namespace SAIN.Preset.BotSettings.SAINSettings.Categories
{
    public class SAINLookSettings
    {
        [NameAndDescription(
            "Base Vision Speed Multiplier",
            "The Base vision speed multiplier, affects all ranges to enemy. " +
            "Bots will see this much faster, or slower, at any range. " +
            "Higher is slower speed, so 1.5 would result in bots taking 1.5 times longer to spot an enemy")]
        [Default(1f)]
        [MinMax(0.1f, 3f, 10f)]
        public float VisionSpeedModifier = 1;

        [NameAndDescription("Close Vision Speed Multiplier",
            "Vision speed multiplier at close range. " +
                "Bots will see this much faster, or slower, at close range. " +
                "Higher is slower speed, so 1.5 would result in bots taking 1.5 times longer to spot an enemy")]
        [Default(1f)]
        [MinMax(0.1f, 3f, 10f)]
        public float CloseVisionSpeed = 1;

        [NameAndDescription("Far Vision Speed Multiplier",
            "Vision speed multiplier at Far range, the range is defined by (Close/Far Threshold Property)."
            + "Bots will see this much faster, or slower, at Far range. "
            + "Higher is slower speed, so 1.5 would result in bots taking 1.5 times longer to spot an enemy")]
        [Default(1f)]
        [MinMax(0.1f, 3f, 10f)]
        public float FarVisionSpeed = 1;

        [NameAndDescription("Close/Far Threshold",
            "The Distance that defines what is close or far for the Close Speed and Far Speed properties.")]
        [Default(50)]
        [MinMax(5, 150)]
        public float CloseFarThresh = 50;

        [Name("Can Use Flashlights")]
        [Default(true)]
        public bool CAN_USE_LIGHT = true;

        [Name("Full 360 Vision Cheat Vision")]
        [Default(false)]
        [Advanced]
        public bool FULL_SECTOR_VIEW = false;

        [NameAndDescription("Vision Speed Distance Clamp",
            "Lower Bot Vision Speed by distance up to a maximum of this value")]
        [Default(500f)]
        [MinMax(50, 500f)]
        [Advanced]
        public float MAX_DIST_CLAMP_TO_SEEN_SPEED = 500f;

        [NameAndDescription("NightVision On Distance",
            "After a bot is below this number in their vision distance, they will turn on night vision if available")]
        [Default(75f)]
        [MinMax(10f, 250f)]
        [Advanced]
        public float NIGHT_VISION_ON = 75f;

        [NameAndDescription("NightVision Off Distance",
            "After a bot is above this number in their vision distance, they will turn off night vision if enabled")]
        [Default(125f)]
        [MinMax(10f, 250f)]
        [Advanced]
        public float NIGHT_VISION_OFF = 125f;

        [NameAndDescription("NightVision Visible Distance",
            "How far a bot can see with NightVision Enabled")]
        [Default(125f)]
        [MinMax(10f, 250f)]
        [Advanced]
        public float NIGHT_VISION_DIST = 125f;

        [NameAndDescription("NightVision Visible Angle",
            "The Maximum Angle of a bot's cone of vision with NightVision Enabled")]
        [Default(90f)]
        [MinMax(25, 180)]
        [Advanced]
        public float VISIBLE_ANG_NIGHTVISION = 90f;

        [Hidden]
        public float LOOK_THROUGH_PERIOD_BY_HIT = 0f;

        [NameAndDescription("FlashLight On Distance",
            "After a bot is below this number in their vision distance, they will turn on their flashlight if available")]
        [Default(40f)]
        [MinMax(10, 100f)]
        [Advanced]
        public float LightOnVisionDistance = 40f;

        [NameAndDescription("FlashLight Visible Angle",
            "The Maximum Angle of a bot's cone of vision with Flashlight Enabled")]
        [Default(30f)]
        [MinMax(10, 180)]
        [Advanced]
        public float VISIBLE_ANG_LIGHT = 30f;

        [NameAndDescription("FlashLight Visible Distance",
            "How far a bot can see with a Flashlight Enabled")]
        [Default(50f)]
        [MinMax(10, 100f)]
        [Advanced]
        public float VISIBLE_DISNACE_WITH_LIGHT = 50f;

        [NameAndDescription("Lose Vision Ability Time",
            "How Long after losing vision a bot will still be able to sense an enemy")]
        [Default(0.25f)]
        [MinMax(0.01f, 3f, 100f)]
        [Advanced]
        public float GOAL_TO_FULL_DISSAPEAR = 0.25f;

        [NameAndDescription("Lose Vision Ability Foliage Time",
            "How Long after losing vision a bot will still be able to sense an enemy")]
        [Default(0.15f)]
        [MinMax(0.01f, 3f, 100f)]
        [Advanced]
        public float GOAL_TO_FULL_DISSAPEAR_GREEN = 0.15f;

        [NameAndDescription("Lose Shoot Ability Time",
            "How Long after losing vision a bot will still be able to shoot an enemy")]
        [Default(0.01f)]
        [MinMax(0.01f, 3f, 100f)]
        [Advanced]
        public float GOAL_TO_FULL_DISSAPEAR_SHOOT = 0.01f;

        [NameAndDescription("Lose Shoot Ability Time",
            "How far into grass a bot will be able to see, how far the depth must be to lose visibilty")]
        [Default(1f)]
        [MinMax(0.0f, 1f, 100f)]
        [Advanced]
        public float MAX_VISION_GRASS_METERS = 1f;

        [Hidden]
        public float MAX_VISION_GRASS_METERS_OPT = 1f;

        [Hidden]
        public float MAX_VISION_GRASS_METERS_FLARE = 4f;

        [Hidden]
        public float MAX_VISION_GRASS_METERS_FLARE_OPT = 0.25f;

        [NameAndDescription("Vision Distance No Foliage",
            "Bots will not see foliage at this distance or less, so if a target is below this number in distance, they will ignore foliage")]
        [Default(3f)]
        [MinMax(1f, 100f)]
        [Advanced]
        public float NO_GREEN_DIST = 3f;

        [NameAndDescription("Vision Distance No Grass",
            "Bots will not see grass at this distance or less, so if a target is below this number in distance, they will ignore grass")]
        [Default(3f)]
        [MinMax(1f, 100f)]
        [Advanced]
        public float NO_GRASS_DIST = 3f;
    }
}