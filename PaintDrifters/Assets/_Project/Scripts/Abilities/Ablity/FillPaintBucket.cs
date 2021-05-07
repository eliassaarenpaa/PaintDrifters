using UnityEngine;

[CreateAssetMenu(menuName = "Fill Paint Bucket")]
public class FillPaintBucket : Ability
{

    public override void ActivateAbility(CarController controller)
    {
        controller.gameObject.GetComponent<PaintInteractor>().ReplenishPaint();
    }

}
