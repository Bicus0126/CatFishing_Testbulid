using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    string objTag;
    public float SpawnInterval = 5f;
    void Start()
    {
        StartCoroutine(spawnBlock(SpawnInterval));
        objectPooler = ObjectPooler.Instance;
    }

    IEnumerator spawnBlock(float spawnInterval)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(spawnInterval);
        while (true)
        {
            int rgn = UnityEngine.Random.Range(0, 10);
            do
            {
                rgn++;
                rgn %= objectPooler.poolList.Count;
                objTag = objectPooler.poolList[rgn].tag;
            } while (objectPooler.SpawnFromPool(tag, transform.position, Quaternion.identity) != null);
            yield return waitForSeconds;
        }
    }
}
