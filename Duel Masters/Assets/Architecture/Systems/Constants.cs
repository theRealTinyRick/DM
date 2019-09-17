/*
 Author: Aaron Hines
 Description: central location for constant strings used in code
*/

namespace DM.Systems
{
    class Constants
    {
        public const int STARTING_SHIELD_COUNT = 5; //TODO: add this to some kind of game config
        public const int STARTING_HAND_COUNT = 5; //TODO: add this to some kind of game config
        public const int MAX_HAND_COUNT = 7; //TODO: add this to some kind of game config

        public const int MIN_DECK_COUNT = 40; //TODO: add this to some kind of game config
        public const int MAX_CARD_DUPLICATE = 4; //TODO: add this to some kind of game config
        public const int MIN_CARD_DUPLICATE = 0; //TODO: add this to some kind of game config

        public const string CREATE_NEW_CARD_DATA = "DM/Create New Card Data";
        public const string CREATE_NEW_CREATURE_RACE = "DM/Create New Creature Race";
        public const string CREATE_NEW_CARD_DATABASE = "DM/Create New Card Database";
        public const string CREATE_NEW_DECK = "DM/Create New Deck";
        public const string CREATE_NEW_TRUNK = "DM/Create New Trunk";

        public const string CREATE_CARDLOCATION = "Game Framework/Create New Location";
    }
}
