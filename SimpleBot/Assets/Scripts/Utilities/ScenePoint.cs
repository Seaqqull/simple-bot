using UnityEngine;


namespace SimpleBot.Utility
{
    public abstract class ScenePoint : Base.BaseBehaviour
    {
#pragma warning disable 0649
        [SerializeField] protected float _size = 0.3f;
#pragma warning restore 0649

        protected Color _gizmoDefault;

        public abstract Color Color
        {
            get;
        }


        protected void OnDrawGizmos()
        {
            _gizmoDefault = Gizmos.color;

            Gizmos.color = Color;
            Gizmos.DrawWireSphere(transform.position, _size);

            Gizmos.color = _gizmoDefault;
        }
    }
}
