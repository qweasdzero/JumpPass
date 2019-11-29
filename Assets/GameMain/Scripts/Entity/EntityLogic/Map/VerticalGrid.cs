using UnityEngine;
using UnityGameFramework.Runtime;
using DG.Tweening;

namespace StarForce
{
    public class VerticalGrid : MapGrid
    {
        private VerticalGridData m_VerticalGridData;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_VerticalGridData = (VerticalGridData) userData;
            if (m_VerticalGridData == null)
            {
                Log.Info("Data is invail");

                return;
            }

            m_VerticalGridData.Pos = m_VerticalGridData.Position.y;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            float move = realElapseSeconds * Vector3.up.y * (m_VerticalGridData.Down ? -1 : 1);
            m_VerticalGridData.Position += new Vector3(0, move);

            if (m_VerticalGridData.Position.y - m_VerticalGridData.Pos > 0.5)
            {
                m_VerticalGridData.Down = true;
            }

            if (m_VerticalGridData.Position.y - m_VerticalGridData.Pos < -0.5)
            {
                m_VerticalGridData.Down = false;
            }
            
            if (m_VerticalGridData.Player != null)
            {
                m_VerticalGridData.Player.Position = new Vector3(m_VerticalGridData.Player.Position.x, m_VerticalGridData.Position.y + 0.3F);
            }
        }
    }
}