using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public Animator animator;
    private bool directionY;
    private bool directionX;
    private Vector3 direction = new Vector3(0, -1, 0);
    private BoxCollider2D boxCollider;
    private Vector2 movementInput;
    string[] physicalLayers = { "Blocking", "Character", "Interact", "Item" };
    private Rigidbody2D rb;

    [SerializeField] private Transform pfArrow;
    [SerializeField] private float interactDistance = 1f;
    [SerializeField] private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        bool success = false;

        if (movementInput != Vector2.zero)
        {
            success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }

            if (!success)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }
        }

        setAnimation(movementInput, success);
    }

    private void setAnimation(Vector2 movement, bool isMoving)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float distanceX = Vector2.Distance(new Vector2(worldPosition.x, 0), new Vector2(this.transform.position.x, 0));
        float distanceY = Vector2.Distance(new Vector2(0, worldPosition.y), new Vector2(0, this.transform.position.y));

        float lookX = this.transform.position.x > 0 ? worldPosition.x - this.transform.position.x : worldPosition.x + Mathf.Abs(this.transform.position.x);
        float lookY = this.transform.position.y > 0 ? worldPosition.y - this.transform.position.y : worldPosition.y + Mathf.Abs(this.transform.position.y);

        directionX = movement.x > 0 ? true : false;
        directionY = movement.y > 0 ? true : false;

        if (distanceX > distanceY)
        {
            animator.SetFloat("Horizontal", lookX > 0 ? 1 : -1);
            animator.SetBool("LookingX", true);
            animator.SetFloat("Vertical", 0);
            animator.SetBool("DirectionRight", lookX > 0 ? true : false);
        }
        else
        {
            animator.SetFloat("Vertical", lookY > 0 ? 1 : -1);
            animator.SetBool("LookingX", false);
            animator.SetFloat("Horizontal", 0);
            animator.SetBool("DirectionUp", lookY > 0 ? true : false);
        }

        if (isMoving) animator.SetFloat("Speed", movement.sqrMagnitude);
        else animator.SetFloat("Speed", 0f);

        if (movement.y != 0)
        {
            
            
        }

        if (movement.x != 0)
        {
            
            
        }
    }

    private bool TryMove(Vector2 movement)
    {
        bool didMove = false;

        if (movement != Vector2.zero)
        {
            //int count = rb.Cast(movement, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

            //if (count == 0)
            //{
            //    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}


            if (IsHit(false, movement, physicalLayers).collider == null)
            {
                transform.Translate(0, moveSpeed * movement.y * Time.deltaTime, 0);
                didMove = true;
            }

            if (IsHit(true, movement, physicalLayers).collider == null)
            {
                transform.Translate(moveSpeed * movement.x * Time.deltaTime, 0, 0);
                didMove = true;
            }
        }

        return didMove;
    }

    void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>();
    }

    public void OnFire(InputValue input)
    {
        //animator.SetTrigger("BowAttack");
        //Transform arrowTransform = Instantiate(pfArrow, rb.position + movementInput, Quaternion.identity);
        //Vector3 shootDir = direction.normalized;
        //arrowTransform.GetComponent<Arrow>().Setup(shootDir);
    }

    public void OnInteract()
    {
        RaycastHit2D raycast = Physics2D.Raycast(new Vector2(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y), direction, interactDistance);

        if (raycast.collider != null)
        {
            if (raycast.collider.name == "Interaction")
            {
                Interactable interactable = raycast.collider.GetComponent<Interactable>();

                interactable.OnInteract();
            }
        }
    }

    private RaycastHit2D IsHit(bool isHorizontal, Vector2 direction, params string[] layerNames)
    {
        if (isHorizontal)
        {
            return Physics2D
            .BoxCast(new Vector2(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y),
            boxCollider.size,
            0,
            new Vector2(direction.x, 0),
            Mathf.Abs(moveSpeed * direction.x * Time.deltaTime),
            LayerMask.GetMask(layerNames));
        }
        else
        {
            return Physics2D
            .BoxCast(new Vector2(transform.position.x + boxCollider.offset.x, transform.position.y + boxCollider.offset.y),
            boxCollider.size,
            0,
            new Vector2(0, direction.y),
            Mathf.Abs(moveSpeed * direction.y * Time.deltaTime),
            LayerMask.GetMask(layerNames));
        }
    }
}
