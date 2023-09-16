using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;

    [Header("Main")]
    [Range (1, 2)] [SerializeField] private int playerNum;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 50;
    [SerializeField] private float slideSpeed = 5;
    [SerializeField] private float wallJumpLerp = 10;
    
    [Header("States")]
    public bool canMove;
    public bool isFrozen;
    public bool canUseAbility;
    public bool wallJumped;
    public bool wallSlide;

    private bool groundTouch;

    [SerializeField] private int side = 1;

    [Header("Effects")]
    [SerializeField] private ParticleSystem jumpParticle;
    [SerializeField] private ParticleSystem wallJumpParticle;
    [SerializeField] private ParticleSystem slideParticle;

    [Header("Special Abilities")]
    [SerializeField] private GameObject lightFlashPrefab;
    [SerializeField] private GameObject[] flashableObjects;

    // Start is called before the first frame update
    void Start() {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();

        rb.gravityScale = 3;
    }

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxis("Horizontal" + playerNum);
        float y = Input.GetAxis("Vertical" + playerNum);
        float xRaw = Input.GetAxisRaw("Horizontal" + playerNum);
        float yRaw = Input.GetAxisRaw("Vertical" + playerNum);
        Vector2 dir = new Vector2(x, y);

        if (!isFrozen) {
            Walk(dir);
            anim.SetHorizontalMovement(x, y, rb.velocity.y);
        } else {
            SetAnimToIdle();
            rb.velocity = Vector2.zero;
        }

        if ((!coll.onWall || !canMove) && !isFrozen) {
            wallSlide = false;
        }

        if (coll.onGround && !isFrozen) {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        if((coll.onWall && !coll.onGround) && !isFrozen) {
            if (x != 0) {
                wallSlide = true;
                WallSlide();
            }
        }

        if ((!coll.onWall || coll.onGround) && !isFrozen) {
            wallSlide = false;
        }

        if (Input.GetButtonDown("Jump" + playerNum) && !isFrozen) {

            bool precheck = coll.onGround && (coll.onLeftWall || coll.onRightWall) && Input.GetAxis("Horizontal" + playerNum) != 0;
            
            if (!precheck)
            {
                anim.SetTrigger("jump");

                if (coll.onGround)
                {
                    Jump(Vector2.up, false);
                }

                if (coll.onWall && !coll.onGround)
                {
                    WallJump();
                }
            }
        }

        if ((coll.onGround && !groundTouch) && !isFrozen) {
            GroundTouch();
            groundTouch = true;
        }

        if ((!coll.onGround && groundTouch) && !isFrozen) {
            groundTouch = false;
        }

        WallParticle(y);

        if ((wallSlide || !canMove) && !isFrozen) {
            return;
        }

        if (x > 0 && !isFrozen) {
            side = 1;
            anim.Flip(side);
        }

        if (x < 0 && !isFrozen) {
            side = -1;
            anim.Flip(side);
        }

        if (Input.GetButtonDown("Fire1") && canUseAbility && playerNum == 1 && !isFrozen) {
            StopCoroutine(DisableAbility(0));
            StartCoroutine(DisableAbility(3f));

            GameObject clone = Instantiate(lightFlashPrefab, transform.position, Quaternion.identity);
            Destroy(clone, 1f);
            
            for (int i = 0; i < flashableObjects.Length; i++)
            {
                if (flashableObjects[i] != null && flashableObjects[i].activeSelf)
                {
                    flashableObjects[i].SendMessage("PlayerFlashed");
                }
            }
        }
    }

    void GroundTouch() {
        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }

    private void WallJump() {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall) {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(0.3f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide() {
        if(coll.wallSide != side) {
            anim.Flip(side * -1);
        }

        if (!canMove) {
            return;
        }

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall)) {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir) {
        if (!canMove) {
            return;
        }

        if (!wallJumped) {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        } else {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool wall) {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }

    IEnumerator DisableMovement(float time) {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    IEnumerator DisableAbility(float time) {
        canUseAbility = false;
        yield return new WaitForSeconds(time);
        canUseAbility = true;
    }

    void RigidbodyDrag(float x) {
        rb.drag = x;
    }

    void WallParticle(float vertical) {
        var main = slideParticle.main;

        if (wallSlide || (vertical < 0)) {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        } else {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide() {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    public void SetAnimToIdle() {
        anim.SetHorizontalMovement(0, 0, 0);
    }

    public void FreezePlayer(bool makeFrozen) {
        isFrozen = makeFrozen;
    }
}
