using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace 贪吃蛇
{
    class palette
    {
        //变量声明部分
        public int score;//得分
        private int width = 20;//宽度
        private int height = 20;//高度
        private Color bgcolor;//背景色
        private Graphics Palette;//画布
        private ArrayList blocks;//蛇体和食物的方块
        private directions direction;//前进方向
        private System.Timers.Timer timerblock;//计时器
        private snake food;//食物
        private int size;//单位大小
        private int level;//游戏等级
        private bool isgameover = false;//是否游戏结束
        private int[] speed = new int[] { 500, 450, 400, 350, 300, 250, 200, 150, 100, 50 };//游戏速度
        //实例化时的初始化
        public palette(int width,int height,int size,Color bgcolor,Graphics g,int level)
        {
            this.width = width;
            this.height = height;
            this.size = size;
            this.bgcolor = bgcolor;
            this.Palette = g;
            this.level = level;
            this.blocks = new ArrayList();
            this.blocks.Insert(0, new snake(Color.Red, this.size, new Point(width / 2, height / 2)));
            this.direction = directions.right;
        }
        //方向属性的get set方法
        public  directions Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }
        //开始游戏
        public void Start()
        {
            //生成一个初始的食物
            this.food = Getfood();
            //初始化计时器
            timerblock = new System.Timers.Timer(speed[this.level]);//按照level设置毫秒间隔
            timerblock.Elapsed += new ElapsedEventHandler(OnBlockTimeEvent);//达到时间间隔触发OnBlockTimeEvent事件
            timerblock.AutoReset = true;//触发elapsed事件后重置计时器
            timerblock.Start();//开始计时
        }
        //根据speed，每隔一段时间更新一次
        private void OnBlockTimeEvent(object source,ElapsedEventArgs e)
        {
            this.Move();//自动前进一个单位
            if (Checkdeath())//死亡检测
            {
                //如果死亡
                this.timerblock.Stop();//停止计时器
                this.timerblock.Dispose();//释放timerblock的空间
                MessageBox.Show("您的得分：" + this.blocks.Count, "游戏结束");//游戏结束，展示分数
            }
            score = this.blocks.Count;
        }
        //检查是否死亡
        private bool Checkdeath()
        {
            snake head = (snake)blocks[0];//取出蛇头
            //判断是否超出墙壁
            if (head.getpoint.X < 0 || head.getpoint.Y < 0||head.getpoint.X>=width||head.getpoint.Y>=height)
            {
                this.isgameover = true;
                return true;
            }
            //判断是否碰到蛇身
            for(int i=1;i<this.blocks.Count;i++)
            {
                snake body = (snake)blocks[i];//循环判断是否与任一蛇身块坐标重合
                if(head.getpoint.X==body.getpoint.X&&head.getpoint.Y==body.getpoint.Y)
                {
                    this.isgameover = true;
                    return true;
                }
            }
            //否则未死亡，游戏继续
            this.isgameover = false;
            return false;
        }
        //吃到食物，蛇体加长，生成下一个食物
        private snake Getfood()
        {
            snake food = null;
            Random r = new Random();
            bool redo = false;
            while(true)
            {
                redo = false;
                //生成食物的随机坐标（x，y不大于width和height）
                int x = r.Next(this.width);
                int y = r.Next(this.height);
                //检测生成的食物坐标是否与蛇体冲突
                for(int i=0;i<this.blocks.Count;i++)
                {
                    snake body = (snake)blocks[i];
                    if(x==body.getpoint.X&&y==body.getpoint.Y)
                    {
                        redo = true;//与蛇体冲突，重新生成一个随机坐标
                    }
                }
                if(redo==false)//不冲突就生成这个食物块，退出循环
                {
                    food = new snake(Color.Yellow, this.size, new Point(x, y));
                    break;
                }
            }
            return food;//将生成的food块作为返回值返回
        }
        //移动
        private void Move()
        {
            Point p;//移动后位置的坐标
            snake head = (snake)blocks[0];//获取蛇头
            p = new Point(head.getpoint.X, head.getpoint.Y);
            //根据当前的移动方向（directions）执行相应的坐标变化
            if(this.direction==directions.left)
            {
                p = new Point(head.getpoint.X - 1, head.getpoint.Y);
            }
            else if(this.direction==directions.right)
            {
                p = new Point(head.getpoint.X + 1, head.getpoint.Y);
            }
            else if(this.direction==directions.up)
            {
                p = new Point(head.getpoint.X, head.getpoint.Y - 1);
            }
            else if(this.direction==directions.down)
            {
                p = new Point(head.getpoint.X, head.getpoint.Y + 1);
            }
            snake new_head = new snake(Color.Red, size, p);//生成新的蛇头
            //如果本次移动没有吃到食物，删除最后一个蛇块
            if(this.food.getpoint!=p)
            {
                this.blocks.RemoveAt(this.blocks.Count - 1);
            }
            //如果吃到了食物，则生成一个新的食物块
            else
            {
                food = Getfood();
            }
            //将新生成的蛇头放到ArrayList中最靠前的位置成为新的蛇头
            blocks.Insert(0, new_head);
            PaintPalette(Palette);//更新画板
        }
        //更新画板
        public void PaintPalette(Graphics gp)
        {
            gp.Clear(bgcolor);//清理画布
            food.Paint(gp);//画食物
            //画蛇
            foreach (snake block in blocks)
            {
                block.Paint(gp);
            }
        }
        //结束游戏进程
        public void Stop()
        {
            this.timerblock.Stop();//停止计时器
            this.timerblock.Dispose();//释放timerblock的空间
        }
        //枚举方向
        public enum directions
        {
            left,right,up,down
        }
    }
}
