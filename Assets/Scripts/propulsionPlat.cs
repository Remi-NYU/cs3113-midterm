using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propulsionPlat : MonoBehaviour
{
    public float maxNewGroundSpeed;
    public float newGroundSpeedMagnitude;

    //public float speedReductionEachUpdate;

    void OnCollisionStay2D(Collision2D collision)
    {
        player _player = collision.gameObject.GetComponent<player>();
        if (_player)
        {
            _player.groundspeed = _player.groundspeed * newGroundSpeedMagnitude;
            _player.groundspeed = Mathf.Min(maxNewGroundSpeed, _player.groundspeed);
        }
    }

    private float defaultGroundSpeed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        player _player = collision.gameObject.GetComponent<player>();
        if (_player)
        {
            defaultGroundSpeed = _player.defaultGroundSpeed;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // give player the illusion that they gained momentum, while in fact we're just changing its speed..
        //StartCoroutine(resetSpeedBoostGradient(collision, defaultGroundSpeed));


        // reset speed...
        player _player = collision.gameObject.GetComponent<player>();
        _player.groundspeed = defaultGroundSpeed;
    }

    //IEnumerator resetSpeedBoostGradient(Collision2D collision, float defaultGroundSpeed)
    //{
    //    player p = collision.gameObject.GetComponent<player>();
    //    if (!p) yield return null;

    //    while (p.groundspeed > defaultGroundSpeed)
    //    {
    //        float curGroundSpeed = p.groundspeed - speedReductionEachUpdate;
    //        p.groundspeed = curGroundSpeed;
    //        yield return new WaitForFixedUpdate();
    //    }

    //    p.groundspeed = defaultGroundSpeed;
    //    Debug.Log("finished");
    //}

}
