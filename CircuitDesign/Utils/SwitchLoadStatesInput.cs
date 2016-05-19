using System;
using System.Collections.Generic;
using System.Text;

namespace CircuitModels
{
    public class SwitchLoadStatesInput
    {
        public bool enable;

        public void set_state(int[] switch_states, int[] load_states)
        {
            _switch_states = clone_array(switch_states);
            _expected_load_states = clone_array(load_states);
            _all_states = clone_array(switch_states, load_states);
        }

        public void set_state(int[] states, int switch_num)
        {
            _switch_states = new int[switch_num];
            copy_array(states, 0, _switch_states, 0, switch_num);

            _expected_load_states = new int[states.Length - switch_num];
            copy_array(states, switch_num, _expected_load_states, 0, states.Length - switch_num);

            _all_states = clone_array(states);
        }

        public int[] get_switch_states()
        {
            return _switch_states;
        }

        public int[] get_expected_load_states()
        {
            return _expected_load_states;
        }

        public int[] get_all_states()
        {
            return _all_states;
        }

        private int[] _switch_states;
        private int[] _expected_load_states;
        private int[] _all_states;

        private int[] clone_array(int[] arr)
        {
            int[] tmp = new int[arr.Length];
            copy_array(arr, 0, tmp, 0, arr.Length);
            return tmp;
        }

        private int[] clone_array(int[] arr1, int[] arr2)
        {
            int[] tmp = new int[arr1.Length + arr2.Length];
            copy_array(arr1, 0, tmp, 0, arr1.Length);
            copy_array(arr2, 0, tmp, arr1.Length, arr2.Length);
            return tmp;
        }

        private void copy_array(int[] from, int from_start, int[] to, int to_start, int len)
        {
            for (int i = 0; i < len; ++i)
            {
                to[i + to_start] = from[i + from_start];
            }
        }
    };
}
