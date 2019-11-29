using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class StaticGrid : MapGrid
    {
        private StaticGridData m_StaticGridData;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_StaticGridData = (StaticGridData) userData;
            if (m_StaticGridData == null)
            {
                Log.Info("Data is invail");
               
                return;
            }
        }
    }
}