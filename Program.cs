using System;
using System.Threading;

namespace ProcessManager
{
    class Program
    { 
        static void Main(string[] args)
        {
            var com = "";
            var proc_man = new ProcessManager();
            ConsoleKeyInfo key;
            var key_name_lower = "";
            // var history = "";
            var prog = new Program();
            int posX, posY = 0;
            var res = "";
            var proc_id = 0;
            var proc_name = "";
            var cur_com = "";
            var id_cur_com = 0;
            var history = new History();
            while (true)
            {
                //for (int i = 0; i < 10; ++i)
                //{
                //    var t = i.ToString();
                //    r += t.PadRight(Console.WindowWidth - t.Length + 1);
                // Console.WriteLine(t);
                //}
                // Console.Write("\r" + r);

                // Console.Clear();

                Console.SetCursorPosition(0, 0);

                Console.WriteLine(proc_man.TrackList());

                posX = Console.CursorLeft; posY = Console.CursorTop - 1;

                // Console.WriteLine("".PadRight(2000));
                for (int i = 0; i < 5 + (10 < history.get_len() * 2 ? 10 : history.get_len() * 2); ++i)
                {
                    prog.ClearLine(posY + i);
                }

                Console.SetCursorPosition(posX, posY + 1);

                Console.WriteLine(history.get_history(10));

                // Console.WriteLine($"\r& {com}");

                posY = Console.CursorTop;

                Console.SetCursorPosition(posX, posY - 1);
                while (Console.KeyAvailable)
                {
                    posX = Console.CursorLeft; posY = Console.CursorTop;

                    key = Console.ReadKey();
                    key_name_lower = key.Key.ToString().ToLower();
                    // Console.Write($"\rUser press key: {key_name_lower, -30}");
                    if (key_name_lower.Length == 2 && key_name_lower.Substring(0, 1) == "d")
                    {
                        key_name_lower = key_name_lower[key_name_lower.Length - 1].ToString();
                    } else if (key_name_lower.Length > 3 && key_name_lower.Substring(0, 3) == "oem")
                    {
                        key_name_lower = key_name_lower.Substring(3, key_name_lower.Length - 3);
                    } 
                    if (key_name_lower.Length == 1) {
                        com += key_name_lower;
                        cur_com = com;
                    } else
                    {
                        if (key_name_lower == "backspace" && com.Length > 0)
                        {
                            com = com.Substring(0, com.Length - 1);
                            cur_com = com;
                        } 
                        else if (key_name_lower == "escape")
                        {
                            System.Environment.Exit(1);
                        }
                        else if (key_name_lower == "spacebar")
                        {
                            com += " ";
                            cur_com = com;
                        } 
                        else if (key_name_lower == "minus")
                        {
                            com += "-";
                            cur_com = com;
                        }
                        else if (key_name_lower == "plus")
                        {
                            com += "+";
                            cur_com = com;
                        }
                        else if (key_name_lower == "period")
                        {
                            com += ".";
                            cur_com = com;
                        }
                        else if (key_name_lower == "uparrow" && id_cur_com < history.get_len())
                        {
                            id_cur_com++;
                            com = history.get_command_by_id(id_cur_com - 1);
                        } 
                        else if (key_name_lower == "downarrow" && id_cur_com > 0)
                        {
                            id_cur_com--;
                            if (id_cur_com == 0)
                            {
                                com = cur_com;
                            } else
                            {
                                com = history.get_command_by_id(id_cur_com - 1);
                            }
                        }
                        else if (key_name_lower == "enter")
                        {
                            id_cur_com = 0;
                            if (com.Length == 4 && com == "help")
                            {
                                history.add_note($"Hacker console: {com}", "kill process by id {process_id} - kill process by his id");
                                history.add_note("kill process by name {process_name} - kill process by his name", "process id {process_name} - return process id by his name");
                                history.add_note("process name {process_id} - return process name by his id", "help - show this menu");
                                com = "";
                                continue;
                            }
                            else if (com.Length > 18 && com.Substring(0, 18) == "kill process by id")
                            {
                                var s_r = com.Split();

                                if (s_r.Length == 5 && int.TryParse(s_r[4], out proc_id))
                                {
                                    res = proc_man.KillById(proc_id);
                                }
                                else
                                {
                                    res = $"Unknow process id: {com.Substring(18, com.Length - 18)}";
                                }
                            }
                            else if (com.Length > 20 && com.Substring(0, 20) == "kill process by name")
                            {
                                var s_r = com.Split();
                                
                                if (s_r.Length > 4)
                                {
                                    proc_name = "";
                                    for (int i = 4; i < s_r.Length; ++i)
                                    {
                                        proc_name += s_r[i] + (i < s_r.Length - 1 ? " " : "");
                                    }
                                    res = proc_man.KillByName(proc_name);
                                }
                                else
                                {
                                    res = $"Name is required";
                                }
                            }
                            else if (com.Length > 12 && com.Substring(0, 12) == "process name")
                            {
                                var s_r = com.Split();

                                if (s_r.Length == 3 && int.TryParse(s_r[2], out proc_id))
                                {
                                    res = proc_man.ProcessName(proc_id);
                                }
                                else
                                {
                                    res = $"Unknow process id: {com.Substring(12, com.Length - 12)}";
                                }
                            }
                            else if (com.Length > 10 && com.Substring(0, 10) == "process id")
                            {
                                var s_r = com.Split();

                                if (s_r.Length > 2)
                                {
                                    proc_name = "";
                                    for (int i = 2; i < s_r.Length; ++i)
                                    {
                                        proc_name += s_r[i] + (i < s_r.Length - 1 ? " " : "");
                                    }
                                    res = proc_man.ProcessId(proc_name);
                                }
                                else
                                {
                                    res = $"Name is required";
                                }
                            }
                            else
                            {
                                res = $"Unknow command - {com}";
                            }
                            history.add_note($"Hacker console: {com}", res);
                            com = "";
                        }
                        // Console.Write($"\rUser press another key: {key_name_lower, -30}");
                    }
                    // Console.WriteLine($"\r {key.Key,30}");
                    Console.Write($"Hacker console: {com}");
                    Console.SetCursorPosition(posX,  posY);
                    for (int i = 0; i < com.Length / Console.WindowWidth + 1; ++i)
                    {
                        prog.ClearLine(posY + i);
                    }

                }
                Console.Write($"Hacker console: {com}");
                //if (key.Key != ConsoleKey.)
                //{

                //}
                // if (Console.ReadKey() != Console.)
                // Console.Clear();
            }
        }

        void ClearLine(int line)
        {
            Console.MoveBufferArea(0, line, Console.BufferWidth, 1, Console.BufferWidth, line, ' ', Console.ForegroundColor, Console.BackgroundColor);
        }
    }
}
