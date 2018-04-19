using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Heroes
{
    public class BotMovement : MonoBehaviour
    {
        public float speed;                 // How fast the bot moves forward and back.
        
        [HideInInspector] public NavMeshAgent navMeshAgent;
        private new Rigidbody rigidbody;              // Reference used to move the bot.
        private Vector3 direction;
        public AudioSource movementAudio;         // Reference to the audio source used to play sounds.
        // TODO : array clip
        public AudioClip idling;            // Audio to play when the bot isn't moving.
        public AudioClip walking;           // Audio to play when the bot is moving.
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.speed = speed;
        }


        private void Audio()
        {
            movementAudio.clip = walking;
            movementAudio.Play(); // Different audio per movement behaviour ?
        }

        private Vector3 RandomNavSphere(Vector3 origin,int distance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * Random.Range(40,distance);
            randomDirection.y = 0;
            randomDirection += origin;
            return randomDirection;
        }


        public void SetupAI(bool aiActivationFromParent)
        {
            if (aiActivationFromParent)
            {
                navMeshAgent.enabled = true;
            }
            else
            {
                navMeshAgent.enabled = false;
            }
        }

        private void OnEnable()
        {
            // When the bot is turned on, make sure it's not kinematic.
            rigidbody.isKinematic = false;
            
        }




        public void Move(Vector3 destination)
        {
            Audio();
            if (navMeshAgent.enabled)
                navMeshAgent.destination = destination;
            anim.SetBool("isMove", true);
            
        }


        public void MoveRandom(int distance)
        {
            //Debug.Log("MySpeed : " + speed);
            if (navMeshAgent.enabled)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
                {

                    direction = RandomNavSphere(this.transform.position, distance);
                    navMeshAgent.destination = direction;
                }
            }
        }
    }
}