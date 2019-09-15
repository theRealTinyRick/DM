using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using GameFramework.Actors.Components;
using GameFramework.Stats;
using GameFramework.State;

namespace GameFramework.Gameplay.Components.HumanoidMovement
{
    public enum RotateMode : int 
    {
        None = 0,
        RotateInMoveDirection = 1,
        RotateWithInputDirection = 2,
        RotateWithOrientation = 3
    }


    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(StateComponent))]
    [RequireComponent(typeof(StatComponent))]
    [RequireComponent(typeof(PlayerInputComponent))]
    public class HumanoidMoveComponent : ActorComponent
    {
        [TabGroup(Tabs.PROPERTIES)]
        [SerializeField]
        public RotateMode rotateMode;

        [TabGroup(Tabs.STATS)]
        [SerializeField]
        private Stat moveSpeedStat;

        [TabGroup(Tabs.STATS)]
        [SerializeField]
        private Stat rotateSpeedStat;

        [TabGroup(Tabs.STATES)]
        [SerializeField]
        private List<StateType> nonMovableStates = new List<StateType>();

        // member vars that won't be seen by desiners
        private Vector3 currentInputDirection;

        private float moveSpeed
        {
            get
            {
                float _speed = 0;
                if(moveSpeedStat != null)
                {
                    _speed = statComponent.GetStat(moveSpeedStat);
                }
                else
                {
                    Debug.LogError("No Move Speed Stat Assigned", gameObject);
                }

                return _speed;
            }
        }

        private float rotateSpeed
        {
            get
            {
                float _speed = 0;
                if (rotateSpeedStat != null)
                {
                    _speed = statComponent.GetStat(rotateSpeedStat);
                }
                else
                {
                    Debug.LogError("No Rotate Speed Stat Assigned", gameObject);
                }

                return _speed;
            }
        }


        // components
        private Rigidbody playerRigidBody;

        private StatComponent _statComponent;
        private StatComponent statComponent
        {
            get
            {
                if (_statComponent == null)
                {
                    _statComponent = owner.GetActorComponent<StatComponent>();
                }
                return _statComponent;
            }
        }

        private StateComponent _stateComponent;
        private StateComponent stateComponent
        {
            get
            {
                if(_stateComponent == null)
                {
                    _stateComponent = owner.GetActorComponent<StateComponent>();
                }

                return _stateComponent;
            }
        }

        private PlayerInputComponent _playerInputComponent;
        private PlayerInputComponent playerInputComponent
        {
            get
            {
                if(_playerInputComponent == null)
                {
                    _playerInputComponent = owner.GetActorComponent<PlayerInputComponent>();
                }

                return _playerInputComponent;
            }
        }

        public override void InitializeComponent()
        {
            if(playerRigidBody == null)
            {
                playerRigidBody = owner.GetComponentInChildren<Rigidbody>();
            }
        }
         
        // satisfying abstract class
        public override void DisableComponent() { }

        public void FixedUpdate()
        {
            if(currentInputDirection != Vector3.zero)
            {
                Vector3 _targetVelocity = new Vector3(currentInputDirection.x * moveSpeed, playerRigidBody.velocity.y, currentInputDirection.z * moveSpeed);
                playerRigidBody.velocity = _targetVelocity;
            }
        }

        private float currentRotateTime = 0;
        private Quaternion targetRotation = new Quaternion(); 

        private void LateUpdate()
        {
            if (rotateMode == RotateMode.RotateInMoveDirection && currentInputDirection != Vector3.zero)
            {
                Vector3 _targetVelocity = new Vector3(playerRigidBody.velocity.x, 0, playerRigidBody.velocity.z);
                if (_targetVelocity != Vector3.zero)
                {
                    Quaternion _rotation = Quaternion.LookRotation(_targetVelocity); 

                    if(_rotation != targetRotation)
                    {
                        currentRotateTime = 0;
                    }

                    targetRotation = _rotation;
                    currentRotateTime  += rotateSpeed * Time.deltaTime;
                    if(currentRotateTime > 1)
                    {
                        currentRotateTime = 1;
                    }

                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, currentRotateTime);
                }
            }

            if (rotateMode == RotateMode.RotateWithInputDirection && currentInputDirection != Vector3.zero)
            {
                Quaternion _rotation = Quaternion.LookRotation(currentInputDirection);
                if (_rotation != targetRotation)
                {
                    currentRotateTime = 0;
                }

                targetRotation = _rotation;
                currentRotateTime += rotateSpeed * Time.deltaTime;
                if (currentRotateTime > 1)
                {
                    currentRotateTime = 1;
                }

                transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, currentRotateTime);
            }

            if (rotateMode == RotateMode.RotateWithOrientation)
            {
                if (playerInputComponent.locomotionOrientation != null)
                {
                    Quaternion _rotation = playerInputComponent.locomotionOrientation.rotation;
                    if (_rotation != targetRotation)
                    {
                        currentRotateTime = 0;
                    }

                    targetRotation = _rotation;
                    currentRotateTime += rotateSpeed * Time.deltaTime;
                    if (currentRotateTime > 1)
                    {
                        currentRotateTime = 1;
                    }

                    transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, currentRotateTime);
                }
            }
        }

        public void RecieveInputDirection(Vector3 direction /*oriented*/)
        {
            currentInputDirection = direction;
        }

        public void SetRotationNone()
        {
            rotateMode = RotateMode.None;
        }

        public void SetRotationToMoveDirection()
        {
            rotateMode = RotateMode.RotateInMoveDirection;
        }

        public void SetRotationToInputDirection()
        {
            rotateMode = RotateMode.RotateWithInputDirection;
        }

        public void SetRotationToOrientationDirection()
        {
            rotateMode = RotateMode.RotateWithOrientation;
        }

        public void DisableGravity()
        {
            playerRigidBody.useGravity = false;
        }

        public void EnableGravity()
        {
            playerRigidBody.useGravity = true;
        }

        public void DisablePhysics()
        {
            playerRigidBody.isKinematic = true;
        }


        public void EnablePhysics()
        {
            playerRigidBody.isKinematic = false;
        }

        public void DisableMovement()
        {
            // TODO: diable movement
        }

        public void EnableMovement()
        {
            // TODO: disable movement
        }
    }
}
