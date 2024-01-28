namespace Gameplay.Gravity
{
    public class GravityReceiver : GravityObject
    {
        public GravityApplier IgnoreGravityApplier { get; set; }

        private void OnEnable()
        {
            GravityController.Instance.AddGravityReceiver(this);
        }

        private void OnDisable()
        {
            GravityController.Instance.RemoveGravityReceiver(this);
        }
    }
}
