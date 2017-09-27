using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 贪吃蛇
{
    class snake
    {
        //定义颜色大小坐标
        private Color color;
        private int size;
        private Point point;
        //实例化类时的初始化数值
        public snake(Color color, int size, Point p)
        {
            this.color = color;
            this.size = size;
            this.point = p;
        }
        //获取坐标的方法
        public Point getpoint
        {
            get
            {
                return point;
            }
        }
        //绘制蛇身体的长方体块
        public virtual void Paint(Graphics g)
        {
            SolidBrush sb = new SolidBrush(color);
            lock (g)
            {
                try
                {
                    g.FillRectangle(sb, point.X * size, point.Y * size, size - 1, size - 1);
                }
                catch { }
            }
        }
    }
}
