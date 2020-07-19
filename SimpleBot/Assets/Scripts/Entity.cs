using System.Collections;
using UnityEngine;
using System;


namespace SimpleBot.Entities
{
    [Serializable]
    public class Entity : Base.BaseBehaviour, Base.IRunLater
    {

        protected Rigidbody _body;
        protected int _id;
        protected bool _isDead;

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
