using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{

    public class BulletBehaviour : MonoBehaviour
    {

        public float speed = 15f;
        public Transform target;
        public int damage = 1;
        public float radius = 0;


        void Start()
        {

        }


        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = target.position - this.transform.localPosition;
            float distThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distThisFrame)
            {
                DoBulletHit();
            }
            else
            {
                transform.Translate(dir.normalized * distThisFrame, Space.World);
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
            }
        }

        void DoBulletHit()
        {
            if (radius == 0)
            {
                target.GetComponent<EnemyBehaviour>().TakeDamage(damage);
            }
            else
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, radius);

                foreach (var c in cols)
                {
                    EnemyBehaviour e = c.GetComponent<EnemyBehaviour>();

                    if (e != null)
                    {
                        e.GetComponent<EnemyBehaviour>().TakeDamage(damage);
                    }
                }
            }

            Destroy(gameObject);
        }
    }
}
