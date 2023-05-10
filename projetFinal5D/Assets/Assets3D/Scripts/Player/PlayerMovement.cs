using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    // Variable Statistique Joueur
    public int health = 5;
    public int damage = 1;

    //Variable imunité quand le joueur est touché
    public float invincibilityTime = 5f;
    private float invincibilityTimer = 0f;
    private bool isInvincible = false;
    private float blinkTimer = 0.0f;
    public Renderer[] renderers;


    // Variable Component Joueur
    private CharacterController controller;
    private Animator animator;
    private Rigidbody rb;
    public AudioSource hurtSound;


    // Variable Mouvement Joueur
    public float speed = 6f;
    private float actualSpeed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public bool palyerIsRunning = false;

    // Variable Attaque Joueur
    private float attackTime = 0.0f;
    private int attackActuel = 0;
    public float attackSpeed = 0.25f;
    public bool isAttacking = false;

    //Variable Block Joueur
    public bool palyerIsBlocking = false;

    //Variable pour la camera
    public Transform cam;
    public CinemachineFreeLook freeLookCamera;
    private bool recenteringEnabled = false;

    // Player Canvas
    public GameObject restartCanvas;
    public GameObject pauseCanvas;

    //Test variable
    public bool clickTest = false;

    private void Start(){
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TPC();
        Attack();
        Block();
        PauseScene();
        InvincibilityTimer();
    }

    //Function pour bouger
    public void Move(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float magnitude = Mathf.Clamp01(direction.magnitude) * speed;
        direction.Normalize();
        
        //Condition pour savoir si le joueur cour ou marche
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            if (!palyerIsRunning)
            {
                palyerIsRunning = true;
            }

            else
            {
                palyerIsRunning = false;
            }
        }

        else{

            if(palyerIsBlocking){
                actualSpeed = 0;
            }

            else if(!palyerIsRunning){
                actualSpeed = speed;
            }

            else{
                actualSpeed = speed * 2;
            }
        }
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(moveDir.normalized * actualSpeed * Time.deltaTime);
            animator.SetBool("Walk", actualSpeed == speed);
            animator.SetBool("Run", actualSpeed == speed * 2);
        }

        else{
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
    }

    //Function pour attaquer
    public void Attack(){
        // Augmenter le timer qui contrôle la combinaison d'attaques
        attackTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && attackTime > attackSpeed)
        {
            attackActuel++;

            // Retourner à l'attaque 1 après la 3ème attaque
            if (attackActuel > 3)
                attackActuel = 1;

            // Réinitialiser la combinaison d'attaques si le temps écoulé depuis la dernière attaque est trop long
            if (attackTime > 1.0f)
                attackActuel = 1;

            // Appeler l'une des trois animations d'attaque : "Attack1", "Attack2", "Attack3"
            animator.SetTrigger("Attack" + attackActuel);

            // Méthode pour savoir si le joueur attaque;
            isAttacking = true;

            // Réinitialiser le timer
            attackTime = 0.0f;
        }

        if (attackTime > attackSpeed)
        {
            isAttacking = false;
        }
    }

    //Function pour Block
    public void Block()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            animator.SetBool("BlockIdle", true);
            palyerIsBlocking = true;
        }

        else if(Input.GetKeyUp(KeyCode.F)){
            animator.SetBool("BlockIdle", false);
            palyerIsBlocking = false;
        }
    }

    //Function pour la camera à la troisième personne
    public void TPC(){
        // Activer ou désactiver la caméra
        if (Input.GetMouseButton(1)) // Bouton droit de la souris enfoncé
        {
            freeLookCamera.m_XAxis.m_InputAxisName = "Mouse X"; // Entrée d'axe horizontal de la souris X
            freeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y"; // Entrée d'axe vertical de la souris Y
        }
        else
        {
            freeLookCamera.m_XAxis.m_InputAxisName = ""; // Désactiver l'entrée d'axe horizontal
            freeLookCamera.m_YAxis.m_InputAxisName = ""; // Désactiver l'entrée d'axe vertical
            freeLookCamera.m_XAxis.m_InputAxisValue = 0f; // Définir la valeur d'entrée d'axe horizontal à 0
            freeLookCamera.m_YAxis.m_InputAxisValue = 0f;
        }

        // Réinitialiser la position de la caméra
        if (Input.GetKeyDown(KeyCode.Q) && !recenteringEnabled)
        {
            // Activer la fonctionnalité de recentrage sur l'orientation cible
            freeLookCamera.m_RecenterToTargetHeading.m_enabled = true;
            recenteringEnabled = true;

            // Attendre 0,5 secondes puis désactiver la fonctionnalité
            StartCoroutine(DisableRecentering(1f));
        }
    }

    //Fonction pour que le joueur qui prend des dégâts
     public void TakeDamage(int damage){
        if(health > 1 && !palyerIsBlocking){
            if(invincibilityTimer <= 0f){ 
                animator.SetTrigger("TakeHit");
                hurtSound.Play();
                health -= damage;
                invincibilityTimer = invincibilityTime;
            } 
            else{
                Debug.Log("Player is invincible");
            }
        }

        else if(palyerIsBlocking){
            Debug.Log("Player is blocking");
        }

        else{
            health -= damage;
            PlayerDie();
        }
    }

    private void InvincibilityTimer()
    {
        if (invincibilityTimer > 0f)
        {
            invincibilityTimer -= Time.deltaTime;

            // Clignite le render du personnage
            blinkTimer += Time.deltaTime;
            if (blinkTimer >= 0.2f)
            {
                blinkTimer = 0f;
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = !renderer.enabled;
                }
            }

            // Fin du clignotement
            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = true;
                }
            }
        }
    }

    //Fonction pour la mort du joueur
    public void PlayerDie(){
        animator.SetBool("Death", true);

        // Désactiver les composants de mouvement et de collision
        GetComponent<Collider>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        // Désactiver ce script
        enabled = false;

        // Appeller la panel de restart
        restartCanvas.SetActive(true);
    }

    //Pause
    void PauseScene()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pauseCanvas.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    //Reset la position de la caméra
    private IEnumerator DisableRecentering(float delay)
    {
        yield return new WaitForSeconds(delay);
        freeLookCamera.m_RecenterToTargetHeading.m_enabled = false;
        recenteringEnabled = false;
    }

}
