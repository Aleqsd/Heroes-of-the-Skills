using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes
{
    public abstract class Action : ScriptableObject
    {

        public abstract void Act(StateController controller);
    }
}
