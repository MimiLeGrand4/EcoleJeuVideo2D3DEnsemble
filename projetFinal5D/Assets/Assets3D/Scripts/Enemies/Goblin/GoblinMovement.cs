using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinMovement : MonoBehaviour
{
    // Variable Statistique Goblin
    public int health = 3;
    public int damage = 1;
    public float destroyEnemyTime = 5f;

    // Variable mouvement
    public float lookRadius = 10f;
    protected Transform target;
    private bool isRunning = false;
    public bool isAttacking = false;
    public bool isTakingHit = false;

    // Variable Component Goblin
    protected NavMeshAgent agent;
    private Animator animator;
    public AudioSource hurtSound;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //Fonction pour le mouvement de l'enemi
    private void Move(){
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius){
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance && !isTakingHit){
                isRunning = false;
                FaceTarget();
                isAttacking = true;
            }
            else {
                isAttacking = false;
                isRunning = true;
            }
        }
        else {
            isRunning = false;
        }

        animator.SetBool("EnemyAttack", isAttacking);
        animator.SetBool("EnemyRun", isRunning);
    }

    //Fonction pour que l'enemi soit toujours face à le joueur
    private void FaceTarget (){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //Fonction pour que l'enemi qui prend des dégâts
    public void TakeDamage(int damage){
        if(health > 1){
            animator.SetTrigger("EnemyTakeHit");
            hurtSound.Play();
            isTakingHit = true;
            health -= damage;
        }

        else{
            health -= damage;
            EnemyDie();
        }
    }

    //Fonction pour la mort de l'enemi
    public void EnemyDie(){
        animator.SetBool("EnemyDeath", true);

        // Désactiver les composants de mouvement et de collision
        agent.enabled = false;
        GetComponent<Collider>().enabled = false;

        // Désactiver ce script
        enabled = false;

        // Détruire le GameObject après 3 secondes
        StartCoroutine(DestroyAfterDelay(destroyEnemyTime));
    }

   // Détruit le GameObject après un certain délai
    private IEnumerator DestroyAfterDelay(float delay){
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // Dessine une sphère filaire rouge autour de l'objet dans l'éditeur de scène lorsque celui-ci est sélectionné
    void OnDrawGizmosSelected (){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
