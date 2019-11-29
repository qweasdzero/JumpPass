using UnityEngine;
using UnityGameFramework.Runtime;
using DG.Tweening;

namespace StarForce
{
    public class HorizontalGrid : MapGrid
    {
        private HorizontalGridData m_HorizontalGridData;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_HorizontalGridData = (HorizontalGridData) userData;
            if (m_HorizontalGridData == null)
            {
                Log.Info("Data is invail");

                return;
            }

            m_HorizontalGridData.Pos = m_HorizontalGridData.Position.x;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            float move = realElapseSeconds * Vector3.right.x * (m_HorizontalGridData.Left ? -1 : 1);

            m_HorizontalGridData.Position += new Vector3(move, 0);
            if (m_HorizontalGridData.Player != null)
            {
                m_HorizontalGridData.Player.Position += new Vector3(move, 0);
            }

            if (m_HorizontalGridData.Position.x - m_HorizontalGridData.Pos > 0.5)
            {
                m_HorizontalGridData.Left = true;
            }

            if (m_HorizontalGridData.Position.x - m_HorizontalGridData.Pos < -0.5)
            {
                m_HorizontalGridData.Left = false;
            }
        }
    }
}