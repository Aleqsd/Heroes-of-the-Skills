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


        override protected void Start()
        {
            base.Start();
            darkNextSpell = new float[darkSpellCooldowns.Length];
            Debug.Log("form : " + PlayerPrefs.GetInt("form"));
        }


        protected override void SpellInput()
        {
            

            if (PlayerPrefs.GetInt("form") == 0)
            {
                base.SpellInput();
            }
            else
            {
                
                if (Input.GetKeyDown(KeyCode.Mouse0) && darkSpells.Length > 0 && Time.time > darkNextSpell[0])
                {
                    darkNextSpell[0] = Time.time + darkSpellCooldowns[0];
                    CmdSpell(darkSpells[0]);
                }

                if (Input.GetKeyDown(KeyCode.Alpha1) && darkSpells.Length > 1 && Time.time > darkNextSpell[1])
                {
                    darkNextSpell[1] = Time.time + darkSpellCooldowns[1];
                    CmdSpell(darkSpells[1]);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2) && darkSpells.Length > 2 && Time.time > darkNextSpell[2])
                {
                    darkNextSpell[2] = Time.time + darkSpellCooldowns[2];
                    CmdSpell(darkSpells[2]);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3) && darkSpells.Length > 3 && Time.time > darkNextSpell[3])
                {
                    darkNextSpell[3] = Time.time + darkSpellCooldowns[3];
                    CmdSpell(darkSpells[3]);
                }

                if (Input.GetKeyDown(KeyCode.Alpha4) && darkSpells.Length > 4 && Time.time > darkNextSpell[4])
                {
                    darkNextSpell[4] = Time.time + darkSpellCooldowns[4];
                    CmdSpell(darkSpells[4]);
                }

                if (Input.GetKeyDown(KeyCode.Alpha5) && darkSpells.Length > 5 && Time.time > darkNextSpell[5])
                {
                    darkNextSpell[5] = Time.time + darkSpellCooldowns[5];
                    CmdSpell(darkSpells[5]);
                }

                if (Input.GetKeyDown(KeyCode.Alpha6) && darkSpells.Length > 6 && Time.time > darkNextSpell[6])
                {
                    darkNextSpell[6] = Time.time + darkSpellCooldowns[6];
                    CmdSpell(darkSpells[6]);
                }
            }
        }
    }
}
