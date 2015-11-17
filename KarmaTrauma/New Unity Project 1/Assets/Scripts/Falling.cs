using UnityEngine;
using System.Collections;

public class Falling : MonoBehaviour {
    public float speed;
    private bool player_arrived = false;
    private int end = 0;
    private bool check = false;
    private bool fallen = false;

    public GameObject cam;
    public GameObject person1;
    
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start () {
       cam.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        // start falling trigger
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x < 10 && check == false && player_arrived == false)
        {
            player_arrived = true;
            Player.Instance.PlayerCamera.SetActive(false);
            if (cam != null)
                cam.SetActive(true);
            check = true;
            //Player.Instance.PlayerCamera.transform.position = Vector3.Lerp(this.transform.position, transform.position, 0.2f);
        }

        // falling
        if (end != 70 && player_arrived == true)
        {
            transform.Translate(0, -speed, 0);
            end += 1;

        }

        // he has fallen
        if (fallen == false && end == 70)
        {
            //Vector3 targetPosition = target.TransformPoint(Player.Instance.PlayerCamera.transform.position);
            //cam.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            Player.Instance.PlayerCamera.SetActive(true);
            if (cam != null)
                cam.SetActive(false);
            fallen = true;
            person1.transform.position=new Vector3(8, -13, 0);
            //He should also turn around
                
            Player.Instance.PlayerCamera.SetActive(true);
            //Player.Instance.PlayerCamera.transform.position = Vector3.Lerp(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 0.2f);
        }

    }
}
