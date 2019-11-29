using UnityEngine;
using UnityGameFramework.Runtime;
using DG.Tweening;

namespace StarForce
{
    public class Indicator : Entity
    {
        private IndicatorData m_IndicatorData;

        private Player m_Player;


        public Vector3 Rotation
        {
            get { return m_IndicatorData.Rotation.eulerAngles; }
            set { m_IndicatorData.Rotation = Quaternion.Euler(value); }
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_IndicatorData = (IndicatorData) userData;
            if (m_IndicatorData == null)
            {
                Log.Info("Data is invail");
                return;
            }

            m_IndicatorData.Scale = new Vector3(1, 1);
            GameEntry.Entity.AttachEntity(this, m_IndicatorData.FatherId);

            gameObject.SetActive(false);
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
            m_Player = (Player) parentEntity;
            CachedTransform.localPosition = Vector3.zero;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

//            float a = Input.GetAxisRaw("Vertical") * 1f;
            float a = 0;
            if (Input.GetMouseButtonDown(0))
            {
                m_IndicatorData.StartJump = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                m_IndicatorData.StartJump = false;
            }

            if (m_IndicatorData.StartJump)
            {
                if (Input.mousePosition.y - m_IndicatorData.MousePos.y > float.Epsilon)
                {
                    a = 3;
                }

                if (Input.mousePosition.y - m_IndicatorData.MousePos.y < -float.Epsilon)
                {
                    a = -3;
                }

                m_IndicatorData.MousePos = Input.mousePosition;
            }

            Rotation += new Vector3(0, 0, a);
            float z = m_IndicatorData.Rotation.eulerAngles.z;
            if (z > 90 && z < 180)
            {
                Rotation = new Vector3(0, 0, 90);
            }

            if (z < float.Epsilon || z > 180)
            {
                Rotation = new Vector3(0, 0, 0);
            }
        }
    }
}