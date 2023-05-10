using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{
    public Animator animator;

    //Attack
    public Transform HitBoxGoblinAttack;
    public float  hitBoxRange = 0.5f;
    public LayerMask playerLayer;
    public float degat = 1;
    private bool goblinAttack = false;

    private bool directionDeLaHitbox = true;

    //Charactéristique
    public float vitesse = 2.0f;
    public float direction = 1.0f;
    public float tempsAttack = 1.0f;
    public float tempsDuMouvement = 1.0f;
    public float tempsDeLaPause = 4.0f;
    private bool pause = false;
    public bool goblinRun = false;
    private SpriteRenderer spriteGoblin;
    public int vie = 3;
    private int vieActuel;

     //Modifier Olivier - Audio
    public AudioSource EnemyHurtSource;
    public AudioClip EnemyHurtClip;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteGoblin = GetComponent<SpriteRenderer>();
        vieActuel = vie;
    }

    void Update()
    {
        if (goblinRun){
            goblinRunning();
        }

        else{
            goblinStaying();
        }
    }

    

    void goblinStaying(){
            tempsDeLaPause -= Time.deltaTime;
            spriteGoblin.flipX = true;

            //Change la direction de la hitbox de l'attaque du goblin
            if(directionDeLaHitbox){
                HitBoxGoblinAttack.localPosition = new Vector3(-HitBoxGoblinAttack.localPosition.x, 
                HitBoxGoblinAttack.localPosition.y, HitBoxGoblinAttack.localPosition.z);

                directionDeLaHitbox = false;
            }

            
            if (tempsDeLaPause <= 4.0f && tempsDeLaPause >= 3.7f)
            {
                animator.SetBool("GoblinAttack1", true);

                //Vérifie si le Goblin a déjà attaqué
                if(!goblinAttack){
                    Attack();
                    goblinAttack = true;
                }
                
            }

            else {
                animator.SetBool("GoblinAttack1", false);
            }

             if (tempsDeLaPause <= 0.0f)
            {
                tempsDeLaPause = 4.0f;
                goblinAttack = false;
            }


    }

    void goblinRunning(){
        if (!pause)
        {
            animator.SetFloat("GoblinRun", Mathf.Abs(transform.position.x));
            animator.SetBool("GoblinAttack1", false);
            tempsDuMouvement -= Time.deltaTime;
            if (tempsDuMouvement <= 0.0f)
            {
                direction *= -1.0f;
                pause = true;
                tempsDuMouvement = 1.0f;
            }
            transform.position = new Vector2(transform.position.x + (vitesse * direction * Time.deltaTime), transform.position.y);
        }
        
        else
        {
            tempsDeLaPause -= Time.deltaTime;

            if (tempsDeLaPause <= 4.0f && tempsDeLaPause >= 3.4f)
            {
                animator.SetBool("GoblinAttack1", true);

                //Vérifie si le Goblin a déjà attaqué
                if(!goblinAttack){
                    Attack();
                    goblinAttack = true;
                }
            }


            else {
                animator.SetBool("GoblinAttack1", false);
            }

            animator.SetFloat("GoblinRun", 0.0f);

            if (tempsDeLaPause <= 0.0f)
            {
                pause = false;
                tempsDeLaPause = 4.0f;
                spriteGoblin.flipX = (direction < 0.0f);
                goblinHitBoxDirectionEnMouvement();
                goblinAttack = false;
            }
        }
    }

    void goblinHitBoxDirectionEnMouvement(){
        if(directionDeLaHitbox){
            HitBoxGoblinAttack.localPosition = new Vector3(HitBoxGoblinAttack.localPosition.x * -1, 
                HitBoxGoblinAttack.localPosition.y, HitBoxGoblinAttack.localPosition.z);
            directionDeLaHitbox = false;
        }

        else{
            HitBoxGoblinAttack.localPosition = new Vector3(-HitBoxGoblinAttack.localPosition.x, 
                HitBoxGoblinAttack.localPosition.y, HitBoxGoblinAttack.localPosition.z);
            directionDeLaHitbox = true;
        }
    }

    void Attack(){
        // Détecter un joueur
        Collider2D[] attackPlayer = Physics2D.OverlapCircleAll(HitBoxGoblinAttack.position, hitBoxRange, playerLayer);

        //Faire des dégats
        foreach(Collider2D player in attackPlayer)
            {
                //Test sur la console
                Debug.Log("Ça fait mal " + player.name);

                //Dégat infligé à le joueur
                player.GetComponent<HeroKnight>().playerTakeHit(degat);

            }
    }

    //Modifier Olivier
    void OnDrawGizmosSelected() {
        if (HitBoxGoblinAttack == null)
        return;

        Gizmos.DrawWireSphere(HitBoxGoblinAttack.position, hitBoxRange);
    }

    public void goblinTakeHit(int damage){
        EnemyHurtSource.PlayOneShot(EnemyHurtClip);
        vieActuel -= damage;

        //Animation Goblin qui prend des dégats
        animator.SetTrigger("GoblinTakeHit");

        if(vieActuel <= 0){
            goblinDie();
        }
    }

    void goblinDie(){

        //Test sur la console
        Debug.Log("Le goblin est mort RIP");

        // Animation Goblin qui meurt
        animator.SetBool("GoblinDeath", true);

        //Rendre le Goblin incapable de faire quelque chose parcequ'il est mort
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
