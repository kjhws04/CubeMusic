using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform thePlayer = null;
    [SerializeField]
    float followSpeed = 15;

    float hitDistance = 0;
    [SerializeField]
    float zoomDistance = -1.25f;

    Vector3 playerDistance = new Vector3();

    private void Start()
    {
        playerDistance = transform.position - thePlayer.position;
    }

    private void Update()
    {
        Vector3 t_destPos = thePlayer.position + playerDistance + (transform.forward * hitDistance);
        transform.position = Vector3.Lerp(transform.position, t_destPos, followSpeed * Time.deltaTime);
    }

    public IEnumerator ZommCamCoroutine()
    {
        hitDistance = zoomDistance;

        yield return new WaitForSeconds(0.15f);

        hitDistance = 0;
    }
}
