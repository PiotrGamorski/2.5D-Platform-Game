using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] private Transform _targetA;
    [SerializeField] private Transform _targetB;
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _platformDelay = 0.5f;
    #endregion

    private bool _switching = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_switching == false)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _targetB.position, _speed * Time.deltaTime);
            if (this.transform.position == _targetB.position)
                StartCoroutine(SwitchingPlatformRoutine(true));
                //_switching = true;
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _targetA.position, _speed * Time.deltaTime);
            if (this.transform.position == _targetA.position)
                StartCoroutine(SwitchingPlatformRoutine(false));
                //_switching = false;
        }
    }

    private IEnumerator SwitchingPlatformRoutine(bool _switching)
    {
        yield return new WaitForSeconds(_platformDelay);
        this._switching = _switching;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
