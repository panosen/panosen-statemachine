using System;
using System.Collections.Generic;
using System.Text;

using Panosen.Collections.Generic;

namespace Panosen.StateMachine.Generic
{
    /// <summary>
    /// 状态机
    /// </summary>
    public class Machine<TState, TAction>
    {
        /// <summary>
        /// 状态字段
        /// </summary>
        public HashSet<TState> StateSet { get; set; }

        /// <summary>
        /// 操作
        /// State(状态) + Action(动作) = State(状态)
        /// </summary>
        public Matrix<TState, TAction, TState> StateActions { get; set; }
    }
}
