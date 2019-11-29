using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    [SelectionBase]
    public class VerticalGridData : MapGridData
    {
        public override int m_Score
        {
            get { return 40; }
        }
        
        private float m_Pos;

        private bool m_Down;

        public bool Down
        {
            get { return m_Down; }
            set { m_Down = value; }
        }

        public float Pos
        {
            get { return m_Pos; }
            set { m_Pos = value; }
        }

        public VerticalGridData(int entityId, int typeId) : base(entityId, typeId)
        {
        }
    }
}