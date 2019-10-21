/*
 Author: Aaron Hines
 Description: Defines civilations in the game
*/
using System.Collections.Generic;

namespace DuelMasters.Systems.Cards
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
        public Civilization()
        {
            civs = new List<Civ>();
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

        public bool Equal(Civ rhs)
        {
            if (!civs.Contains(rhs))
            {
                return false;
            }

            return true;
        }

        public bool Equal(Civilization rhs)
        {
            foreach (Civ _civ in rhs.civs)
            {
                if (!rhs.civs.Contains(_civ))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
