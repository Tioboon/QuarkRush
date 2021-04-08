using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGenerator : MonoBehaviour
{
    public List<int> ropeSizeBounds;
    public List<int> heightBounds;
    public List<float> xPosVariation;

    private float xPos = 0;

    private GameObject baseRope;
    private GameObject ropeNode;
    private SpriteRenderer spriteNode;
    private Transform containerRope;
    private Rigidbody2D firstRB;
    private Rigidbody2D secondRB;
    private GameObject lastNode;
    public Vector3 lastRopeSpawnedPos;

    private int nodeNumber;

    public int initialRopesNumber;
    
    private bool firstRope;
    private int ropeNumber;

    public float nodeSize;

    private GameObject collectable;
    public List<float> spawnGap;
    public List<float> distXFromPlayer;
    private GameObject player;
    private Transform collectableContainer;

    void Start()
    { 
        GetReferences();
        for (int i = 0; i < initialRopesNumber; i++)
        {
            SpawnRope();
        }
    }

    void GetReferences()
    {
        baseRope = transform.Find("Rope").gameObject;
        ropeNode = transform.Find("RopeNode").gameObject;
        spriteNode = ropeNode.GetComponent<SpriteRenderer>();
        containerRope = transform.Find("RopeContainer");
        collectable = transform.Find("Collectable").gameObject;
        player = GameObject.Find("Player");
        collectableContainer = transform.Find("CollectableContainer");
    }

    public void SpawnRope()
    {
        int randomSize;
        float yPos;
        //Randomize values
        if(!firstRope)
        {
            randomSize = 50;
            yPos = 50;
            firstRope = true;
        }
        else
        {
            randomSize = Random.Range(ropeSizeBounds[0], ropeSizeBounds[1]);
            yPos = Random.Range(heightBounds[0], heightBounds[1]);
        }
        //Instantiate the base of the rope
        GameObject rope = Instantiate(baseRope);
        rope.transform.position = new Vector3(xPos, yPos);

        rope.transform.parent = containerRope;
        
        rope.SetActive(true);

        //change the yPos by half of the baseRope y Scale
        yPos -= rope.transform.lossyScale.y / 1.5f;

        lastNode = rope;

        var ropeBase = rope.GetComponent<Base>();
        ropeBase.number = ropeNumber;
        ropeNumber += 1;
        lastRopeSpawnedPos = rope.transform.position;
            
        for (int i = 0; i < randomSize; i++)
        {
            //Instantiate
            var node = Instantiate(ropeNode);
            var nodeHinge = node.GetComponent<HingeJoint2D>();
            nodeSize = node.transform.lossyScale.y * spriteNode.sprite.bounds.extents.y;
            
            //Get hinge of last instantiated node
            if (i != 0)
            {
                var lastNodeHinge = lastNode.AddComponent<HingeJoint2D>();
                lastNodeHinge.autoConfigureConnectedAnchor = false;
                lastNodeHinge.connectedBody = node.GetComponent<Rigidbody2D>();
                lastNodeHinge.anchor += new Vector2(0, nodeSize/2);
                var limits = lastNodeHinge.limits;
                limits.max = -nodeHinge.limits.max;
                limits.min = -nodeHinge.limits.min;
                lastNodeHinge.limits = limits;
                lastNodeHinge.connectedAnchor = Vector2.zero;
                lastNodeHinge.enableCollision = true;
            }
            else
            {
                yPos -= nodeSize / 2;
            }
            
            
            //Set connected to the hinge of last node
            nodeHinge.connectedBody = lastNode.GetComponent<Rigidbody2D>();
            nodeHinge.anchor -= new Vector2(0, nodeSize/2);

            //change the position in Y by half of the scale of ropeNode;
            yPos -= nodeSize;
            node.transform.position = new Vector3(xPos, yPos);
            
            node.transform.parent = rope.transform;
            
            node.SetActive(true);
            node.name = "Node_" + nodeNumber;
            node.GetComponent<Node>().number = nodeNumber;
            nodeNumber += 1;
            ropeBase.nodes.Add(node);
            ropeBase.nodesNumber += 1;

            lastNode = node;
        }

        var distanceJoint = rope.GetComponent<DistanceJoint2D>();
        distanceJoint.connectedBody = lastNode.GetComponent<Rigidbody2D>();
        distanceJoint.distance = (lastNode.transform.position - rope.transform.position).magnitude;

        var xPosIncrease = Random.Range(xPosVariation[0], xPosVariation[1]);
        xPos += xPosIncrease;
        
        nodeNumber = 0;
    }

    public void SpawnCollectables()
    {
        print("Spawnando bixinhos");
        var newColl = Instantiate(collectable);
        var randomYPos = Random.Range(heightBounds[0], heightBounds[1]);
        var spawnPos = player.transform.position + new Vector3(Random.Range(distXFromPlayer[0], distXFromPlayer[1]), randomYPos);
        newColl.transform.position = spawnPos;
        newColl.gameObject.SetActive(true);
        newColl.transform.parent = collectableContainer;
    }
}
