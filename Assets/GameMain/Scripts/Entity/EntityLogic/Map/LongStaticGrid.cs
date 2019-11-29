using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class LongStaticGrid : MapGrid
    {
        private LongStaticGridData m_LongStaticGridData;

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_LongStaticGridData = (LongStaticGridData) userData;
            if (m_LongStaticGridData == null)
            {
                Log.Info("Data is invail");
               
                return;
            }
        }
    }
}