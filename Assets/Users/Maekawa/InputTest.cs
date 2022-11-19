using UnityEngine;

public class InputTest : MonoBehaviour
{
    public bool P1 = false;
    public bool P2 = false;
    public TestInputType inputType = TestInputType.down;
    public enum TestInputType
    {
        down,
        up,
        hold,
        Lstick,
        Rstick
    }

    public void Update()
    {
        if (!P1)
            goto P2;

        if (inputType == TestInputType.down)
        {
            if (InputProvider.Instance.GetFireDown1P(InputProvider.FireType.Circle))
                Debug.Log("1P_circleDown");
            if (InputProvider.Instance.GetFireDown1P(InputProvider.FireType.Cross))
                Debug.Log("1P_crossDown");
            if (InputProvider.Instance.GetFireDown1P(InputProvider.FireType.Square))
                Debug.Log("1P_squareDown");
            if (InputProvider.Instance.GetFireDown1P(InputProvider.FireType.Triangle))
                Debug.Log("1P_triangleDown");
        }
        if(inputType == TestInputType.up)
        {
            if (InputProvider.Instance.GetFireUp1P(InputProvider.FireType.Circle))
                Debug.Log("1P_circleUp");
            if (InputProvider.Instance.GetFireUp1P(InputProvider.FireType.Cross))
                Debug.Log("1P_crossUp");
            if (InputProvider.Instance.GetFireUp1P(InputProvider.FireType.Square))
                Debug.Log("1P_squareUp");
            if (InputProvider.Instance.GetFireUp1P(InputProvider.FireType.Triangle))
                Debug.Log("1P_triangleUp");
        }
        if(inputType == TestInputType.hold)
        {
            if (InputProvider.Instance.GetFire1P(InputProvider.FireType.Circle))
                Debug.Log("1P_circleHold");
            if (InputProvider.Instance.GetFire1P(InputProvider.FireType.Cross))
                Debug.Log("1P_crossHold");
            if (InputProvider.Instance.GetFire1P(InputProvider.FireType.Square))
                Debug.Log("1P_squareHold");
            if (InputProvider.Instance.GetFire1P(InputProvider.FireType.Triangle))
                Debug.Log("1P_triangleHold");
        }

        if (inputType == TestInputType.Lstick)
            Debug.Log("1PLstick_" + InputProvider.Instance.GetLeftStick1P());
        if (inputType == TestInputType.Rstick)
            Debug.Log("1PRstick_" + InputProvider.Instance.GetRightStick1P());
        P2:
        if(!P2)
            return;

        if (inputType == TestInputType.down)
        {
            if (InputProvider.Instance.GetFireDown2P(InputProvider.FireType.Circle))
                Debug.Log("2P_circleDown");
            if (InputProvider.Instance.GetFireDown2P(InputProvider.FireType.Cross))
                Debug.Log("2P_crossDown");
            if (InputProvider.Instance.GetFireDown2P(InputProvider.FireType.Square))
                Debug.Log("2P_squareDown");
            if (InputProvider.Instance.GetFireDown2P(InputProvider.FireType.Triangle))
                Debug.Log("2P_triangleDown");
        }
        if (inputType == TestInputType.up)
        {
            if (InputProvider.Instance.GetFireUp2P(InputProvider.FireType.Circle))
                Debug.Log("2P_circleUp");
            if (InputProvider.Instance.GetFireUp2P(InputProvider.FireType.Cross))
                Debug.Log("2P_crossUp");
            if (InputProvider.Instance.GetFireUp2P(InputProvider.FireType.Square))
                Debug.Log("2P_squareUp");
            if (InputProvider.Instance.GetFireUp2P(InputProvider.FireType.Triangle))
                Debug.Log("2P_triangleUp");
        }
        if (inputType == TestInputType.hold)
        {
            if (InputProvider.Instance.GetFire2P(InputProvider.FireType.Circle))
                Debug.Log("2P_circleHold");
            if (InputProvider.Instance.GetFire2P(InputProvider.FireType.Cross))
                Debug.Log("2P_crossHold");
            if (InputProvider.Instance.GetFire2P(InputProvider.FireType.Square))
                Debug.Log("2P_squareHold");
            if (InputProvider.Instance.GetFire2P(InputProvider.FireType.Triangle))
                Debug.Log("2P_triangleHold");
        }

        if (inputType == TestInputType.Lstick)
            Debug.Log("2PLstick_" + InputProvider.Instance.GetLeftStick2P());
        if (inputType == TestInputType.Rstick)
            Debug.Log("2PRstick_" + InputProvider.Instance.GetRightStick2P());
    }
}