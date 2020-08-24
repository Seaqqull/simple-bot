using UnityEngine;


namespace SimpleBot.Health
{
    [System.Serializable]
    public abstract class HealthBase : MonoBehaviour
    {
        public class HealthNo : HealthBase
        {
            protected static HealthBase _instance;

            public override bool IsZero => false;
            public override int Value => 0;
            public override int MaxValue => 0;


            public static HealthBase Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        GameObject instance = new GameObject("HealthNo", typeof(HealthNo));
                        instance.transform.SetAsFirstSibling();

                        _instance = instance.GetComponent<HealthNo>();
                    }

                    return _instance;
                }
            }


            public override void ResetHealth(int amount) { }

            public override void ModifyHealth(int amount) { }
        }


        protected System.Action _onHealthMinus;
        protected System.Action _onHealthPlus;


        public event System.Action OnHealthPlus
        {
            add { _onHealthPlus += value; }
            remove { _onHealthPlus -= value; }
        }
        public event System.Action OnHealthMinus
        {
            add { _onHealthMinus += value; }
            remove { _onHealthMinus -= value; }
        }
        public float Percent { get { return 1.0f * Value / MaxValue; } }
        public abstract int MaxValue { get; }
        public abstract bool IsZero { get; }
        public abstract int Value { get; }


        protected virtual void Awake() { }
        protected virtual void Start() { }


        public virtual void ResetHealth()
        {
            ResetHealth(MaxValue);
        }

        public virtual void ModifyHealth(int amount)
        {
            if (amount > 0)
                _onHealthMinus.Invoke();
            else if (amount < 0)
                _onHealthPlus.Invoke();
        }


        public abstract void ResetHealth(int amount);
    }
}
