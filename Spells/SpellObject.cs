using UnityEngine;
using System.Collections;
using BeheaderTavern.Scripts.Spells;
using BeheaderTavern.Scripts.Mobiles;
using UnityEngine.AI;

public class SpellObject : MonoBehaviour
{
    private SpellBase _spell;
    private SpellProperties _props;
    private float _durationTimer;
    public Rigidbody rb;
    public bool collided;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_props != null)
        {
            if (_durationTimer < _props.SpellDuration)
            {
                _durationTimer += Time.deltaTime;
            }
            if (_durationTimer > _props.SpellDuration || collided)
            {
                _durationTimer = 0;
                collided = false;
                GameObjectPool.Recycle(this.gameObject);
                rb.velocity = Vector3.zero;
                //Destroy(this.gameObject);
            }
        }
    }

    public void SetProps(SpellBase spell)
    {
        _spell = spell;
        _props = spell.SpellProps;
    }


    public void OnTriggerEnter(Collider col)
    {
        _spell.OnTriggerEnter(col);
    }
}

