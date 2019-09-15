/*
 Author: Aaron Hines
 Description: central location for constant strings used in code
*/

namespace DM.Systems
{
    class Constants
    {
        public const int MIN_DECK_COUNT = 40; //TODO: add this to some kind of game config
        public const int MAX_CARD_DUPLICATE = 4; //TODO: add this to some kind of game config
        public const int MIN_CARD_DUPLICATE = 0; //TODO: add this to some kind of game config

        public const string CREATE_NEW_CARD_DATA = "DM/New Card Data";
        public const string CREATE_NEW_CREATURE_RACE = "DM/New Creature Race";
        public const string CREATE_NEW_CARD_DATABASE = "DM/New Card Database";
        public const string CREATE_NEW_DECK = "DM/New Deck";
        public const string CREATE_NEW_TRUNK = "DM/New Trunk";

        public const string CREATE_CARDLOCATION = "Game Framework/ Create New Location";
    }
}
