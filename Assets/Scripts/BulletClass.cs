using UnityEngine;

public class BulletClass : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 1.5f;

    void Start()
    {
        //Destroy gamObject นี้ภายในเวลา = lifeTime
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        //เคลื่อนที่ในทิศทางข้างหน้า ด้วย ความเร็ว = speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
