using UnityEngine;

namespace GameFramework.Utilities
{
    enum UpdateTime
    {
        Normal,
        Fixed,
        Late
    }

    public class MatchRotationComponent : MonoBehaviour
    {
        [SerializeField]
        public Transform rotationToMatch;

        [SerializeField]
        public Transform transformToRotate;

        [SerializeField]
        private UpdateTime updateTime;

        void Update()
        {
            if(updateTime == UpdateTime.Normal && rotationToMatch != null && transformToRotate != null) 
            {
                transformToRotate.rotation = rotationToMatch.rotation;
            }
        }

        private void FixedUpdate()
        {
            if (updateTime == UpdateTime.Fixed && rotationToMatch != null && transformToRotate != null)
            {
                transformToRotate.rotation = rotationToMatch.rotation;
            }
        }

        private void LateUpdate()
        {
            if (updateTime == UpdateTime.Late && rotationToMatch != null && transformToRotate != null)
            {
                transformToRotate.rotation = rotationToMatch.rotation;
            }
        }
    }
}

