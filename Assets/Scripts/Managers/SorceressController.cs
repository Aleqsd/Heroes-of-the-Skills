using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Heroes
{
    public class SorceressController : PlayerController
    {
        public GameObject[] darkSpells;
        public float[] darkSpellCooldowns;
        private float[] darkNextSpell;


        new void Start()
        {
            base.Start();
            darkNextSpell = new float[darkSpellCooldowns.Length];
        }


        protected override void SpellInput()
        {
            

            if (PlayerPrefs.GetInt("form") == 0)
            {
                base.SpellInput();
            }
            else
            {
                //Debug.Log("form : " + PlayerPrefs.GetInt("form"));
                if (Input.GetKeyDown(KeyCode.Mouse0) && darkSpells[0] != null && Time.time > darkNextSpell[0])
                {
                    darkNextSpell[0] = Time.time + darkSpellCooldowns[0];
                    CmdSpell(0);
                }

                if (Input.GetKeyDown(KeyCode.Alpha1) && darkSpells[1] != null && Time.time > darkNextSpell[1])
                {
                    darkNextSpell[1] = Time.time + darkSpellCooldowns[1];
                    CmdSpell(1);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) && darkSpells[2] != null && Time.time > darkNextSpell[2])
                {
                    darkNextSpell[2] = Time.time + darkSpellCooldowns[2];
                    CmdSpell(2);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) && darkSpells[3] != null && Time.time > darkNextSpell[3])
                {
                    darkNextSpell[3] = Time.time + darkSpellCooldowns[3];
                    CmdSpell(3);
                }

                if (Input.GetKeyDown(KeyCode.Alpha4) && darkSpells[4] != null && Time.time > darkNextSpell[4])
                {
                    darkNextSpell[4] = Time.time + darkSpellCooldowns[4];
                    CmdSpell(4);
                }

                if (Input.GetKeyDown(KeyCode.Alpha5) && darkSpells[5] != null && Time.time > darkNextSpell[5])
                {
                    darkNextSpell[5] = Time.time + darkSpellCooldowns[5];
                    CmdSpell(5);
                }

                if (Input.GetKeyDown(KeyCode.Alpha6) && darkSpells[6] != null && Time.time > darkNextSpell[6])
                {
                    darkNextSpell[6] = Time.time + darkSpellCooldowns[6];
                    CmdSpell(6);
                }
            }
        }
    }
}
