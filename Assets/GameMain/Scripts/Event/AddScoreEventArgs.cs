using GameFramework.Event;

namespace StarForce
{
    /// <summary>
    /// 开始跳跃事件。
    /// </summary>
    public sealed class AddScoreEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(AddScoreEventArgs).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }

        private int m_Score;

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public override void Clear()
        {
        }

        public AddScoreEventArgs Fill(int score)
        {
            m_Score = score;
            return this;
        }
    }
}