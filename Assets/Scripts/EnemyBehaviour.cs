using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{

    public class EnemyBehaviour : MonoBehaviour
    {
        private GameObject pathGo;
        private Transform targetPathNode;
        private int pathNodeIndex = 0;
        private float speed = 5f;
        public int health = 1;
        public int moneyValue = 1;


        // Use this for initialization
        void Start()
        {
            pathGo = GameObject.Find("Path");
        }

        // Update is called once per frame
        void Update()
        {
            if (targetPathNode == null)
            {
                GetNextPathNode();
                if (targetPathNode == null)
                {
                    ReachedGoal();
                }
            }

            Vector3 dir = targetPathNode.position - this.transform.localPosition;

            float distanceThisFrame = speed * Time.deltaTime;
            if (dir.magnitude <= distanceThisFrame)
            {
                targetPathNode = null;
            }
            else
            {
                transform.Translate(dir.normalized * distanceThisFrame, Space.World);
                Quaternion targetRotation = Quaternion.LookRotation(dir);
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
            }
        }

        void GetNextPathNode()
        {
            targetPathNode = pathGo.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
        }

        void ReachedGoal()
        {
            Destroy(gameObject);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            GameObject.FindObjectOfType<ScoreManager>().money += moneyValue;
            Destroy(gameObject);
        }

    }

}