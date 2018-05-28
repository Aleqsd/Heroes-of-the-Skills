using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes
{

    [CreateAssetMenu(menuName = "Heroes/Actions/ReachPosition")]
    public class ReachPositionAction : Action
    {

        public override void Act(StateController controller)
        {
            ReachPosition(controller);
        }

        private void ReachPosition(StateController controller)
        {
            controller.botMovement.Move(new Vector3(21.1991f, 27.03f, -1.5f));
        }
    }
}