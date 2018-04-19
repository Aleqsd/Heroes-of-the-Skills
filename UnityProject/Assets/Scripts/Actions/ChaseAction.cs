using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes
{
    [CreateAssetMenu(menuName = "Heroes/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        private void Chase(StateController controller)
        {
            // TODO : When player dies, his GameObject is destroyed, target becomes null => exception
            controller.botMovement.navMeshAgent.destination = controller.target ? controller.target.position : controller.transform.position;
            controller.botMovement.navMeshAgent.isStopped = false;
        }
    }
}