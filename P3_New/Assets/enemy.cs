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

    private float viewDistance;
    //public GameObject target;
    private float fovSize;
    private FieldOfview fov;
    private waypoint[] waypoints;
    private int currWayInd;
    private int numWaypoints;
    public bool lookOverride;
    public Vector3 lookDir;
    private float speed = 1f;

    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<player>();
        alertSlider = alert;
        fovSize = 50f;
        viewDistance = 1f;
        fov = Instantiate(pfov, null).GetComponent<FieldOfview>();

        waypoints = this.transform.parent.GetComponentsInChildren<waypoint>();
        currWayInd = 0;
        numWaypoints = waypoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (numWaypoints > 0)
        {
            Vector3 currWayPos = waypoints[currWayInd].transform.position;

            if (Vector3.Distance(transform.position, currWayPos) < .01f)
            {
                currWayInd++;
                if (currWayInd == numWaypoints) currWayInd = 0;
            }

            this.transform.position = Vector3.MoveTowards(transform.position, currWayPos, speed * Time.deltaTime);
            dir = (currWayPos - transform.position).normalized;
        }
        if(numWaypoints == 0 || lookOverride)
        {
            dir = (lookDir - Vector3.zero).normalized;
        }
        
        fov.SetOrigin(transform.position);
        fov.SetAimDirection(dir);
        FindTargetPlayer();
    }

    private void FindTargetPlayer()
    {
        float distToPlayer = Vector3.Distance(GetPosition(), player.GetPosition());

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
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }
}
