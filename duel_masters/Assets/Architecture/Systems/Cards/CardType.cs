/*
 Author: Aaron Hines
 Description: Defines card types in the game. Types determine how a card is played.
*/
using System;
using System.Collections.Generic;

namespace DuelMasters.Systems.Cards
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

    [Serializable]
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

        public bool Equal(CType rhs)
        {
            if (!cardTypes.Contains(rhs))
            {
                return false;
            }

            return true;
        }

        public bool Equal(CardType rhs)
        {
            foreach (CType _type in rhs.cardTypes)
            {
                if (!rhs.cardTypes.Contains(_type))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
