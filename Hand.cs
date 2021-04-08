using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Vector2 initPos;
    private Vector2 finalPos;

    private bool lerp;
    private float lerpVar;
    public float lerpMod;

    public int isHoldingNumber;

    private Rigidbody2D holdingRigidBody;
    private Rigidbody2D thisRigidBody;
    
    private FixedJoint2D newFixedJoint;
    private DistanceJoint2D _distanceJoint2D;
    private float waitingToCollide;

    public float timeWaitToCollide;
    public float velocityMultiplier;

    private HingeJoint2D[] hinges;

    private bool flippedSprite;

    private RopeGenerator _ropeGenerator;

    private float cameraXDis;

    private Base actualBase;

    private bool unchained;
    private bool spawnedRopes;
    private bool holdingLastRope;

    private PlayerAnimations _playerAnimations;

    private bool doMortal;
    private float countMortal;

    public float maxYPos;

    private GameController _gameController;

    public float initialVelocity;

    private bool calledCanvas;

    public GameObject canvasMenu;
    public GameObject canvasInGame;

    public float score;
    public float scoreMod;
    private float lastScoreSpawned;
    public float scoreDif;
    public float scoreDifMod;

    private GameVars _gameVars;

    public bool zoomCameraOut;
    public bool zoomCameraIn;

    public float applyForceTimeExec;
    private bool goingRight;
    private bool goingLeft;

    public float velocityModModifier;
    private float initVelocityMod;

    private SoundEmmiter _soundEmmiter;
    private void Start()
    {
        isHoldingNumber = 999; //Just for check true without bool
        thisRigidBody = GetComponent<Rigidbody2D>();
        holdingRigidBody = thisRigidBody;
        _ropeGenerator = GameObject.Find("RopeGenerator").GetComponent<RopeGenerator>();
        cameraXDis = Camera.main.orthographicSize / 9 * 16;
        _playerAnimations = transform.Find("PlayerSprite").GetComponent<PlayerAnimations>();
        _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        thisRigidBody.velocity += Vector2.right * initialVelocity;
        canvasInGame = GameObject.Find("CanvasInGame");
        _gameVars = GameObject.Find("GameController").GetComponent<GameVars>();
        initVelocityMod = velocityMod;
        _soundEmmiter = Camera.main.transform.Find("Sounds").GetComponent<SoundEmmiter>();
    }

    private void Update()
    {
        if (lerp)
        {
            LerpToNode();
        }
        Inputs();
        waitingToCollide -= Time.deltaTime;
        if (transform.position.x + cameraXDis > _ropeGenerator.lastRopeSpawnedPos.x)
        {
            _ropeGenerator.SpawnRope();
            spawnedRopes = true;
        }

        if (doMortal)
        {
            countMortal += Time.deltaTime;
            if (countMortal > _playerAnimations.mortalClip.length)
            {
                _playerAnimations.ChangeToFlying();
                countMortal = 0;
                doMortal = false;
            }
        }

        if (transform.position.y < maxYPos)
        {
            Die();
        }

        if (isHoldingNumber == 999)
        {
            score += scoreMod * Time.deltaTime;
        }
        
        CheckSpawnCollectable();
    }

    private void CheckSpawnCollectable()
    {
        if (score - scoreDif > lastScoreSpawned)
        {
            _ropeGenerator.SpawnCollectables();
            lastScoreSpawned = score;
            WaitToChangeVar();
        }
    }

    private IEnumerator WaitToChangeVar()
    {
        yield return new WaitForSeconds(1);
        scoreDif -= scoreDifMod;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        _gameController.paused = true;
        CallMenu();
        if (score > _gameVars.maxHighScore)
        {
            _gameVars.maxHighScore = (int)score;
            PlayerPrefs.SetInt("HighScore", _gameVars.maxHighScore);
        }

        isHoldingNumber = 999;
        
    }
    
    private void CallMenu()
    {
        if (_gameController.paused && !calledCanvas)
        {
            canvasMenu.SetActive(true);
            canvasInGame.SetActive(false);
            calledCanvas = true;
        }
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RopeNode"))
        {
            if(other.transform.parent.GetComponent<Base>() == actualBase) return;
            if(waitingToCollide > 0) return;
            Node otherNode = other.GetComponent<Node>();
            if (isHoldingNumber == 999)
            {
                isHoldingNumber = otherNode.number;
            }
            if (isHoldingNumber != otherNode.number) return;
            _soundEmmiter.GrapRopeSound();
            zoomCameraOut = true;
            zoomCameraIn = false;
            _playerAnimations.ChangeToGrap();
            doMortal = false;
            countMortal = 0;
            SetLerpTrue(other);
            _distanceJoint2D = other.transform.parent.GetComponent<DistanceJoint2D>();
            _distanceJoint2D.connectedBody = GetComponent<Rigidbody2D>();
            _distanceJoint2D.distance = (transform.position - _distanceJoint2D.transform.position).magnitude;
            _distanceJoint2D.connectedAnchor = Vector2.zero;

            //holdingRigidBody = other.GetComponent<Rigidbody2D>();
            
            var baseBase = other.transform.parent.GetComponent<Base>();
            if (isHoldingNumber < baseBase.nodesNumber-1 && !unchained)
            {
                baseBase.nodes[isHoldingNumber + 1].GetComponent<HingeJoint2D>().enabled = false;
                unchained = true;
            }

            //Fixar o player de alguma outra forma, essa buga a corda;
            //newFixedJoint = other.gameObject.AddComponent<FixedJoint2D>();
            //newFixedJoint.connectedBody = GetComponent<Rigidbody2D>();

            actualBase = other.transform.parent.GetComponent<Base>();
            if(isHoldingNumber != actualBase.nodesNumber-1)
            {
                hinges = other.GetComponents<HingeJoint2D>();
                var hingeTwo = hinges[1]; 
                hingeTwo.connectedBody = thisRigidBody;
            }
            else
            {
                var newHinge = other.gameObject.AddComponent<HingeJoint2D>();
                newHinge.autoConfigureConnectedAnchor = false;
                newHinge.connectedBody = thisRigidBody;
                newHinge.anchor += new Vector2(0, _ropeGenerator.nodeSize/2);
                var limits = newHinge.limits;
                limits.max = -baseBase.nodes[isHoldingNumber-1].GetComponent<HingeJoint2D>().limits.max;
                limits.min = -baseBase.nodes[isHoldingNumber-1].GetComponent<HingeJoint2D>().limits.min;
                newHinge.limits = limits;
                newHinge.connectedAnchor = Vector2.zero;
                newHinge.enableCollision = true;
                holdingLastRope = true;
            }
            
            
            actualBase.nodesNumber = isHoldingNumber;
            for (int i = 0; i < actualBase.nodes.Count-1; i++)
            {
                if (i > isHoldingNumber)
                {
                    actualBase.nodes.Remove(actualBase.nodes[i]);
                    Destroy(actualBase.nodes[i]);
                }
            }
            
            spawnedRopes = false;
        }
    }

    private void LerpToNode()
    {
        transform.position = Vector3.Lerp(initPos, finalPos, lerpVar);
        lerpVar += lerpMod;
        if (lerpVar > 1)
        {
            transform.position = Vector3.Lerp(initPos, finalPos, 1);
            lerp = false;
            lerpVar = 0;
        }
    }

    private void SetLerpTrue(Collider2D other)
    {
        finalPos = other.transform.position;
        initPos = transform.position;
        lerp = true;
    }
    
    public float velocityMod;

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(MoveLeft());
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(MoveRight());
        }

        if(isHoldingNumber != 999)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    public void Jump()
    {
        //newFixedJoint.connectedBody = null;
        _soundEmmiter.JumpSound();
        if(!holdingLastRope)
        {
            var hingeTwo = hinges[1];
            hingeTwo.connectedBody = null;
        }
        thisRigidBody.velocity = holdingRigidBody.velocity * velocityMultiplier;
        holdingRigidBody = thisRigidBody;
        _distanceJoint2D.connectedBody = actualBase.nodes[actualBase.nodesNumber-1].GetComponent<Rigidbody2D>();
        isHoldingNumber = 999;
        waitingToCollide = timeWaitToCollide;
        unchained = false;
        actualBase.StartDestroy();
        _playerAnimations.ChangeToMortal();
        countMortal = 0;
        doMortal = true;
        zoomCameraOut = false;
        zoomCameraIn = true;
    }

    public void StartMovingLeft()
    {
        if (!goingLeft)
        {
            if(isHoldingNumber != 999)
            {
                _soundEmmiter.SwingRopeSound();
            }
        }
        goingLeft = true;
        goingRight = false;
        StopAllCoroutines();
        StartCoroutine(MoveLeft());
    }

    public void StartMovingRight()
    {
        if (!goingRight)
        {
            if (isHoldingNumber != 999)
            {
                _soundEmmiter.SwingRopeSound();
            }
        }
        goingRight = true;
        goingLeft = false;
        StopAllCoroutines();
        StartCoroutine(MoveRight());
    }

    public IEnumerator MoveLeft()
    {
        for (int i = 0; i < 10; i++)
        {
            holdingRigidBody.velocity += (-Vector2.right * velocityMod);
        }
        if(isHoldingNumber != 999)
        {
            if(!doMortal)
            {
                _playerAnimations.ChangeToBack();
            }
        }

        velocityMod -= velocityModModifier;
        
        yield return new WaitForSeconds(applyForceTimeExec);
        if (velocityMod <= 0)
        {
            StopAllCoroutines();
            goingLeft = false;
            velocityMod = initVelocityMod;
        }
        if (goingLeft)
        {
            StartCoroutine(MoveLeft());
        }
    }

    public IEnumerator MoveRight()
    {
        for (int i = 0; i < 10; i++)
        {
            holdingRigidBody.velocity += (Vector2.right * velocityMod);
        }
        if (isHoldingNumber != 999)
        {
            if(!doMortal)
            {
                _playerAnimations.ChangeToFront();
            }
        }
        
        velocityMod -= velocityModModifier;

        if (velocityMod <= 0)
        {
            StopAllCoroutines();
            goingRight = false;
            velocityMod = initVelocityMod;
        }
        
        yield return new WaitForSeconds(applyForceTimeExec);
        if (goingRight)
        {
            StartCoroutine(MoveRight());
        }
    }
    
    private void Rotate()
    {
        if (isHoldingNumber != 999)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            if (!flippedSprite)
            {
                var velocityAngle = Vector3.Angle(Vector3.right, thisRigidBody.velocity);
                if (Vector2.right.y > thisRigidBody.velocity.y)
                {
                    velocityAngle = -velocityAngle;
                }

                if (thisRigidBody.velocity.x < 0)
                {
                    flippedSprite = true;
                }

                transform.eulerAngles = new Vector3(0, 0, velocityAngle);
            }
            else
            {
                var velocityAngle = Vector3.Angle(Vector3.left, thisRigidBody.velocity);
                if (Vector2.right.y > thisRigidBody.velocity.y)
                {
                    velocityAngle = -velocityAngle;
                }

                if (thisRigidBody.velocity.x > 0)
                {
                    flippedSprite = false;
                }

                transform.eulerAngles = new Vector3(0, 180, velocityAngle);
            }
        }
    }
}
