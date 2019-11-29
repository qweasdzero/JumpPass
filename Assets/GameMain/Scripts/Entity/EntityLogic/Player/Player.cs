using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class Player : Entity
    {
        private PlayerData m_PlayerData;

        private Indicator m_Indicator;

        private Animator m_Anim;

        public Vector3 Position
        {
            get { return m_PlayerData.Position; }
            set { m_PlayerData.Position = value; }
        }

        private MapGrid m_MapGrid;

        private float JumpTime;
        private const float Jump = 0.2F;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_Anim = transform.Find("Body").GetComponent<Animator>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_PlayerData = (PlayerData) userData;
            if (m_PlayerData == null)
            {
                Log.Info("Data is invail");
                return;
            }

            JumpTime = 0;
            GameEntry.Entity.ShowIndicator(new IndicatorData(GameEntry.Entity.GenerateSerialId(), 30, Id));

            GameEntry.Event.Subscribe(StartJumpEventArgs.EventId, OnStartJump);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            GameEntry.Event.Unsubscribe(StartJumpEventArgs.EventId, OnStartJump);
        }

        private void OnStartJump(object sender, GameEventArgs e)
        {
            StartJumpEventArgs ne = e as StartJumpEventArgs;
            if (ne == null)
            {
                return;
            }

            if (m_PlayerData.Stop)
            {
                m_MapGrid.Leave();
                m_PlayerData.Stop = false;
                m_Indicator.SetActive(false);
                m_PlayerData.SpeedY = 4f * ne.Power * Mathf.Sin(m_Indicator.Rotation.z * Mathf.Deg2Rad);
                m_PlayerData.SpeedX = 2.5f * ne.Power * Mathf.Cos(m_Indicator.Rotation.z * Mathf.Deg2Rad);
                m_Anim.SetBool("Prepare", true);
            }
        }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);

            if (childEntity is Indicator)
            {
                m_Indicator = (Indicator) childEntity;
                return;
            }
        }

        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
            if (childEntity is Indicator)
            {
                m_Indicator = null;
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (!m_PlayerData.Stop)
            {
                JumpTime += realElapseSeconds;
                m_PlayerData.SpeedY -=2* elapseSeconds;
                m_PlayerData.Position = m_PlayerData.Position + elapseSeconds * new Vector3(m_PlayerData.SpeedX,
                                            m_PlayerData.SpeedY);
                ShootRay();
            }

            GameEntry.Scene.MainCamera.transform.position = new Vector3(m_PlayerData.Position.x, 0, -7);
            if (!m_PlayerData.Die && m_PlayerData.Position.y < -4)
            {
                GameEntry.Event.Fire(this, ReferencePool.Acquire<GameOverEventArgs>().Fill());
                m_PlayerData.SpeedX = 0;
                m_PlayerData.SpeedY = 0;
                m_PlayerData.Die = true;
            }
        }

        private void ShootRay()
        {
            if (JumpTime > Jump)
            {
                RaycastHit2D info = Physics2D.Raycast(transform.position, Vector2.down, 0.2f);
                if (info)
                {
                    m_PlayerData.SpeedX = 0;
                    m_PlayerData.SpeedY = 0;
                    m_PlayerData.Stop = true;
                    m_Anim.SetBool("Prepare", false);
                    MapGrid mapGrid = info.collider.gameObject.GetComponent<MapGrid>();
                    JumpTime = 0;
                    if (mapGrid != null)
                    {
                        m_MapGrid = mapGrid;
                        m_MapGrid.BeJump(this);
                    }

                    if (m_Indicator)
                    {
                        m_Indicator.SetActive(true);
                        m_Indicator.Rotation = Vector3.zero;
                    }
                }

                if (Physics2D.Raycast(transform.position, Vector3.right, 0.2f))
                {
                    m_PlayerData.SpeedX = 0;
                }
                else if (Physics2D.Raycast(transform.position, Vector3.left, 0.2f))
                {
                    m_PlayerData.SpeedX = 0;
                }
                else if (m_PlayerData.SpeedY > 0 && Physics2D.Raycast(transform.position, Vector3.up, 0.2f))
                {
                    m_PlayerData.SpeedY = 0;
                }
            }
        }
    }
}