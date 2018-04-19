using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Heroes
{
    public class StateController : MonoBehaviour
    {

        public State currentState;
        public AiParameters aiParameters;
        public Transform eyes;
        public State remainState;


        
        [HideInInspector] public BotAttack botAttack;
        [HideInInspector] public Health health;
        [HideInInspector] public BotMovement botMovement;
        [HideInInspector] public Transform target;
        [HideInInspector] public float stateTimeElapsed;

        private bool aiActive;


        void Start()
        {
            botAttack = GetComponent<BotAttack>();
            health = GetComponent<Health>();
            botMovement = GetComponent<BotMovement>();
            botMovement.navMeshAgent.speed = aiParameters.moveSpeed;

            // If "Failed to create agent because it is not close enough to the NavMesh" appears
            // that's because the object linked with nma is too far from the floor for example
        }

        public bool SetupAI(bool aiActivationFromGameManager)
        {
            aiActive = aiActivationFromGameManager;
            return aiActive;
        }

        void Update()
        {
            if (!aiActive)
                return;
            currentState.UpdateState(this);
        }

        void OnDrawGizmos()
        {
            if (currentState != null && eyes != null)
            {
                Gizmos.color = currentState.sceneGizmoColor;
                Gizmos.DrawWireSphere(eyes.position, aiParameters.lookSphereCastRadius);
            }
        }

        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                currentState = nextState;
                OnExitState();
            }
        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return (stateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }

    }
}