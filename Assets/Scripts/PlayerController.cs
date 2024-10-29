using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    Gyroscope _gyro;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _rotationRangeVector3;

    [Header("��]�̕���ݒ�(�x��)")]
    [SerializeField] private float _xAxisRotationRange;
    [SerializeField] private float _yAxisRotationRange;

    [Header("�f�o�b�O�p�@�{�Ԋ��ł͏����Ă�������")]
    [SerializeField] private TextMeshProUGUI _gyroText;
    [SerializeField] private TextMeshProUGUI _objectText;

    Quaternion _defaultAttitude;

    void Start()
    {
        _gyro = Input.gyro;
        _gyro.enabled = true;

        //�[���̃W���C���̏����ʒu���擾
        _defaultAttitude = _gyro.attitude;
    }

    void Update()
    {
        Debug.Log("Gyro rotation rate" + _gyro.rotationRate + "/Gyro attitude" + _gyro.attitude + "/Gyro enabled :" + _gyro.enabled);
        _gyroText.text = "Gyro rotation rate" + _gyro.rotationRate + "/Gyro attitude" + _gyro.attitude + "/Gyro enabled :" + _gyro.enabled;

        //�J�n���̃W���C���̐��l�Ƃ̍��������߂�
        var attitudeX = _gyro.attitude.x - _defaultAttitude.x;
        var attitudeY = _gyro.attitude.y - _defaultAttitude.y;

        //X����]�i���i�́A�㉺��]�j
        transform.rotation *= Quaternion.AngleAxis(attitudeY * _rotationRangeVector3.x, Vector3.right);

        //Y,Z����]�i��̉�]�A�i�ތ�����ς���j
        if (attitudeX < _yAxisRotationRange || attitudeX > -_yAxisRotationRange)
        {
            transform.rotation *= Quaternion.AngleAxis(attitudeX * _rotationRangeVector3.y, Vector3.down);
            transform.rotation *= Quaternion.AngleAxis(attitudeX * _rotationRangeVector3.z, Vector3.back);
        }
        else
        {
            transform.rotation *= Quaternion.AngleAxis(_yAxisRotationRange * _rotationRangeVector3.y, Vector3.down);
            transform.rotation *= Quaternion.AngleAxis(_yAxisRotationRange * _rotationRangeVector3.z, Vector3.back);
        }

        //�㏸����Ɛ��i
        if (_gyro.attitude.y < -0.1f)
        {
            _rb.AddForce(transform.forward * _speed);
        }

        _objectText.text = "(x, y, z) = " + transform.eulerAngles + "/default attitude" + (_defaultAttitude);
    }
}
