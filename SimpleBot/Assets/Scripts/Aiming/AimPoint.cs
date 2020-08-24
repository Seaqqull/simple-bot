using UnityEngine;


namespace SimpleBot.Aiming
{
    public class AimPoint : Utility.ScenePoint
    {
#pragma warning disable 0649
        [SerializeField] private Color _color = Color.black;
#pragma warning restore 0649

        public override Color Color
        {
            get
            {
                return this._color;
            }
        }
    }
}
