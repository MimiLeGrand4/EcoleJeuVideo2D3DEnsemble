using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_isWallSliding = false;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;
    private float               m_rollDuration = 8.0f / 14.0f;
    private float               m_rollCurrentTime;
    public int sceneBuildIndex;

    //Modifier Olivier - Attack
    public Transform HitBox;
    public GameObject restartPanel;
    public GameObject pausePanel;
    public float  hitBoxRange = 0.5f;
    private bool directionDeLaHitBox = true;
    private Vector3 positionInitial;
    public LayerMask enemyLayer;
    public int degat = 1;
    public float attackSpeed = 0.25f;
    public float vie = 5;
    private float vieActuel;
    
    //Modifier Olivier - Protection
    private bool playerProtected = false;
    public float tempsInvincibilite = 1f;
    private bool playerInvincible = false;
    private SpriteRenderer playerSprite;
    private float flickerTemps = 0.1f;

    //Modifier Olivier - Audio
    public AudioSource PlayerHurtSource;
    public AudioClip PlayerHurtClip;



    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();

        //Modifier par Olivier
        positionInitial = HitBox.localPosition;
        vieActuel = vie;
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update ()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // Swap direction of sprite depending on walk direction
        //Modifier par Olivier
        if (inputX > 0 && !directionDeLaHitBox)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;

            // Modifier par Olivier
            HitBox.localPosition = new Vector3(HitBox.localPosition.x * -1, HitBox.localPosition.y, HitBox.localPosition.z);
            directionDeLaHitBox = true;

        }
            
        else if (inputX < 0 && directionDeLaHitBox)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;

            // Modifier par Olivier
            HitBox.localPosition = new Vector3(-positionInitial.x, positionInitial.y, positionInitial.z);
            directionDeLaHitBox = false;
        }

        // Modifier par Olivier
        // Move
        if (!m_rolling && !playerProtected)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
            

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // -- Handle Animations --
        //Wall Slide
        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        //Attack(Modifier: Olivier)
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > attackSpeed && !m_rolling)
        {
            m_currentAttack++;

            // Loop back to one after third attack
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            m_animator.SetTrigger("Attack" + m_currentAttack);
            Attack();

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
            playerProtected = true;
        }

        else if (Input.GetMouseButtonUp(1)){
            m_animator.SetBool("IdleBlock", false);
            playerProtected = false;
        }
            

        // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
            

        //Jump
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            // Reset timer
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseScene();
        }

        //Idle
        else
        {
            // Prevents flickering transitions to idle
            m_delayToIdle -= Time.deltaTime;
                if(m_delayToIdle < 0)
                    m_animator.SetInteger("AnimState", 0);
        }
    }

    //Modifier Olivier

    //Modifier Olivier
    public float getVieActuel(){
        return vieActuel;
    }

    void Attack(){
        // Détecter un enemies
        Collider2D[] attackEnemies = Physics2D.OverlapCircleAll(HitBox.position, hitBoxRange, enemyLayer);
        //Faire des dégats
        foreach(Collider2D enemis in attackEnemies)
        {
            //Test sur la console
            Debug.Log("Ça fait mal " + enemis.name);

            //Dégat infligé à l'ennemi
            if (enemis.name == "Goblin" || enemis.name.StartsWith("Goblin (") && enemis.name.EndsWith(")")){
                enemis.GetComponent<GoblinScript>().goblinTakeHit(degat);
            }

            if(enemis.name == "King" || enemis.name.StartsWith("King (") && enemis.name.EndsWith(")")){
                enemis.GetComponent<KingScript>().kingTakeHit(degat);
            }

            if(enemis.name == "Skeleton" || enemis.name.StartsWith("Skeleton (") && enemis.name.EndsWith(")")){
                enemis.GetComponent<SkeletonScript>().skeletonTakeHit(degat);
            }
            
            

        }
    }

    //Modifier Olivier
    void OnDrawGizmosSelected() {
        if (HitBox == null)
        return;

        Gizmos.DrawWireSphere(HitBox.position, hitBoxRange);
    }

    //Modifier Olivier
    public void playerTakeHit(float damage){
        if(playerProtected){
            //Animation Joueur qui prend des dégats
             m_animator.SetTrigger("Block");
        }

        else if(playerInvincible){
            return;
        }

        else{
            PlayerHurtSource.PlayOneShot(PlayerHurtClip);
            Debug.Log(damage);
                
            vieActuel -= damage;

            //Animation Joueur qui prend des dégats
            m_animator.SetTrigger("Hurt");

            // Le joueur est invincible et le timer d'invincibilité commence
            playerInvincible = true;
            Invoke("EndInvincibility", tempsInvincibilite);

            if (vieActuel <= 0){
                playerDie();
            }

            // Appelle de la fonction FlickerSprite
            StartCoroutine(FlickerSprite());
        }
        
    }

    //Modifier Olivier - Collision avec des Spikes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Collision avec des Spikes
        if (collision.gameObject.CompareTag("Spike"))
        {
            vieActuel -= vieActuel;

            if (vieActuel <= 0)
            {
                playerDie();
            }
        }
    }

    //Modifier Olivier - Collision avec des Monstres
     private void OnTriggerEnter2D(Collider2D collision)
     {
        // Regarde si le joueur est invincible
        if (playerInvincible) {
            return;
        }

        if (collision.CompareTag("Monstre"))
        {
            // Le joueur est invincible et le timer d'invincibilité commence

            if(playerProtected){
            //Animation Joueur qui prend des dégats
             m_animator.SetTrigger("Block");
            }

            else{
                PlayerHurtSource.PlayOneShot(PlayerHurtClip);
                m_animator.SetTrigger("Hurt");
                playerInvincible = true;
                Invoke("EndInvincibility", tempsInvincibilite);

                
                vieActuel -= 2;

                if(vieActuel <= 0){
                    playerDie();
                }

                // Appelle de la fonction FlickerSprite
                StartCoroutine(FlickerSprite());
            }
        }/*else if (collision.CompareTag("changementScene"))
        {
            Debug.Log("Changement de niveau");
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }*/
     }

    //Modifier Olivier - Invicibilité
    private void EndInvincibility()
    {
        playerInvincible = false;
        playerSprite.enabled = true;
    }

    //Modifier Olivier - Annimation flicker
    private IEnumerator FlickerSprite()
    {
        while (playerInvincible)
        {
            // Animation du sprite qui s'active et qui se désactive
            playerSprite.enabled = !playerSprite.enabled;

            // Attendre que le flicker s'arrête
            yield return new WaitForSeconds(flickerTemps);
        }

        // Activer le Spritre du joueur
        playerSprite.enabled = true;
    }

    //Modifier Olivier
    void playerDie(){

        //Test sur la console
        Debug.Log("Le joueur est mort RIP");

        // Animation Joueur qui meurt
        m_animator.SetBool("noBlood", m_noBlood);
        m_animator.SetTrigger("Death");

        //Rendre le Joueur incapable de faire quelque chose parcequ'il est mort
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // Appeller la panel de restart
        restartPanel.SetActive(true);
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

    //Pause
    void PauseScene()
    {
        if (Time.timeScale == 1)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
