using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessManager
{
    class History
    {
        // private string[] histor = new string[0] { };
        private List<string> history_com = new List<string> { };
        private List<string> history_res = new List<string> { };

        public void add_note(string com, string res)
        {
            history_com.Add(com);
            history_res.Add(res);
        }

        public string get_history(int need_len)
        {
            var res = "";
            need_len /= 2;
            var c_el = history_com.Count;
            if (need_len > c_el)
            {
                need_len = c_el;
            }
            for (int i = c_el - need_len; i < c_el; ++i)
            {
                res += history_com[i] + "\n" + history_res[i] + "\n";
            }
            return res;
        }

        public int get_len()
        {
            return history_com.Count;
        }

        public string get_command_by_id(int id)
        {
            id = history_com.Count - id - 1;
            while (id >= 0)
            {
                if (history_com[id].Length > 16 && history_com[id].Substring(0, 16) == "Hacker console: ")
                {
                    return history_com[id].Substring(16, history_com[id].Length - 16);
                }
                id--;
            }
            return "";
        }
    }
}
