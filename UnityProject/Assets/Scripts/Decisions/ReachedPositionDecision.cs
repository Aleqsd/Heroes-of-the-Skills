using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes
{
    /*
     * Check if GameObject is at less than 5 units from 0,0,0
     */ 
    [CreateAssetMenu(menuName = "Heroes/Decisions/ReachedPosition")]
    public class ReachedPositionDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool reachedPosition = ReachedPosition(controller);
            return reachedPosition;
        }

        private bool ReachedPosition(StateController controller)
        {
            bool reachedPosition = Vector3.Distance(new Vector3(21.1991f, 27.03f, -1.5f), controller.transform.position) < 5f;
            
            if (reachedPosition)
            {
                RaycastHit hit;
                Vector3 castOrigin = controller.eyes.position - controller.eyes.forward * controller.aiParameters.lookSphereCastRadius * 3; // 5 more or less, depend the castradius 

                // TODO : Change to overlapsphere (check all direction around)
                if (Physics.SphereCast(castOrigin, controller.aiParameters.lookSphereCastRadius * 3, controller.eyes.forward, out hit, controller.aiParameters.lookRange, LayerMask.GetMask("Nexus")))
                {
                    controller.target = hit.transform;
                    //Debug.Log("target : " + controller.target);
                }
            }
            return reachedPosition;
        }
    }
}
