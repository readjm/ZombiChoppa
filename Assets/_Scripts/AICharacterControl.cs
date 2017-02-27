using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public AudioClip[] zombieSounds;
        public float attackDamage;
        public float attackSpeed;
        public float health;

        public Transform target;                                    // target to aim for

        private AudioSource audioSource;
        private float timeSinceLastAttack = 0;
        private bool dead; 

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;

            audioSource = GetComponentInChildren<AudioSource>();
            audioSource.clip = zombieSounds[0];
            audioSource.Play();
        }


        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    character.Move(agent.desiredVelocity, false, false);
                }
            }
            else
            {
                agent.SetDestination(transform.position);
                character.Move(Vector3.zero, false, false);
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        private void AttackPlayer()
        {
            GameObject.Find("Player").GetComponent<Player>().TakeDamage(attackDamage);
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Clamp(health - damage, 0, health);

            if (health <= 0 && !dead)
            {
                GetComponent<Animator>().SetBool("IsDead", true);
                GetComponent<AICharacterControl>().target = null;
                GetComponent<CapsuleCollider>().enabled = false;
                audioSource.Stop();
                dead = true;
            }
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {
                GetComponent<Animator>().SetBool("IsAttacking", true);
                audioSource.clip = zombieSounds[1];
                audioSource.Play();
                agent.enabled = false;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.name == "Player" && timeSinceLastAttack >= attackSpeed)
            {
                Invoke("AttackPlayer", .5f);
                timeSinceLastAttack = 0;
            }
            timeSinceLastAttack += Time.deltaTime;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.name == "Player")
            {
                GetComponent<Animator>().SetBool("IsAttacking", false);
                audioSource.clip = zombieSounds[0];
                audioSource.Play();
                agent.enabled = true;
            }
        }
    }
}
