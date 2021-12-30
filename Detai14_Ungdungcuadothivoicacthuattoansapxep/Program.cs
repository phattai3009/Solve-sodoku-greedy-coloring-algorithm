//Ứng dụng thuật toán tô màu tham lam giải quyết bài toán Sudoku
//https://en.wikipedia.org/wiki/Greedy_coloring
//https://en.wikipedia.org/wiki/Precoloring_extension
//
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Detai14_Ungdungcuadothivoicacthuattoansapxep
{
    class Program
    {
        static Cell[] Cells = new Cell[81];     //Tập đỉnh
        static List<int> V = new List<int>();   //Tập đỉnh chưa được điền số (tô màu)
        static Cell[,] E = new Cell[1944, 2];   //Tập cạnh kề
        static void Nhap(string path)
        {
            string read = File.ReadAllText(path);
            read = read.Replace("\r\n", "");

            int dong = 1;
            int cot = 1;
            for (int j = 0; j < 81; j++)
            {
                if (cot == 10)
                {
                    cot = 1; dong++;
                }

                int value = Convert.ToInt32(read.Substring(j, 1));
                Cells[j] = new Cell(dong, cot, value);

                cot++;

                if (value == 0)
                {
                    V.Add(j);
                }
            }

            Xuat();
        }
        static void Nhapcanh()
        {
            int[] Row_index = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };         //Ds phần tử dòng thứ nhất
            int[] Col_index = new int[] { 0, 9, 18, 27, 36, 45, 54, 63, 72 };  //Ds phần tử cột thứ nhất
            int[] Square_index = new int[] { 0, 1, 2, 9, 10, 11, 18, 19, 20 }; //Ds phần tử khối 3x3 thứ nhất
            int idx;
            int row; int col; int square;
            int e = 0;

            for (int i = 0; i < 81; i++)
            {
                row = Cells[i].Row_ - 1;
                col = Cells[i].Col_ - 1;
                square = ((row / 3) * 3) * 9 + (col / 3) * 3; //Phần tử đầu tiên trong khối chứa ô [i]

                for (int j = 0; j < 9; j++)
                {
                    idx = row * 9 + Row_index[j]; //Hàng xóm cùng dòng
                    if (idx != i)
                    {
                        E[e, 0] = Cells[i];
                        E[e, 1] = Cells[idx];
                        e++;
                    }

                    idx = col + Col_index[j]; //Hàng xóm cùng cột
                    if (idx != i)
                    {
                        E[e, 0] = Cells[i];
                        E[e, 1] = Cells[idx];
                        e++;
                    }

                    idx = square + Square_index[j]; //Hàng xóm cùng khối
                    if (idx != i)
                    {
                        E[e, 0] = Cells[i];
                        E[e, 1] = Cells[idx];
                        e++;
                    }
                }
            }
        }
        static Boolean KtraKe(int i, int so)
        {
            i = i * 24;

            for (int j = 0; j < 24; j++)
            {
                if (E[i, 1].Value_ == so)
                {
                    return true;
                }
                i++;
            }

            return false;
        }
        static void Tomau()
        {
            List<int> dltcell;

            while (V.Count > 0)
            {
                dltcell = new List<int>();
                foreach (var cell in V)
                {
                    string so = "";
                    for (int i = 1; i < 10; i++)
                    {
                        if (!KtraKe(cell, i))
                        {
                            so += i;
                        }
                    }

                    if (so.Length == 1)
                    {
                        Cells[cell].Value_ = Convert.ToInt32(so);
                        dltcell.Add(cell);
                    }
                }

                if (dltcell.Count > 0)
                {
                    foreach (var cell in dltcell)
                    {
                        V.Remove(cell);
                    }
                }
                else
                {
                    Console.WriteLine("===================");
                    Console.WriteLine("Không thể hoàn thành.");
                    break;
                }
            }
        }
        static void Xuat()
        {
            for (int i = 0; i < 81; i++)
            {
                Console.Write(Cells[i].Value_.ToString().PadRight(2));
                if ((i + 1) % 3 == 0)
                    Console.Write("".PadRight(1));
                if ((i + 1) % 9 == 0)
                    Console.WriteLine();
                if (i == 26 || i == 53)
                    Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                String Path = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\Desktop\test.txt");
                Nhap(Path);
                Nhapcanh();
                Tomau();
                Console.WriteLine("===================");
                Xuat();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                TextWriter error = Console.Error;
                error.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
