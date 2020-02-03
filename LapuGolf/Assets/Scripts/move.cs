using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    public float thrust = 1.0f;
    public float max_thrust = 10.0f;
    public float speed = 1.0f;
    public float jumpspeed = 10;
    public Object bomb;
    public Text coinCountText;
    public Text bombCountText;
    public Text coinCountText02;
    public Text heartCountText02;
    public GameObject heart02;
    public GameObject heart03;

    private Rigidbody rb;
    private LineRenderer dirline;
    private int bombCount = 99;
    private int coinCount = 0;
    private int heartCount = 1;

    private Vector3 direction;
    private Vector3 pos;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        dirline = GetComponent<LineRenderer>();
    }


    void FixedUpdate()
    {

        if (Input.GetKey("w"))
            rb.AddForce(Camera.main.transform.forward * speed * 100);

        if (Input.GetKey("s"))
            rb.AddForce(-1 * Camera.main.transform.forward * speed * 100);

        if (Input.GetKey("a"))
            rb.AddForce(-1 * Camera.main.transform.right * speed * 100);

        if (Input.GetKey("d"))
            rb.AddForce(Camera.main.transform.right * speed * 100);

        if (Input.GetKey("space")) {
            Debug.Log("awsl");
            rb.AddForce(Vector3.up * jumpspeed * 100000);
        }

        shootSelf();
        shootBomb();
    }

    private void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name.Contains("bomb"))
        {
            Destroy(col.gameObject);
            bombCount++;
            bombCountText.text = bombCount.ToString();
        }

        if (col.gameObject.name.Contains("coin"))
        {
            Destroy(col.gameObject);
            coinCount++;
            coinCountText.text = coinCount.ToString();
            coinCountText02.text = coinCount.ToString();
        }

        if (col.gameObject.name.Contains("heart"))
        {
            Destroy(col.gameObject);
            heartCount++;
            if (heartCount == 2)
                heart02.SetActive(true);
            if (heartCount == 3)
                heart03.SetActive(true);
            heartCountText02.text = heartCount.ToString();
        }
    }

    //预测路线，显示预测线，有投掷上限
    void shootBomb()
    {
        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.name == "Ground" || hit.rigidbody.name.Contains("platform"))
                {

                    if (thrust <= max_thrust)
                    {
                        thrust++;
                    }
                    else thrust = max_thrust;

                    dirline.positionCount = 2;
                    dirline.SetPosition(0, rb.transform.position);
                    dirline.SetPosition(1, hit.point);
                    pos = hit.point;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            direction = pos - rb.transform.position;
            direction.y = 2;
            dirline.positionCount = 0;

            Vector3 newpos = rb.transform.position;
            newpos.x += 1;
            newpos.z += 1;

            if (bombCount > 0)
            {
                GameObject bo = Instantiate(bomb, newpos, Quaternion.identity) as GameObject;
                bo.name = "bobobo";
                //rb.AddForce(direction * thrust * 200);
                bo.GetComponent<Rigidbody>().AddForce(direction * thrust * 200);

                thrust = 0;

                bombCount--;
                bombCountText.text = bombCount.ToString();
            }
            else
            {
                Debug.Log("No Bombs!");
            }
        }
    }

    void shootSelf()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody.name == "Ground")
                {

                    if (thrust <= max_thrust)
                    {
                        thrust += 0.2f;
                    }
                    else thrust = max_thrust;

                    dirline.positionCount = 2;
                    dirline.SetPosition(0, rb.transform.position);
                    dirline.SetPosition(1, hit.point);
                    pos = hit.point;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {

            direction = pos - rb.transform.position;
            dirline.positionCount = 0;

            Vector3 newpos = rb.transform.position;
            newpos.y += 1;

            GameObject bo = Instantiate(bomb, newpos, Quaternion.identity) as GameObject;

            rb.AddForce(direction * thrust * 100);
            //bo.GetComponent<Rigidbody>().AddForce(direction * thrust * 100);

            thrust = 0;

            bombCount--;
            coinCountText.text = coinCount.ToString();
        }

    }

    void isBoom()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.rigidbody != null)
            {


            }
        }
    }
}

