using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public Animator animator;

    //Attack
    public Transform HitBoxSkeletonAttack;
    public float  hitBoxRange = 0.5f;
    public LayerMask playerLayer;
    public float degat = 2;
    public float colliderDegat = 2;
    private bool skeletonAttack = false;

    private bool directionDeLaHitbox = true;

    //Charactéristique
    public float vitesse = 2.0f;
    public float direction = 1.0f;
    public float tempsAttack = 1.0f;
    public float tempsDuMouvement = 1.0f;
    public float tempsDeLaPause = 4.0f;
    private bool pause = false;
    private SpriteRenderer spriteSkeleton;
    public int vie = 5;
    private int vieActuel;

     //Modifier Olivier - Audio
    public AudioSource EnemyHurtSource;
    public AudioClip EnemyHurtClip;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteSkeleton = GetComponent<SpriteRenderer>();
        vieActuel = vie;
    }

    void Update()
    {
        skeletonWalking();
        //bodyAttack();
    }

    void skeletonWalking(){
        if (!pause)
        {
            animator.SetFloat("SkeletonWalk", Mathf.Abs(transform.position.x));
            animator.SetBool("SkeletonAttack1", false);
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

            if (tempsDeLaPause <= 3.5f && tempsDeLaPause >= 3.4f)
            {
                animator.SetBool("SkeletonAttack1", true);

                //Vérifie si le skeleton a déjà attaqué
                if(!skeletonAttack){
                    Attack();
                    skeletonAttack = true;
                }
            }


            else {
                animator.SetBool("SkeletonAttack1", false);
            }

            animator.SetFloat("SkeletonWalk", 0.0f);

            if (tempsDeLaPause <= 0.0f)
            {
                pause = false;
                tempsDeLaPause = 4.0f;
                spriteSkeleton.flipX = (direction < 0.0f);
                skeletonHitBoxDirectionEnMouvement();
                skeletonAttack = false;
            }
        }
    }

    void skeletonHitBoxDirectionEnMouvement(){
        if(directionDeLaHitbox){
            HitBoxSkeletonAttack.localPosition = new Vector3(HitBoxSkeletonAttack.localPosition.x * -1, 
            HitBoxSkeletonAttack.localPosition.y, HitBoxSkeletonAttack.localPosition.z);
            directionDeLaHitbox = false;
        }

        else{
            HitBoxSkeletonAttack.localPosition = new Vector3(-HitBoxSkeletonAttack.localPosition.x, 
            HitBoxSkeletonAttack.localPosition.y, HitBoxSkeletonAttack.localPosition.z);
            directionDeLaHitbox = true;
        }
    }

    void Attack(){
        // Détecter un joueur
        Collider2D[] attackPlayer = Physics2D.OverlapCircleAll(HitBoxSkeletonAttack.position, hitBoxRange, playerLayer);

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
        if (HitBoxSkeletonAttack == null)
        return;

        Gizmos.DrawWireSphere(HitBoxSkeletonAttack.position, hitBoxRange);
    }

    public void skeletonTakeHit(int damage) {
        EnemyHurtSource.PlayOneShot(EnemyHurtClip);
        vieActuel -= damage;

        //Animation skeleton qui prend des dégats
        animator.SetTrigger("SkeletonTakeHit");

        if(vieActuel <= 0) {
            skeletonDie();
        }
    }

    void skeletonDie()
    {
        //Test sur la console
        Debug.Log("Le skeleton est mort RIP");

        // Animation skeleton qui meurt
        animator.SetBool("SkeletonDeath", true);

        //Rendre le skeleton incapable de faire quelque chose parcequ'il est mort
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    //Si le Joueur touche le monstre, il prend des dégats
}
