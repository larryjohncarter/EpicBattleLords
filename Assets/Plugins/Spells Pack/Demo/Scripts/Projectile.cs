using System;
using UnityEngine;

namespace ZakhanSpellsPack
{
    public class Projectile : MonoBehaviour
    {
        public GameObject ExplosionPrefab;
        public float DestroyExplosion = 4.0f;
        private Rigidbody rb;

        public Vector2 velocity;

        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            rb.velocity = velocity;
        }

        public void Collision()
        {
            var exp = Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);
            Destroy(exp, DestroyExplosion);
        }
    }
}
