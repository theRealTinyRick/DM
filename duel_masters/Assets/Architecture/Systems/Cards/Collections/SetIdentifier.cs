using UnityEngine;
using Sirenix.OdinInspector;

namespace DM.Systems.Cards
{
    [CreateAssetMenu( fileName = "New Set", menuName = Constants.CREATE_NEW_SET, order = 1 )]
    public class SetIdentifier : SerializedScriptableObject
    {
        [SerializeField]
        public string setName;
    }
}
