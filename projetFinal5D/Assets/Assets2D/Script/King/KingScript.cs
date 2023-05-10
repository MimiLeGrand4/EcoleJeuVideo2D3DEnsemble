using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KingScript : MonoBehaviour
{
    public Animator animator;

    //Attack
    public Transform HitBoxKing;
    public float  hitBoxRange = 0.5f;
    public LayerMask playerLayer;
    public float degat = 3;
    private bool kingAttack = false;
    private bool directionDeLaHitbox = true;

    //Charactéristique
    public float vitesse = 10.0f;
    public float direction = 1.0f;
    public float tempsAttack = 1.0f;
    public float tempsDuMouvement = 1.0f;
    public float tempsDeLaPause = 6.0f;
    private float tempsDuMouvementActuel;
    private float tempsDeLaPauseActuel;
    private bool pause = false;
    private SpriteRenderer spriteKing;
    public int vie = 10;
    private int vieActuel;

    //Modifier Olivier - Audio
    public AudioSource EnemyHurtSource;
    public AudioClip EnemyHurtClip;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteKing = GetComponent<SpriteRenderer>();
        tempsDuMouvementActuel = tempsDuMouvement;
        tempsDeLaPauseActuel = tempsDeLaPause;
        vieActuel = vie;
    }

    void Update()
    {
        kingRunning();
    }

    void kingRunning(){
        if (!pause)
        {
            animator.SetFloat("KingRun", Mathf.Abs(transform.position.x));
            //animator.SetBool("KingAttack1", false);
            tempsDuMouvement -= Time.deltaTime;
            if (tempsDuMouvement <= 0.0f)
            {
                direction *= -1.0f;
                pause = true;
                tempsDuMouvement =tempsDuMouvementActuel;
            }
            transform.position = new Vector2(transform.position.x + (vitesse * direction * Time.deltaTime), transform.position.y);
        }
        
        else
        {
            tempsDeLaPause -= Time.deltaTime;

            if(tempsDeLaPause <= 5.0f && tempsDeLaPause >= 4.7f){
                    animator.SetBool("KingAttack1", true);
                    //Vérifie si il y a déjà eu une attaque
                    if(!kingAttack){
                        Attack();
                        kingAttack = true;
                    }
            }

            else{
                animator.SetBool("KingAttack1", false);
            }

            
            animator.SetFloat("KingRun", 0.0f);

            if (tempsDeLaPause <= 0.0f)
            {
                pause = false;
                tempsDeLaPause = tempsDeLaPauseActuel;
                spriteKing.flipX = (direction < 0.0f);
                HitBoxDirectionEnMouvement();
                kingAttack = false;
            }
        }
    }

    void HitBoxDirectionEnMouvement(){
        if(directionDeLaHitbox){
            HitBoxKing.localPosition = new Vector3(HitBoxKing.localPosition.x * -1, 
                HitBoxKing.localPosition.y, HitBoxKing.localPosition.z);
            directionDeLaHitbox = false;
        }

        else{
            HitBoxKing.localPosition = new Vector3(-HitBoxKing.localPosition.x, 
                HitBoxKing.localPosition.y, HitBoxKing.localPosition.z);
            directionDeLaHitbox = true;
        }
    }

    void Attack(){
        // Détecter un joueur
        Collider2D[] attackPlayer = Physics2D.OverlapCircleAll(HitBoxKing.position, hitBoxRange, playerLayer);

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
        if (HitBoxKing == null)
        return;

        Gizmos.DrawWireSphere(HitBoxKing.position, hitBoxRange);
    }

    public void kingTakeHit(int damage){
        EnemyHurtSource.PlayOneShot(EnemyHurtClip);
        vieActuel -= damage;

        //Animation King qui prend des dégats
        animator.SetTrigger("KingTakeHit");

        if(vieActuel <= 0){
            kingDie();
        }
    }

    void kingDie(){

        //Test sur la console
        Debug.Log("Le roi est mort RIP");

        // Animation Goblin qui meurt
        animator.SetBool("KingDeath", true);

        //Rendre le Goblin incapable de faire quelque chose parcequ'il est mort
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
