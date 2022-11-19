using UnityEngine;

interface IInput
{
    bool GetFireDown1P(InputProvider.FireType type);
    bool GetFireUp1P(InputProvider.FireType type);
    bool GetFire1P(InputProvider.FireType type);

    Vector3 GetLeftStick1P();
    Vector3 GetRightStick1P();

    //
    bool GetFireDown2P(InputProvider.FireType type);
    bool GetFireUp2P(InputProvider.FireType type);
    bool GetFire2P(InputProvider.FireType type);

    Vector3 GetLeftStick2P();
    Vector3 GetRightStick2P();
}