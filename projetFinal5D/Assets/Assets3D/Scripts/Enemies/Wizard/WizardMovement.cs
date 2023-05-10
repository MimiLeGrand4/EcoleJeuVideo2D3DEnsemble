using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardMovement : MonoBehaviour
{

    // Variable Statistique Wizard
    public int health = 5;
    public int damage = 2;
    public float destroyEnemyTime = 5f;

    // Variable Projectile
    [SerializeField] private float projectileTimer = 5;
    private float fireBallTime;
    public GameObject enemyFireBall;
    public Transform spawnPoint;
    public float wizardSpeed;

    //private bool isAttacking = false;

    // Variable mouvement
    public float lookRadius = 10f;
    protected Transform target;

    // Variable Component
    protected UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    public AudioSource hurtSound;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
            animator.SetBool("EnemyAttack", true);
            Attack();

            if (distance <= agent.stoppingDistance){
                FaceTarget();
            }
        }

        else{
            animator.SetBool("EnemyAttack", false);
        }
    }

    public void Attack(){
        fireBallTime -= Time.deltaTime;

        if (fireBallTime > 0) return;

        fireBallTime = projectileTimer;

        GameObject fireBallObject = Instantiate(enemyFireBall, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody fireBallRb = fireBallObject.GetComponent<Rigidbody>();
        fireBallRb.AddForce(fireBallRb.transform.forward * wizardSpeed);
        Destroy(fireBallObject, 5f);
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
