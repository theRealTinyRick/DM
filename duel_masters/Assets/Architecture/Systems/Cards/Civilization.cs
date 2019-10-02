/*
 Author: Aaron Hines
 Description: Defines civilations in the game
*/
using System.Collections.Generic;

namespace DM.Systems.Cards
{
    public enum Civ
    {
        Light, 
        Water,
        Darkness,
        Fire,
        Nature,

        Zero,
        Joker
    }

    public static class CivExtension
    {
        public static string GetName(this Civ civ)
        {
            switch (civ)
            {
                case Civ.Light:
                    return "Light";
                case Civ.Water:
                    return "Water";
                case Civ.Darkness:
                    return "Darkness";
                case Civ.Fire:
                    return "Fire";
                case Civ.Nature:
                    return "Nature";
                case Civ.Zero:
                    return "Zero";
                case Civ.Joker:
                    return "Joker";
            }
            return "";
        }
    }

    [System.Serializable]
    public class Civilization
    {
        public Civilization() { }

        public Civilization(Civ civ1)
        {
            civs = new List<Civ>() { civ1 };
        }

        public Civilization(Civ civ1, Civ civ2)
        {
            civs = new List<Civ>() { civ1, civ2 };
        }

        public Civilization(Civ civ1, Civ civ2, Civ civ3)
        {
            civs = new List<Civ>() { civ1, civ2, civ3 };
        }

        public Civilization(Civ civ1, Civ civ2, Civ civ3, Civ civ4)
        {
            civs = new List<Civ>() { civ1, civ2, civ3, civ4 };
        }

        public Civilization(Civ civ1, Civ civ2, Civ civ3, Civ civ4, Civ civ5)
        {
            civs = new List<Civ>() { civ1, civ2, civ3, civ4, civ5 };
        }

        public List<Civ> civs = new List<Civ>();

        public override string ToString()
        {
            string _result = "";
            foreach(Civ _civ in civs)
            {
                if(!string.IsNullOrEmpty(_result))
                {
                    _result += "/";
                }
                _result += _civ.GetName();
            }
            return _result;
        }
    }
}
