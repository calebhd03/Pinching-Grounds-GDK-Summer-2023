using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject handPrefab;
    [SerializeField] GameObject feetPrefab;
    List<GameObject> hands = new List<GameObject>();
    GameObject boss;

    public GameObject SpawnHand(Vector3 spawnPoint)
    {
        Debug.Log("Spawn hand at " + spawnPoint);
        GameObject newHand = Instantiate(handPrefab, spawnPoint, Quaternion.identity, transform);

        newHand.GetComponent<HandManager>().SetPlayer(player);
        return newHand;
    }

    public void SpawnBoss(Vector3 spawnPoint)
    {
        Debug.Log("Spawn boss at " + spawnPoint);
        boss = Instantiate(feetPrefab, spawnPoint, Quaternion.identity, transform);

        GameObject newHand = SpawnHand(boss.transform.position);
        hands.Add(newHand);

        FeetBoss feetBoss = boss.GetComponent<FeetBoss>();
        feetBoss.SetLeftHand(newHand);
        feetBoss.SetPlayer(player);
        feetBoss.SetEnemyManager(this);
    }

    public void StartPhase2()
    {
        Debug.Log("Start Phase 2");
        GameObject newHand = SpawnHand(boss.transform.position);

        FeetBoss feetBoss = boss.GetComponent<FeetBoss>();
        feetBoss.SetRightHand(newHand);

        foreach(GameObject hand in hands)
        {
            hand.GetComponent<HandManager>().StartPhase2();
        }
    }


}
