using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Seat : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Seat east;
    Seat west;
    Seat south;
    Seat north;


    [SerializeReference] Image statefeedback;

    [SerializeField] Color annoyedColor;
    [SerializeField] Color normalColor;
    [SerializeField] Color focusedColor;

    bool isHoveredOver;

    public Student occupant;



    // Start is called before the first frame update
    void Start()
    {
        GetStudentInChildren();
        InitializeNeighbors();


        ClassroomManager.Instance.RegisterSeat(this);
    }

    void DoStudentEffect()
    {

    }

    public void UpdateState()
    {
        if (occupant == null) return;

        if (occupant.Focused)
        {
            statefeedback.color = focusedColor;
            return;
        }
        if (occupant.Annoyed)
        {
            statefeedback.color = annoyedColor;
            return;
        }
        else
        {
            statefeedback.color = normalColor;
            return;
        }
      
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
        

        

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse is over GameObject.");
        isHoveredOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
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

   




    #endregion

}
