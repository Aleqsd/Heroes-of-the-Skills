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
            return Vector3.Distance(Vector3.zero, controller.transform.position) < 5f;
        }
    }
}
