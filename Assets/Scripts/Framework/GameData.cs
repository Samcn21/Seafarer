using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData
{
    public enum GameStates
    {
        Begin,
        PinCode,
        Ready,
        Play,
        Winning,
        Report
    }

    public enum InternalEventType
    {
        GAME_INIT,
        GAME_END,
        AMMO_CHANGE,
        HEALTH_CHANGE,
        DEAD
    }

    public enum TeamCountry
    {
        Denmark,
        England,
        France,
        Germany,
        Holland,
        Portugal,
        Spain,
        Venice
    }

    public enum TeamCapital
    {
        Copenhagen,
        London,
        Paris,
        Berlin,
        Amsterdam,
        Lisbon,
        Madrid,
        Venice
    }

    public static Dictionary<TeamCountry, TeamCapital> TeamCC = new Dictionary<TeamCountry, TeamCapital>() 
        {
            {TeamCountry.Denmark,   TeamCapital.Copenhagen  },
            {TeamCountry.England,   TeamCapital.London      },
            {TeamCountry.France,    TeamCapital.Paris       },
            {TeamCountry.Germany,   TeamCapital.Berlin      },
            {TeamCountry.Holland,   TeamCapital.Amsterdam   },
            {TeamCountry.Portugal,  TeamCapital.Lisbon      },
            {TeamCountry.Spain,     TeamCapital.Madrid      },
            {TeamCountry.Venice,    TeamCapital.Venice      }
        };
    /*
    public enum Team 
    {
        Blue = 0,
        Cyan = 1,
        Purple = 2,
        Yellow = 3,
        Neutral = -1
    }

    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    public enum PlayerSourceDirection
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }



    public static Dictionary<Team, Color> CoopTeamColors = new Dictionary<Team, Color>()
        {
            {Team.Cyan,          new Color32(0, 255, 255 , 255) },
            {Team.Purple,        new Color32(255, 0, 255 , 255) },
            {Team.Neutral,       new Color32(232, 232, 232 , 255) }
        };

    public enum SourceMachineStates
    {
        SourceMachineBlue,
        SourceMachineCyan,
        SourceMachinePurple,
        SourceMachineYellow
    }

    public enum CenterMachineStates
    {
        CenterMachineNeutral,
        CenterMachineBlue,
        CenterMachineCyan,
        CenterMachinePurple,
        CenterMachineYellow
    }

    public enum ControllerStates 
    {
        XboxController,
        XboxLeftScroll,
        XboxA,
        XboxB,
        XboxStart,
        PsController,
        PsLeftScroll,
        PsX,
        PsCircle,
        PsStart
    }

    public enum ConveyorBeltStates 
    {
        CornerTopRight,
        CornerTopLeft,
        CornerBottomRight,
        CornerBottomLeft,
        Straight
    }
    public enum PipesStates
    {
        PipeNeutralEmptyCorner,
        PipeNeutralEmptyT,
        PipeNeutralEmptyStraight,
        PipeNeutralEmptyCross,

        PipeNeutralAllowedCorner,
        PipeNeutralAllowedT,
        PipeNeutralAllowedStraight,
        PipeNeutralAllowedCross,

        PipeNeutralForbiddenCorner,
        PipeNeutralForbiddenT,
        PipeNeutralForbiddenStraight,
        PipeNeutralForbiddenCross,

        PipeBlueEmptyCorner,
        PipeBlueEmptyT,
        PipeBlueEmptyStraight,
        PipeBlueEmptyCross,

        PipeCyanEmptyCorner,
        PipeCyanEmptyT,
        PipeCyanEmptyStraight,
        PipeCyanEmptyCross,

        PipePurpleEmptyCorner,
        PipePurpleEmptyT,
        PipePurpleEmptyStraight,
        PipePurpleEmptyCross,

        PipeYellowEmptyCorner,
        PipeYellowEmptyT,
        PipeYellowEmptyStraight,
        PipeYellowEmptyCross,

        PipeBlueFullCorner,
        PipeBlueFullT,
        PipeBlueFullStraight,
        PipeBlueFullCross,

        PipeCyanFullCorner,
        PipeCyanFullT,
        PipeCyanFullStraight,
        PipeCyanFullCross,

        PipePurpleFullCorner,
        PipePurpleFullT,
        PipePurpleFullStraight,
        PipePurpleFullCross,

        PipeYellowFullCorner,
        PipeYellowFullT,
        PipeYellowFullStraight,
        PipeYellowFullCross
    }

    public enum PlayerState
    {
        IdleFront,
        MovementFront,
        IdleBack,
        MovementBack,
        IdleRight,
        MovementRight,
        IdleLeft,
        MovementLeft,
        PipeGrabFront,
        PipeGrabBack,
        PipeGrabRight,
        PipeGrabLeft,
        PipePlaceFront,
        PipePlaceBack,
        PipePlaceRight,
        PipePlaceLeft,
        MovementFrontCarryPipe,
        MovementBackCarryPipe,
        MovementRightCarryPipe,
        MovementLeftCarryPipe,
        Dance
    }

    public enum SpriteSheet
    {
        ConveyorBelt,
        Character,
        Pipe,
        PipeNeutral,
        CenterMachine,
        SourceMachine,
        XboxController,
        FlameMachine,
        PsController
    }

    public enum AudioClipState
    {
        Music,
        PickupPipe,
        PlacePipe,
        PushOthers,
        Walking,
        Explosion,
        Winning,
        RotatePipe
    }



    public class Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Coordinate c = obj as Coordinate;
            if ((System.Object)c == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (x == c.x) && (y == c.y);
        }

        public override string ToString()
        {
            return "[" + x + "," + y + "]";
        }
    }

    */
}

