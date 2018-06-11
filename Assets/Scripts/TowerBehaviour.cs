using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{

    public class TowerBehaviour : MonoBehaviour
    {
        Transform turretTransform;
        public GameObject bulletPrefab;
        public int cost;
        private float fireCooldown = 0.5f;
        private float fireCooldownLeft = 0;
        public int damage = 1;
        public float radius = 0;
        public float range = 10f;

        // Use this for initialization
        void Start()
        {
            turretTransform = transform.Find("Turret");
        }

        // Update is called once per frame
        void Update()
        {

            EnemyBehaviour[] enemies = GameObject.FindObjectsOfType<EnemyBehaviour>();
            EnemyBehaviour nearestEnemy = null;
            float dist = Mathf.Infinity;

            foreach (var e in enemies)
            {
                float d = Vector3.Distance(this.transform.position, e.transform.position);
                if (nearestEnemy == null || d < dist)
                {
                    nearestEnemy = e;
                    dist = d;
                }
            }

            if (nearestEnemy == null)
            {
                return;
            }

            Vector3 dir = nearestEnemy.transform.position - this.transform.position;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            turretTransform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);

            fireCooldownLeft -= Time.deltaTime;
            if (fireCooldownLeft <= 0 && dir.magnitude <= range)
            {
                fireCooldownLeft = fireCooldown;
                ShootAt(nearestEnemy);
            }
        }
        void ShootAt(EnemyBehaviour e)
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

            BulletBehaviour b = bulletGO.GetComponent<BulletBehaviour>();
            b.target = e.transform;
            b.damage = damage;
            b.radius = radius;
        }
    }

}