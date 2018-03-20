using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.

    public bool hasKey1 = false;
    public bool hasKey2 = false;

    public bool swap = false;               // Should Character witch inside/outside
    public bool location = true;            // True = outside / False = inside
    public bool firstSwap = true;           // 
    public GameObject size;

    public GameObject mainCamera;
    public GameObject spawnInside;
    public GameObject spawnOutside;
    public GameObject respawnOutside;
    public Vector2 velocityInside;
    public Vector2 velocityOutside;
    public Vector2 velocityCurrent;

    public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
    public float jumpForceBig = 1000f;
    public float currentJumpForce;
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.


	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
    private float cameraPositionZ;

    public bool goingIn = false;

	void Awake()
	{
		// Setting up references.
		anim = GetComponent<Animator>();
        cameraPositionZ = mainCamera.transform.position.z;
    }


    void Update()
    {
        velocityCurrent = GetComponent<Rigidbody2D>().velocity;
        anim.ResetTrigger("Jump");
        anim.ResetTrigger("IN");
        anim.ResetTrigger("OUT");

        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        if (Mathf.Abs(transform.localScale.x) > 2f)
        {
            currentJumpForce = jumpForceBig;
            grounded = Physics2D.BoxCast(transform.position, new Vector2(2f, 1f), 0f, Vector2.down, 3.3f, 1 << LayerMask.NameToLayer("Ground"));
        }
        else
        {
            currentJumpForce = jumpForce;
            grounded = Physics2D.BoxCast(transform.position, new Vector2(2f, 1f), 0f, Vector2.down, 1.5f, 1 << LayerMask.NameToLayer("Ground"));
        }

        if (grounded) anim.SetBool("IsFalling", false);

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;
        if (grounded == false) anim.SetBool("IsFalling", true);

        // Swap between Outside/Inside
        if (Input.GetKeyDown(KeyCode.E))
            swap = true;
        if (location == false)
        {
            size.transform.position = this.transform.position;
        }
    }

    void FixedUpdate()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 5f, 1 << LayerMask.NameToLayer("Ground"));
        //if (hit)
        //{
        //    Debug.DrawRay(hit.point, hit.normal, Color.red);
        //    Debug.Log(hit.normal);
        //    Quaternion targetRotation = transform.rotation;
        //    Vector3 direction = new Vector3(hit.normal.x, hit.normal.y, 0f);
        //    //direction.x = 0;
        //    if (direction != Vector3.zero) targetRotation = Quaternion.LookRotation(direction);
        //    targetRotation.x = 0;
        //    targetRotation.y = 0;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20f * Time.deltaTime);
        //    Debug.Log(transform.rotation);
        //}
        //else
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //}

        // Swap between Outside/Inside
        if (swap)
        {
            if (firstSwap)              // Zabránění swapu na stejné místo
            {
                firstSwap = false;
                location = false;
            }
            swap = false;
            //anim.GetCurrentAnimatorStateInfo(0).IsName("Main.IN")
            if (goingIn)
            {
                Debug.Log("Baby");
            }
            else
            {
                // To Inside
                if (location)
                {
                    anim.SetTrigger("OUT");
                    // Save Outside velocity
                    velocityInside = GetComponent<Rigidbody2D>().velocity;
                    // Save Outside position
                    spawnOutside.transform.position = transform.position;
                    // Move camera to prevent blur
                    mainCamera.transform.position = new Vector3(spawnInside.transform.position.x, spawnInside.transform.position.y, cameraPositionZ);
                    // Inside
                    location = false;
                    // Apply Inside velocity
                    GetComponent<Rigidbody2D>().AddForce(velocityOutside * moveForce * 0.1f, ForceMode2D.Force);
                    // Move player to Inside position
                    transform.position = spawnInside.transform.position;
                }

                // To Outside
                else
                {
                    // Save Inside veloctiy
                    velocityOutside = GetComponent<Rigidbody2D>().velocity;
                    // Save Inside position
                    spawnInside.transform.position = transform.position;
                    // Move camera to prevent blur
                    mainCamera.transform.position = new Vector3(spawnOutside.transform.position.x, spawnOutside.transform.position.y, cameraPositionZ);
                    // Outside
                    location = true;
                    // Apply Outside velocity
                    GetComponent<Rigidbody2D>().AddForce(velocityInside * moveForce * 0.1f, ForceMode2D.Force);
                    // Move player to Outside position
                    transform.position = spawnOutside.transform.position;
                }
            }

            

            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        else
        {
            // Cache the horizontal input.
            float h = Input.GetAxis("Horizontal");
            if (Mathf.Abs(h) >= 0.1f)
            {
                anim.SetBool("IsWalking", true);
            }
            else
            {
                anim.SetBool("IsWalking", false);
            }

		    // If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		    if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
		    	// ... add a force to the player.
		    	GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);
            
		    // If the player's horizontal velocity is greater than the maxSpeed...
		    if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
		    	// ... set the player's velocity to the maxSpeed in the x axis.
		    	GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		    // If the input is moving the player right and the player is facing left...
		    if(h > 0 && !facingRight)
		    	// ... flip the player.
		    	Flip();
		    // Otherwise if the input is moving the player left and the player is facing right...
		    else if(h < 0 && facingRight)
		    	// ... flip the player.
		    	Flip();

		    // If the player should jump...
		    if(jump)
		    {
                // Set the Jump animator trigger parameter.
                anim.SetTrigger("Jump");
                

		    	// Play a random jump audio clip.
		    	int i = Random.Range(0, jumpClips.Length);
		    	AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

		    	// Add a vertical force to the player.
		    	GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, currentJumpForce));

		    	// Make sure the player can't jump again until the jump conditions from Update are satisfied.
		    	jump = false;
            }
        }
    }

    public void RespawnInside()
    {

    }

    public void RespawnOutside(bool change, Vector2 that)
    {
        if (change)
        {
            respawnOutside.transform.position = that;
        }
        else
        {
            transform.position = respawnOutside.transform.position;
        }
    }


    void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				GetComponent<AudioSource>().clip = taunts[tauntIndex];
				GetComponent<AudioSource>().Play();
			}
		}
	}

    IEnumerator Wait()
    {
        anim.SetTrigger("IN");
        //yield return StartCoroutine(anim.GetCurrentAnimatorStateInfo(0).IsName("IN"));
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}
