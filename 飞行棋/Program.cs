using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞行棋
{
    //游戏规则
    //如果玩家a踩到玩家B，玩家B退6格
    //踩到地雷退6格
    //踩到时空隧道进10格
    //踩到幸运轮盘，1 交换位置 2 轰炸对方使其退6格
    //踩到暂停，暂停一回合
    //踩到方块，没事发生
    internal class Program
    {
        //把地图变量声明成静态字段，模拟成全局变量
        static int[] maps = new int[100];
        //声明一个静态数组来存储玩家A跟玩家B的坐标
        static int[] playerpos=new int[2];
        //存储两个玩家的姓名
        static string[] playernames = new string[2];
        //两个玩家的标记
        static bool[] flags = new bool[2];
        static void Main(string[] args)
        {
            gameshow();
            inputname();
            //玩家输入姓名后先清屏
            Console.Clear();//清屏
            gameshow();
            Console.WriteLine("{0}的士兵用A表示", playernames[0]);
            Console.WriteLine("{0}的士兵用B表示", playernames[1]);
            initailmap();
            drawmap();

            //当玩家A和玩家B都没到终点时，游戏进行
            while (playerpos[0] < 99 && playerpos[1] < 99)
            {
                if (flags[0] == false)
                {
                    playgame(0);
                }
                else
                {
                    flags[0] = false;
                }
                if (playerpos[0] >= 99)
                {
                    Console.WriteLine("玩家{0}侥幸赢了玩家{1}", playernames[0], playernames[1]);
                    break;
                }

                if (flags[1] == false)
                {
                    playgame(1);
                }
                else
                {
                    flags[1] = false;
                }
                if (playerpos[1] >= 99)
                {
                    Console.WriteLine("玩家{0}侥幸赢了玩家{1}", playernames[1], playernames[1]);
                    break;

                }
            }
                Console.ReadKey();

        }

        public static void gameshow()//绘制游戏菜单头
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*****************************");
            Console.WriteLine("*****************************");
            Console.WriteLine("*********飞行棋游戏**********");
            Console.WriteLine("*****************************");
            Console.WriteLine("*****************************");
        }

        public static void initailmap()//初始化地图
        {
            int[] luckyturn = {6,23,40,55,69,83 };//幸运轮盘
            int[] landmine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//地雷
            int[] pause = { 9, 27, 60, 93 };//暂停
            int[] timetunnel = { 20, 25, 63, 72, 88, 90 };//时空隧道
            for(int i = 0; i < luckyturn.Length; i++)
            {
                maps[luckyturn[i]] = 1;
            }
            for (int i = 0; i < landmine.Length; i++)
            {
                maps[landmine[i]] = 2;
            }
            for (int i = 0; i < pause.Length; i++)
            {
                maps[pause[i]] = 3;
            }
            for (int i = 0; i < timetunnel.Length; i++)
            {
                maps[timetunnel[i]] = 4;
            }

        }

        public static void drawmap()//绘制地图
        {
            //绘制注释
            Console.WriteLine("图例：幸运轮盘：@    地雷：#     暂停：￥    时空隧道：&");

            //第一横行
            for(int i = 0; i < 30; i++)
            {
                Console.Write(drawstringmap(i));

            }

            //换行
            Console.WriteLine();

            //第一竖行
            for(int i = 30; i < 35; i++)
            {
                for(int j = 0; j <=28; j++)
                {
                    Console.Write(" ");
                }
                Console.Write(drawstringmap(i));
                Console.WriteLine();

            }
            
            //第二竖行
            for(int i = 64; i >= 35; i--)
            {
                Console.Write(drawstringmap(i));
            }
            //换行
            Console.WriteLine();
            
            //第二竖行
            for(int i = 65; i <= 69; i++)
            {
                Console.WriteLine(drawstringmap(i));
              
            }

            //第三横行
            for(int i = 70; i <= 99; i++)
            {
                Console.Write(drawstringmap(i));
            }

            //画完最后一行应该换行
            Console.WriteLine();
        }

        public static string drawstringmap(int i)//将绘制地图的重复步骤封装成方法
        {
            string str = "";
            //如果玩家A，B位置相同，并且都在地图上，画一个尖括号
            if (playerpos[0] == playerpos[1] && playerpos[0] == i)
            {
                str="!";
            }
            else if (playerpos[0] == i)
            {
                str="A";
            }
            else if (playerpos[1] == i)
            {
                str="B";
            }
            else
            {
                switch (maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str="@";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str = "#";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        str = "$";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str = "&";
                        break;

                }
            }
            return str;
        }
    
        public static void inputname()//输入玩家姓名
        {
            Console.WriteLine("请输入玩家A的姓名");
            playernames[0] = Console.ReadLine();
            while (playernames[0] == "")
            {
                Console.WriteLine("玩家A的姓名不能为空，请重新输入");
                playernames[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家B的姓名");
            playernames[1] = Console.ReadLine();
            while (playernames[1] == playernames[0] || playernames[1] == "")
            {
                if (playernames[1] == playernames[0])
                {
                    Console.WriteLine("玩家B的姓名不能与玩家A姓名相同，请重新输入");
                    playernames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("玩家B的姓名不能为空，请重新输入");
                    playernames[1] = Console.ReadLine();

                }
            }
        }

        public static void playgame(int playernumber)//玩家进行游戏
        {
            Random r = new Random();
            int rnumber = r.Next(1, 7);//生成1到6的随机数

            Console.WriteLine("{0}按任意键开始掷骰子", playernames[playernumber]);
            Console.ReadKey(true);//添加参数true表示不显示玩家按的键，默认时false
            Console.WriteLine("{0}掷出了{1}", playernames[playernumber],rnumber);
            playerpos[playernumber] += rnumber;
            Console.ReadKey(true);
            //玩家A有可能踩到玩家B，方块，幸运轮盘，地雷，暂停，时空隧道
            if (playerpos[playernumber] == playerpos[1- playernumber])//踩到对方玩家
            {
                Console.WriteLine("玩家{0}踩到玩家{1},玩家{2}退六格", playernames[playernumber], playernames[1- playernumber], playernames[1- playernumber]);
                playerpos[1- playernumber] -= 6;
                Console.ReadKey(true);
            }
            else//踩到关卡
            {
                switch (maps[playerpos[playernumber]])
                {
                    case 0:
                        Console.WriteLine("{0}踩到方块，安全", playernames[playernumber]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("{0}踩到幸运轮盘，请选择：1--交换位置  2--轰炸对方", playernames[playernumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                int temp = playerpos[playernumber];
                                playerpos[playernumber] = playerpos[1- playernumber];
                                playerpos[1- playernumber] = temp;
                                Console.WriteLine("交换位置成功");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                playerpos[1- playernumber] -= 6;
                                Console.WriteLine("轰炸成功，{0}退6格", playernames[1- playernumber]);
                                Console.ReadKey(true);
                                break;

                            }
                            else
                            {
                                Console.WriteLine("只能输入1或者2");
                                input = Console.ReadLine();
                            }

                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷，退6格", playernames[playernumber]);
                        playerpos[playernumber] -= 6;
                        Console.ReadKey(true);
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", playernames[playernumber]);
                        flags[playernumber] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", playernames[playernumber]);
                        playerpos[playernumber] += 10;
                        Console.ReadKey(true);
                        break;
                }

            }
            changepos();
            Console.Clear();
            drawmap();
        }

        public static void changepos()//当玩家坐标改变判断是否超出地图
        {
            if (playerpos[0] < 0)
            {
                playerpos[0] = 0;
            }
            if (playerpos[0] >= 99)
            {
                playerpos[0] = 99;
            }
            if (playerpos[1] < 0)
            {
                playerpos[1] = 0;
            }
            if (playerpos[1] >= 99)
            { 
                playerpos[1] = 99;
            }
        }
    }

}
