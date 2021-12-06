using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    [SerializeField] private Transform pfov;
    [SerializeField] private GameManager1 alert;

    private player player;
    private GameManager1 alertSlider;

    public Slider al;

    public RaycastHit2D raycastHit2D;

    private float viewDistance;
    //public GameObject target;
    private float fovSize;
    private FieldOfview fov;
    private waypoint[] waypoints;
    private int currWayInd;
    private int numWaypoints;
    public bool lookOverride;
    public Vector3 lookDir;
    private float speed = 0.5f;
    private int orientation; //0 up, 1 right, 2 down, 3 left
    private bool distracted;
    public Animator animator;
    private enum State
    {
        Staying,
        Moving,
        Looking,
        AttackingPlayer,

    }

    private State state;

    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<player>();
        alertSlider = alert;
        fovSize = 50f;
        viewDistance = 1f;
        fov = Instantiate(pfov, null).GetComponent<FieldOfview>();

        state = State.Staying;

        waypoints = this.transform.parent.GetComponentsInChildren<waypoint>();
        currWayInd = 0;
        numWaypoints = waypoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Staying:
            case State.Moving:
                Moving();
                FindTargetPlayer();
                break;
            case State.Looking:
                LookFor();
                FindTargetPlayer();
                break;
            case State.AttackingPlayer:
                //AttackPlayer();
                break;

        }
    }

    private void Moving()
    {
        fov.SetViewDistance(1f);
        viewDistance = 1f;
        if (numWaypoints > 0)
        {
            Vector3 currWayPos = waypoints[currWayInd].transform.position;

            if (Vector3.Distance(transform.position, currWayPos) < .01f)
            {
                currWayInd++;
                if (currWayInd == numWaypoints) currWayInd = 0;
            }
            if(!distracted)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, currWayPos, speed * Time.deltaTime);
            }
            
            dir = (currWayPos - transform.position).normalized;
        }
        if(numWaypoints == 0 || lookOverride)
        {
            dir = (lookDir - Vector3.zero).normalized;
            animator.SetFloat("Speed", 0.0f);
        }
        
        if(!distracted)
        {
            fov.SetOrigin(transform.position);
            fov.SetAimDirection(dir);
            FindTargetPlayer();
            setOrientation(dir);
        }
        else
        {
            animator.SetFloat("Speed", 0.0f);
        }
        //Debug.Log("OR" + orientation);

    }

    private void FindTargetPlayer()
    {
        /*float distToPlayer = Vector3.Distance(GetPosition(), player.GetPosition());

        if (distToPlayer < viewDistance)
        {
            Vector3 dirToPlayer = (player.GetPosition() - this.GetPosition()).normalized;
            if (Vector3.Angle(dir, dirToPlayer) < fovSize / 2)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance);
                if (raycastHit2D.collider != null && player.visible)
                {
                    al.value += (25 - 5 * distToPlayer) * Time.deltaTime;
                    //alert.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(1, 0, 0, 1);
                    //alert.gameObject.transform.Find("Background").GetComponent<Image>().color = new Color(1, 1, 1, 1);
                }
            }
        }*/

        if (Vector3.Distance(this.GetPosition(), player.GetPosition()) < viewDistance && Vector3.Distance(this.GetPosition(), player.GetPosition()) > viewDistance / 3f)
        {
            Vector3 dirToPlayer = (player.GetPosition() - this.GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fovSize)
            {
                raycastHit2D = Physics2D.Raycast(this.GetPosition(), dirToPlayer, viewDistance);
                if (raycastHit2D.collider != null)
                {
                    if (raycastHit2D.collider.gameObject.GetComponent<player>() != null)
                    {
                        if(raycastHit2D.collider.tag == "Player")
                        {
                            al.value += 55f * Time.deltaTime;
                            fov.SetAimDirection(dirToPlayer);
                            state = State.Looking;
                            

                        }
                        
                    }
                }

            }
        }

        else if (Vector3.Distance(this.GetPosition(), player.GetPosition()) < viewDistance / 3f)
        {
            Vector3 dirToPlayer = (player.GetPosition() - this.GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fovSize)
            {
                raycastHit2D = Physics2D.Raycast(this.GetPosition(), dirToPlayer, viewDistance / 3f);
                if (raycastHit2D.collider != null)
                {
                    if (raycastHit2D.collider.gameObject.GetComponent<player>() != null)
                    {
                        if (raycastHit2D.collider.tag == "Player")
                        {
                            al.value = 100f;
                            fov.SetAimDirection(dirToPlayer);
                            state = State.Looking;
                        }
                    }
                }

            }
        }


        else
        {
            state = State.Moving;
            //StartCoroutine(Wait());

        }
    }

    private void LookFor()
    {

        this.transform.position = this.transform.position;
        fov.SetViewDistance(1.5f);
        viewDistance = 1.5f;

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

    private Vector3 GetAimDir()
    {

        return dir;
    }
    public RaycastHit2D Ret()
    {
        return raycastHit2D;
    }
    public int GetOrientation()
    {
        return orientation;
    }
    //Getting the difference of the x and y of the direction of the
    public void setOrientation(Vector2 direction)
    {
        Vector2 orientationVec = new Vector2(0f, 0f);
        orientationVec.x = (direction.x - this.transform.position.x);
        orientationVec.y = (direction.y - this.transform.position.y);
        if(direction != Vector2.zero)
        {
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        animator.SetFloat("Speed", direction.sqrMagnitude);
        if (direction.x >= 0)
        {
            orientation = 1;
        }
        else
        {
            orientation = 3;
        }
        if (direction.y >= 0)
        {
            if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                orientation = 2;
            }
        }
        else
        {
            if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                orientation = 0;
            }
        }
    }
    public void CauseDistracted(Vector3 distractPos)
    {
        Debug.Log("HEYOO");
        StartCoroutine(BeDistracted(distractPos));
    }
    //Destroy the object
    public void Kod()
    {
        fov.DestroyFOV();
        Debug.Log("KOD");
        Destroy(gameObject);

    }
    //
    IEnumerator BeDistracted(Vector3 distractPos)
    {
        distracted = true;
        dir = (distractPos - transform.position).normalized;
        setOrientation(dir);
        fov.SetAimDirection(dir);
        yield return new WaitForSeconds(5f);
        distracted = false;
    }
}
