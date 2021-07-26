using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Panosen.Collections.Generic;

namespace Savory.StateMachine.Generic
{
    /// <summary>
    /// 状态机扩展方法
    /// </summary>
    public static class MachineExtension
    {
        /// <summary>
        /// 添加状态
        /// </summary>
        public static void AddState<TState, TActionKey>(this Machine<TState, TActionKey> machine, TState state)
        {
            if (machine.StateSet == null)
            {
                machine.StateSet = new HashSet<TState>();
            }
            if (machine.StateSet.Contains(state))
            {
                return;
            }

            machine.StateSet.Add(state);
        }

        /// <summary>
        /// 添加行为
        /// </summary>
        public static void AddStateAction<TState, TAction>(this Machine<TState, TAction> machine, TState fromStateKey, TAction actionKey, TState toStateKey)
        {
            if (machine.StateActions == null)
            {
                machine.StateActions = new Matrix<TState, TAction, TState>();
            }
            machine.StateActions.Add(fromStateKey, actionKey, toStateKey);
        }

        /// <summary>
        /// State(状态) + Action(动作) = State(状态)
        /// </summary>
        public static TState ComputeState<TState, TAction>(this Machine<TState, TAction> machine, TState state, TAction action)
        {
            if (machine.StateActions == null)
            {
                return default;
            }
            if (machine.StateActions.ContainsKey(state, action))
            {
                return machine.StateActions.GetValue(state, action);
            }

            return default;
        }

        /// <summary>
        /// 获取当前状态的所有可能的下一个状态
        /// </summary>
        public static HashSet<TState> NextStates<TState, TAction>(this Machine<TState, TAction> machine, TState state)
        {
            if (machine.StateActions == null)
            {
                return null;
            }

            var values = machine.StateActions.GetValues(state);
            if (values == null)
            {
                return null;
            }

            return new HashSet<TState>(values);
        }
    }
}
