using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes
{
    [CreateAssetMenu(menuName = "Heroes/Actions/Attack")]
    public class AttackAction : Action
    {
        
        public override void Act(StateController controller)
        {
            Attack(controller);
        }

        private void Attack(StateController controller)
        {
            RaycastHit hit;

            Vector3 castOrigin = controller.eyes.position - controller.eyes.forward * controller.aiParameters.lookSphereCastRadius; // the origin of the spherecast need to start behind
                                                                                                                                        // the gameobject
            //Debug.Log("Origin : " + castOrigin.z);
            Debug.DrawRay(castOrigin, controller.eyes.forward.normalized * controller.aiParameters.attackRange, Color.red);

            if (Physics.SphereCast(castOrigin, controller.aiParameters.lookSphereCastRadius, controller.eyes.forward, out hit, controller.aiParameters.attackRange, LayerMask.GetMask("Player","Nexus")))
            {
                //StateController target = hit.rigidbody.GetComponent<StateController>();

                //Debug.Log("Hit : "+hit);
                controller.botAttack.Attack(controller.aiParameters.attackRate, hit);
            }
            else
            {
                //Debug.Log("Not hit");
            }
        }
    }
}
