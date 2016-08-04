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
 


        public static Dictionary<TeamCountry, Color> CoopTeamColors = new Dictionary<TeamCountry, Color>()
            {
                {TeamCountry.Denmark,          new Color32(255, 0, 0 , 255) },

            };


}

