using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int number;
    public List<GameObject> nodes;
    public int nodesNumber;
    
    public float xTolerance;
    public float worldLimitY;
    private Transform mainCam;

    public float destroyVelocity;

    private int i;

    public AnimationClip explodingClip;

    public float destroyNodeEachXSec;

    private void Start()
    {
        mainCam = Camera.main.transform;
    }

    private void Update()
    {
        if (transform.position.x < mainCam.position.x - xTolerance)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < worldLimitY)
        {
            Destroy(gameObject);
        }
    }
    
    private IEnumerator DestroyBack()
    {
        Destroy(nodes[nodesNumber - i]);
        yield return new WaitForSeconds(destroyVelocity);
        i += 1;
        StartDestroy();
    }

    public void StartDestroy()
    {
        if (i == nodesNumber - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(DestroyBack());
        }
    }

    public void DestroyThis()
    {
        StartCoroutine(DestroyThisCoroutine(explodingClip.length));
    }

    private IEnumerator DestroyThisCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        StartCoroutine(DestroyNodes());
    }

    private IEnumerator DestroyNodes()
    {
        foreach (var node in nodes)
        {
            Destroy(node);
            yield return new WaitForSeconds(destroyNodeEachXSec);
        }
    }
}
