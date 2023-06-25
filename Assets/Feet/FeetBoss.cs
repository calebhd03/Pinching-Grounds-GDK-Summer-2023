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

    [Header("Feet and Attack")]
    public GameObject Left;
    public GameObject Right;
    public GameObject Blast;
    public Transform firePointLeft;
    public Transform firePointRight;

    GameObject player;
    GameObject leftHand;
    GameObject rightHand;
    EnemyManager enemyManager;
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
        //anim.SetInteger("Health", currentHealth);

        //player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

        healthSlider.gameObject.SetActive(false);

        //Freeze Rotations
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);

        //Random Movement
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                agent.SetDestination(point);
            }
        }

        //Attacks
        int randomNumber = Random.Range(0, 2);
        if (randomNumber == 1)
        {
            anim.SetTrigger("Walk/RightStomp");
            //Instantiate(Blast, firePointRight.position, firePointRight.rotation);
        }
        else
        {
            anim.SetTrigger("Walk/LeftStomp");
            //Instantiate(Blast, firePointLeft.position, firePointLeft.rotation);
        }

        if(LeftcurrentHealth == 0 && RightcurrentHealth == 0)
        {
            healthSlider.gameObject.SetActive(true);
            Left.tag = "Enemy";
            Right.tag = "Enemy";
        }
        if(LeftcurrentHealth == 0)
        {
            LefthealthSlider.gameObject.SetActive(false);
        }
        if (RightcurrentHealth == 0)
        {
            RighthealthSlider.gameObject.SetActive(false);
        }
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

    public void Damage()
    {
        currentHealth -= 1;
        //anim.SetInteger("Health", currentHealth);
        healthSlider.value = currentHealth;
    }

    public void DamageLeft()
    {
        LeftcurrentHealth -= 1;
        //anim.SetInteger("Health", currentHealth);
        LefthealthSlider.value = LeftcurrentHealth;
    }

    public void DamageRight()
    {
        RightcurrentHealth -= 1;
        //anim.SetInteger("Health", currentHealth);
        RighthealthSlider.value = RightcurrentHealth;
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

    /*
    public void Phase2()
    {
        if (anim.GetInteger("Health") <= 10)
        {
            enemyManager.StartPhase2();    
        }
    }*/
}