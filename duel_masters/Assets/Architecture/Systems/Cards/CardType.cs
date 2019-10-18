/*
 Author: Aaron Hines
 Description: Defines card types in the game
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Systems.Cards
{
    public enum CType
    {
        Creature,
        Spell,
        EvolutionCreature,
        PsychicCreature
    }

    public static class CardTypeExtention
    {
        public static string GetName(this CType civ)
        {
            switch (civ)
            {
                case CType.Creature:
                    return "Creature";
                case CType.Spell:
                    return "Spell";
                case CType.EvolutionCreature:
                    return "EvolutionCreature";
                case CType.PsychicCreature:
                    return "PsychicCreature";
            }
            return "";
        }
    }

    public class CardType
    {
        public List<CType> cardTypes = new List<CType>();

        public override string ToString()
        {
            string _result = "";
            foreach (CType _type in cardTypes)
            {
                if (!string.IsNullOrEmpty(_result))
                {
                    _result += "/";
                }
                _result += _type.GetName();
            }
            return _result;
        }

        public static bool operator ==(CardType obj1, CardType obj2)
        {
            foreach (CType _civ in obj1.cardTypes)
            {
                if (!obj2.cardTypes.Contains(_civ))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(CardType obj1, CardType obj2)
        {
            foreach (CType _civ in obj1.cardTypes)
            {
                if (!obj2.cardTypes.Contains(_civ))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool operator ==(CardType obj1, CType obj2)
        {
            return obj1.cardTypes.Contains(obj2);
        }

        public static bool operator !=(CardType obj1, CType obj2)
        {
            return !obj1.cardTypes.Contains(obj2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
