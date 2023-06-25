using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class FeetBoss : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody rb;
    private Animator anim;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider LefthealthSlider;
    [SerializeField] Slider RighthealthSlider;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent navMeshAgent;

    [SerializeField] MultipleVoiceline feetAttackVoiceLines;
    [SerializeField] MultipleVoiceline toesDamageVoiceLines;

    //GameObject player;
    [Header("Movement")]
    public float range;
    public Transform centrePoint;

    [Header("Health")]
    public int FinalHealth = 20;
    public int currentHealth;
    public int LeftHealth = 10;
    public int LeftcurrentHealth;
    public int RightHealth = 10;
    public int RightcurrentHealth;

    public GameObject LeftDeath;
    public GameObject RightDeath;
    public GameObject LeftNew;
    public GameObject RightNew;

    [Header("Feet and Attack")]
    public GameObject Left;
    public GameObject Right;
    public GameObject Blast;
    public Transform firePointLeft;
    public Transform firePointRight;

    GameObject leftHand;
    GameObject rightHand;
    List<GameObject> hands = new List<GameObject>();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        currentHealth = FinalHealth;
        healthSlider.maxValue = FinalHealth;
        healthSlider.value = currentHealth;

        LeftcurrentHealth = LeftHealth;
        LefthealthSlider.maxValue = LeftHealth;
        LefthealthSlider.value = LeftcurrentHealth;

        RightcurrentHealth = RightHealth;
        RighthealthSlider.maxValue = RightHealth;
        RighthealthSlider.value = RightcurrentHealth;

        healthSlider.gameObject.SetActive(false);

        //anim.SetInteger("Health", currentHealth);

        //player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

        //Random Movement
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                agent.SetDestination(point);
            }
        }

        
        

        if(LeftcurrentHealth <= 0 && RightcurrentHealth <= 0)
        {
            healthSlider.gameObject.SetActive(true);
            Left.tag = "Enemy";
            Right.tag = "Enemy";
        }
    }

    public void Legs()
    {
        //Attacks
        int randomNumber = Random.Range(0, 5);
        Debug.Log(randomNumber);

        //still in phase 1
        if (LeftcurrentHealth > 0 || RightcurrentHealth > 0)
        {
            Debug.Log("Attack phase 1");
            if (LeftcurrentHealth > 0 && RightcurrentHealth > 0)
            {
                Debug.Log("Random");
                RandomChooseAttack(randomNumber);
            }
            else if (LeftcurrentHealth <= 0)
            {
                anim.SetTrigger("Walk/RightStomp");
            }

            else
            {
                anim.SetTrigger("Walk/LeftStomp");
            }
            //Instantiate(Blast, firePointRight.position, firePointRight.rotation);
        }
        //in phase 2
        else
        {
            RandomChooseAttack(randomNumber);
        }
    }

    private void RandomChooseAttack(int randomNumber)
    {
        if (randomNumber > 2)
        {
            anim.SetTrigger("Walk/RightStomp");
            //Instantiate(Blast, firePointRight.position, firePointRight.rotation);
        }
        else
        {
            anim.SetTrigger("Walk/LeftStomp");
            //Instantiate(Blast, firePointLeft.position, firePointLeft.rotation);
        }
    }

    public void StopMoving()
    {
        navMeshAgent.isStopped = true;
    }
    public void StartMoving()
    {
        navMeshAgent.isStopped = false;
    }

    //Random Movement
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    public void AttackLeft()
    {
        Debug.Log("Feet attack");
        Instantiate(Blast, firePointLeft.position, firePointLeft.rotation);
        feetAttackVoiceLines.Play();
    }

    public void AttackRight()
    {
        Debug.Log("Feet attack");
        Instantiate(Blast, firePointRight.position, firePointRight.rotation);
        feetAttackVoiceLines.Play();
    }

    public void Damage()
    {
        currentHealth -= 1;
        //anim.SetInteger("Health", currentHealth);
        healthSlider.value = currentHealth;
        toesDamageVoiceLines.Play();

        if (currentHealth <= 0)
        {
            BossDied();
        }
    }

    public void DamageLeft()
    {
        LeftcurrentHealth -= 1;
        //anim.SetInteger("Health", currentHealth);
        LefthealthSlider.value = LeftcurrentHealth;
        feetAttackVoiceLines.Play();

        if (LeftcurrentHealth <= 0)
        {
            LefthealthSlider.gameObject.SetActive(false);
            LeftDeath.gameObject.SetActive(false);
            LeftNew.gameObject.SetActive(true);
        }

        CheckLeftPhase2();
    }

    public void DamageRight()
    {
        RightcurrentHealth -= 1;
        //anim.SetInteger("Health", currentHealth);
        RighthealthSlider.value = RightcurrentHealth;
        feetAttackVoiceLines.Play();

        if (RightcurrentHealth <= 0)
        {
            RighthealthSlider.gameObject.SetActive(false);
            RightDeath.gameObject.SetActive(false);
            RightNew.gameObject.SetActive(true);
        }

        CheckRightPhase2();
    }

    public void StareAtPlayer()
    {
        //Freeze Rotations
        transform.LookAt(player.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    public void SetPlayer(GameObject p)
    {
        this.player = p;
    }

    public void SetLeftHand(GameObject l)
    {
        this.leftHand = l;
    }
    public void SetRightHand(GameObject r)
    {
        this.rightHand = r;
    }
    public void SetEnemyManager(EnemyManager e)
    {
        this.enemyManager= e;
    }

    public void CheckRightPhase2()
    {
        if (RightcurrentHealth <= 0)
        {
            enemyManager.RightHandStartPhase2();    
        }
    }
    public void CheckLeftPhase2()
    {
        if (LeftcurrentHealth <= 0)
        {
            enemyManager.LeftHandStartPhase2();
        }
    }
    public void BossDied()
    {
        enemyManager.BossDied();
    }
}