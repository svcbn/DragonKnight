using UnityEngine;

public class Test_EnemyDescentAttack : MonoBehaviour
{
    Vector3 dir;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        transform.LookAt(player.transform.position);
        dir = player.transform.position;
        dir += player.transform.forward * 10;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y > 2.5f)
        {
            transform.position = Vector3.Lerp(transform.position, dir, 0.04f);
            transform.LookAt(player.transform.position);
        }
        else if(transform.position.y > 0.5f && transform.position.y < 2.5f)
        {
            transform.position += Vector3.down * 10f * Time.deltaTime;
            
        }
        
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            print("Ãæµ¹");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.LookAt(player.transform.position);
        }
        
    }
}
