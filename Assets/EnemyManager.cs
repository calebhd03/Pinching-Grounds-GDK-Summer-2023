using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject handPrefab;
    List<GameObject> hands = new List<GameObject>();

    public void SpawnHand(Vector3 spawnPoint)
    {
        Debug.Log("Spawn hand at " + spawnPoint);
        GameObject newHand = Instantiate(handPrefab, spawnPoint, Quaternion.identity, transform);
        hands.Add(newHand);
        newHand.GetComponent<HandManager>().SetPlayer(player);
    }
    
}
