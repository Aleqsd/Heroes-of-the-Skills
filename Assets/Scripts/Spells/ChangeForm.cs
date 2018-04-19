using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes
{
    public class ChangeForm : MonoBehaviour
    {
        public GameObject lightAnimation;
        public GameObject darkAnimation;

        private GameObject instance;

        // Use this for initialization
        void Start()
        {
            // Destroy the spell after x seconds
            transform.position = Vector3.zero;
            transform.parent = FindObjectOfType<SorceressController>().transform;


            Destroy(gameObject, 3.0f);
            if (PlayerPrefs.GetInt("form") == 1)
            {
                PlayerPrefs.SetInt("form", 0);
                instance = Instantiate(lightAnimation, transform.position, lightAnimation.transform.rotation);
                instance.transform.parent = gameObject.transform;
                Destroy(instance, 3.0f);
            }
            else
            {
                PlayerPrefs.SetInt("form", 1);
                instance = Instantiate(darkAnimation, transform.position, darkAnimation.transform.rotation);
                instance.transform.parent = gameObject.transform;
                Destroy(instance, 3.0f);
            }
        }

        private void Update()
        {
            // transform.Translate(transform.parent.position);
        }


    }
}
