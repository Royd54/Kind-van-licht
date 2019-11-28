using UnityEngine;
using System.Collections;

public class playerSpirit : MonoBehaviour
{
    [SerializeField] private GameObject igniculusLight1;
    [SerializeField] private GameObject igniculusLight2;

    public float distance = 10.0f;
    public bool useInitalCameraDistance = false;

    private float actualDistance;

    // Use this for initialization
    void Start()
    {
        if (useInitalCameraDistance)
        {
            Vector3 toObjectVector = transform.position - Camera.main.transform.position;
            Vector3 linearDistanceVector = Vector3.Project(toObjectVector, Camera.main.transform.forward);
            actualDistance = linearDistanceVector.magnitude;
        }
        else
        {
            actualDistance = distance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 22;
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

        if (Input.GetMouseButton(1)){
            igniculusLight2.SetActive(true);
        }
        else
        {
            igniculusLight2.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (Input.GetMouseButton(1) && collision.gameObject.tag == "plant")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerCombat>().addRecourses(Random.Range(2, 5), Random.Range(1, 3));
        }
    }

}
