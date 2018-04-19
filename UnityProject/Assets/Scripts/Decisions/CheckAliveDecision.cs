using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Heroes
{
    [CreateAssetMenu(menuName = "Heroes/Decisions/CheckAlive")]
    public class CheckAliveDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool targetIsActive = controller.target ? !controller.target.GetComponent<Health>().dead : false; // If target has been destroyed
            return targetIsActive;
        }

    }
}