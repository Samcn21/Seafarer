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

    public enum GameStatus
    {
        UsingGPS,
        ConnectingAdminPanel,
        OnlinePhoton,
        HasPinCodePanel
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
        ___,
        Denmark,
        England,
        France,
        Germany,
        Holland,
        Portugal,
        Spain,
        Venice
    }

    public enum HitTarget
    {
        Me,
        Opponent,
        City
    }

    public enum TeamPlayMode
    {
        Exploring,
        Attacking
    }

    public enum City
    {
        Amsterdam,
        Berlin,
        Cario,
        Copenhagen,
        Esfahan,
        Fes,
        Istanbul,
        Lisbon,
        London,
        Madrid,
        MexicoCity,
        Paris,
        Seville,
        Tokyo,
        Venice
    }

    public enum CityStatus
    { 
        Neutral, 
        OccupiedByOneCountry, 
        OccupiedByAllies
    }

    public enum Continent
    {
        Africa,
        Asia,
        Australia,
        Europe,
        NorthAmerica,
        SouthAmerica
    }

    public enum DefenceStatus 
    {
        Free,
        UnderSiege,
        UnderAttack
    }

    public enum CitySet
    { 
        World25,
        World40,
        Europe25,
        Europe32,
        Asia20
    }

    public static Dictionary<TeamCountry, City> TeamCC = new Dictionary<TeamCountry, City>() 
        {
            {TeamCountry.Denmark,   City.Copenhagen  },
            {TeamCountry.England,   City.London      },
            {TeamCountry.France,    City.Paris       },
            {TeamCountry.Germany,   City.Berlin      },
            {TeamCountry.Holland,   City.Amsterdam   },
            {TeamCountry.Portugal,  City.Lisbon      },
            {TeamCountry.Spain,     City.Madrid      },
            {TeamCountry.Venice,    City.Venice      }
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

