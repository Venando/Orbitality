namespace Gameplay.Gravity
{
    public class GravityApplier : GravityObject
    {
        private void OnEnable()
        {
            GravityController.Instance.AddGravityApplier(this);
        }

        private void OnDisable()
        {
            GravityController.Instance.RemoveGravityApplier(this);
        }
    }
}
