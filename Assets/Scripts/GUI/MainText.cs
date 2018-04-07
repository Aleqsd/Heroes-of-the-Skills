using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Heroes
{
    public class MainText : NetworkBehaviour
    {
        [SyncVar]
        public GameObject text;

    }
}
