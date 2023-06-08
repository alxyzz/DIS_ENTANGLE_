using UnityEngine;

public class Seat : MonoBehaviour
{
    Seat east;
    Seat west;
    Seat south;
    Seat north;

    bool isHoveredOver;

    public Student occupant;



    // Start is called before the first frame update
    void Start()
    {
        GetStudentInChildren();
        InitializeNeighbors();
    }

    void GetStudentInChildren()
    {
        Student[] components = GetComponentsInChildren<Student>();
        if (components.Length >0)
        {
            occupant = components[0];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isHoveredOver)
            {
                OnClick();
            }
        }

        

    }


    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        isHoveredOver = true;
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        isHoveredOver = false;
    }



    void RaycastDirection(Vector3 direction)
    {
        RaycastHit hit;

        // Send a raycast in the specified direction
        if (Physics.Raycast(transform.position, direction, out hit, 55))
        {
            // A collision occurred, handle it
            Debug.DrawRay(transform.position, direction * hit.distance, Color.red);
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
        }
        else
        {
            // No collision, draw the ray as green
            Debug.DrawRay(transform.position, direction * 55, Color.green);
        }
    }

    Seat GetNeighboringSeat(Vector3 direction)
    {
        RaycastHit hit;

        // Send a raycast in the specified direction
        if (Physics.Raycast(transform.position, direction, out hit, 55))
        {
            if (hit.transform.tag == "seat")
            {
                return hit.transform.GetComponent<Seat>();
            }
        }
        return null;
    }

    #region Meth-ods

    void InitializeNeighbors()
    {
        west = GetNeighboringSeat(Vector3.left);
        east = GetNeighboringSeat(Vector3.right);
        north = GetNeighboringSeat(Vector3.forward);
        south = GetNeighboringSeat(Vector3.back);
    }


    public void OnClick()
    {
        Debug.Log("Clicked this.");
        ClassroomManager.Instance.OnClickSeat(this);
    }

    #endregion

}
