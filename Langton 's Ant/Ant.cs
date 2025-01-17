﻿using System;
using System.Collections.Generic;

namespace Langton__s_Ant
{
    public class Ant
    {
        private Vector AntPos;
        private Vector OldAntPos;

        private static int rows;
        private static int cols;

        private static bool[,] Fild;

        private List<Vector> queue = new List<Vector>(4);
        private Vector Right = new Vector(1, 0);
        private Vector Up = new Vector(0, 1);
        private Vector Left = new Vector(-1, 0);
        private Vector Down = new Vector(0, -1);

        private Vector Increment;
        private int i; //first increment from -1 to 3     

        private static Random random = new Random();

        public Ant(int rows, int cols, bool StartPos)
        {
            if (StartPos)
            {
                AntPos = new Vector(cols / 2, rows / 2);
                OldAntPos = AntPos;
            }
            if (!StartPos)
            {
                AntPos = new Vector(random.Next(cols - 1), random.Next(rows - 1));
                OldAntPos = AntPos;
            }

            Ant.rows = rows;
            Ant.cols = cols;

            Fild = new bool [cols,rows];

            queue.Add(Right);
            queue.Add(Up);
            queue.Add(Left);
            queue.Add(Down);

            i = random.Next(1, 4) - 1;        
        }

        public Vector NewPos()
        {
            while (i < queue.Count)
            {
                if (!Fild[AntPos.X, AntPos.Y])
                {
                    i++;
                    if (i > 3) { i = 0; }
                     
                    Increment = new Vector(queue[i].X, queue[i].Y);

                    return Increment;
                }
                if (Fild[AntPos.X, AntPos.Y])
                {
                    i--;
                    if (i == -1) { i = 3; }

                    Increment = new Vector(queue[i].X, queue[i].Y);

                    if (i > 3) { i = 0; }

                    return Increment;
                }
            }
            return Increment;
        }

        public void NextStep()
        {
            AntPos = AntPos + Increment;

            if (AntPos.X < 0)
            {
                AntPos.X = (cols - 1);
            }
            if (AntPos.Y < 0)
            {
                AntPos.Y = (rows - 1);
            }
            if (AntPos.X > (cols - 1))
            {
                AntPos.X = 0;
            }
            if (AntPos.Y > (rows - 1))
            {
                AntPos.Y = 0;
            }

            Fild[OldAntPos.X, OldAntPos.Y] = !Fild[OldAntPos.X, OldAntPos.Y];
            OldAntPos = AntPos;
        }


        public static bool[,] GetFildCopy()
        {
            var CopyFild = new bool[cols, rows];
            
            for (int x = 0; x < cols; x++)
            {
                for(int y = 0; y < rows; y++)
                {
                    CopyFild[x, y] = Fild[x, y];
                }
            }
            return CopyFild;
        }

        public static int GetCols()
        {
            var cols = Ant.cols;
            return cols;
        }

        public static int GetRows()
        {
            var rows = Ant.rows;
            return rows;
        }

        public Vector GetAntPos()
        {
            var AntPos = this.AntPos;
            return AntPos;
        }

        public Vector GetOldAntPos()
        {
            var OldAntPos = this.OldAntPos;
            return OldAntPos;
        }
    }
}
