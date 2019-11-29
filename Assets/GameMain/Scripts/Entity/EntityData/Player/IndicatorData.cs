using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    [SelectionBase]
    public class IndicatorData : EntityData
    {
        private int m_FatherId;
        private Vector3 m_MousePos;
        private bool m_StartJump;
        public int FatherId
        {
            get { return m_FatherId; }
            set { m_FatherId = value; }
        }

        public Vector3 MousePos
        {
            get { return m_MousePos; }
            set { m_MousePos = value; }
        }

        public bool StartJump
        {
            get { return m_StartJump; }
            set { m_StartJump = value; }
        }

        public IndicatorData(int entityId, int typeId, int fatherId) : base(entityId, typeId)
        {
            m_FatherId = fatherId;
        }
    }
}