using GameFramework.Event;

namespace StarForce
{
    /// <summary>
    /// 生成新地图事件。
    /// </summary>
    public sealed class ShowNewMapEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ShowNewMapEventArgs).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }

        public int Grid;
        public override void Clear()
        {
        }

        public ShowNewMapEventArgs Fill(int grid)
        {
            Grid = grid;
            return this;
        }
    }
}