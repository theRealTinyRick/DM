using UnityEngine;
using Sirenix.OdinInspector;
using DM.Systems.Players;
using DM.Systems.Cards;

namespace Systems.Duel
{
    public class DuelController : SerializedMonoBehaviour
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Deck player1Deck;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Deck player2Deck;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Player player1;

        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public Player player2;

        [Button]
        public void Start()
        {
            player1 = new Player( player1Deck );
            player2 = new Player( player2Deck );
        }

        [Button]
        public void Clear()
        {
            player1 = null;
            player2 = null;
        }
    }
}
