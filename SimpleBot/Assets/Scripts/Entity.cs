using System.Collections;
using UnityEngine;
using System;


namespace SimpleBot.Entities
{
    [Serializable]
    public class Entity : Base.BaseBehaviour, Base.IRunLater
    {
        [SerializeField] [OwnAttribute.ReadOnly] protected Health.HealthBase _health;

        protected Rigidbody _body;
        protected int _id;
        protected bool _isDead;

        public Health.HealthBase Health
        {
            get { return _health; }
        }
        public Rigidbody Body
        {
            get { return _body; }
        }
        public bool IsDead
        {
            get { return _isDead; }
        }
        public int Id
        {
            get { return _id; }
        }


        protected override void Awake()
        {
            base.Awake();

            _id = GameObj.GetInstanceID();
            _body = GetComponent<Rigidbody>();
            _health = GetComponent<Health.HealthBase>();

            if (_health == null)
                _health = SimpleBot.Health.HealthBase.HealthNo.Instance;
            else
            {
                _health.OnHealthMinus += OnHealthMinus;
                _health.OnHealthPlus += OnHealthPlus;
            }
        }


        protected virtual bool CheckDead()
        {
            if (_isDead) return false;
            return _health.IsZero;
        }

        protected virtual void OnHealthPlus() { }
        protected virtual void OnHealthMinus()
        {
            if (CheckDead())
                OnDeath();
        }


        protected virtual void OnDeath()
        {
            _isDead = true;
        }


        #region RunLater
        public void RunLater(Action method, float waitSeconds)
        {
            RunLaterValued(method, waitSeconds);
        }

        public Coroutine RunLaterValued(Action method, float waitSeconds)
        {
            if ((waitSeconds < 0) || (method == null))
                return null;

            return StartCoroutine(RunLaterCoroutine(method, waitSeconds));
        }

        public IEnumerator RunLaterCoroutine(Action method, float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            method();
        }
        #endregion
    }
}
