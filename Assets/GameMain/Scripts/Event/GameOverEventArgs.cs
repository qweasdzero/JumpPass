using GameFramework.Event;

namespace StarForce
{
    /// <summary>
    /// 游戏结束事件。
    /// </summary>
    public sealed class GameOverEventArgs : GameEventArgs
    {
        /// <summary>
        /// 开始游戏事件编号。
        /// </summary>
        public static readonly int EventId = typeof(GameOverEventArgs).GetHashCode();

        /// <summary>
        /// 获取开始游戏事件编号。
        /// </summary>
        public override int Id
        {
            get { return EventId; }
        }

        /// <summary>
        /// 清理开始游戏事件。
        /// </summary>
        public override void Clear()
        {
        }

        /// <summary>
        /// 填充开始游戏事件。
        /// </summary>
        /// <param name="gameMode">游戏模式。</param>
        /// <GameOverEventArgs>开始游戏事件。</returns>
        public GameOverEventArgs Fill()
        {
            return this;
        }
    }
}