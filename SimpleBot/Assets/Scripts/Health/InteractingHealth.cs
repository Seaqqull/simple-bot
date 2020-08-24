using UnityEngine.Events;
using UnityEngine;


namespace SimpleBot.Health
{
    public class InteractingHealth : HealthBase
    {
        [System.Serializable]
        public class HealthUpdateEvent : UnityEvent<float> { }


#pragma warning disable CS0414, CS0649
        [SerializeField] private HealthUpdateEvent _onHealthChanged;
        [SerializeField] private int _maxHealth; // By convention min health is 0
        [SerializeField] private int _health;
#pragma warning restore CS0414, CS0649

        public HealthUpdateEvent HealthEvent
        {
            get { return _onHealthChanged; }
        }
        public override int MaxValue
        {
            get { return _maxHealth; }
        }
        public override bool IsZero
        {
            get { return _health == 0; }
        }
        public override int Value
        {
            get { return _health; }
        }


        protected override void Awake()
        {
            if (_health > _maxHealth) _health = _maxHealth;
        }

        protected override void Start()
        {
            base.Start();

            ModifyHealth(0);
        }


        public override void ResetHealth(int amount)
        {
            _health = (amount > _maxHealth) ? _maxHealth : amount;
        }

        public override void ModifyHealth(int amount)
        {
            _health = (_health < amount) ? 0 : _health - amount;

            _onHealthChanged.Invoke(Percent);

            base.ModifyHealth(amount);
        }
    }
}
