using UnityEngine;
using UnityEngine.UI;

namespace Heroes
{
    public class BotAttack : MonoBehaviour
    {
        public int damage = 10;
        private float nextAttackTime; // kind of attack speed thing, useful
        public AudioSource attackAudio;               // The audio source to play.
        public AudioClip attack;            // Audio to play when the bot attack.
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void Attack(float attackRate, RaycastHit hit) // attackSpeed ?
        {
            //Debug.Log("hit : " + hit.rigidbody);
            Health target = hit.transform.GetComponent<Health>();
            //Debug.Log("BotAttack : " + Time.time + " | " + nextAttackTime + " | " + target);
            // Find the Health script associated with the rigidbody.
            if (target)
            {
                
                if (Time.time > nextAttackTime && target.isActiveAndEnabled)
                {
                    nextAttackTime = Time.time + attackRate;

                    Audio();
                    anim.SetTrigger("isAttack");

                    if (target.getHealth() <= 0)
                        return;
                    
                    target.TakeDamage(damage);
                    
                }
            }

        }

        private void Audio()
        {
            if (attackAudio)
            {
                attackAudio.clip = attack;
                attackAudio.Play();
            }
        }
    }
}