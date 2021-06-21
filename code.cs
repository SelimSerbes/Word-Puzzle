using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace proje_3
{
    class Program
    {

        public static char[,] board;
        public static string[] word_list;
        public static string[] word_list2;
        public static int row;
        public static int col_num;
        public static int word_counter = 0;
        public static string plus_word;
        public static string[] word_list_plus;

        public static void readBoard(string fileName)
        {
            StreamReader f = File.OpenText("puzzle.txt");///This is for read the file from computer.
            string line;

            do
            {
                line = f.ReadLine();
                col_num = line.Length;
                row++;
            } while (!f.EndOfStream);
            board = new char[row, col_num];
            f.Close();

            f = File.OpenText("puzzle.txt");
            do
            {
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    line = f.ReadLine();
                    for (int k = 0; k < board.GetLength(1); k++)
                    {
                        board[i, k] = Convert.ToChar(line.Substring(k, 1));
                    }
                }
            } while (!f.EndOfStream);
            f.Close();


        } //+
        public static void readWordList(string fileName)
        {
            StreamReader f = File.OpenText("dictionary.txt");///Also this is for read the file from computer.
            string line = "";
            int counter = 0;
            do
            {
                line = f.ReadLine();
                counter++;
            } while (!f.EndOfStream);
            ////counters for words.
            word_list = new string[counter];
            word_list2 = new string[counter];
            word_list_plus = new string[counter];
            f.Close();
            f = File.OpenText("dictionary.txt");
            do
            {
                for (int i = 0; i < word_list.Length; i++)
                {
                    line = f.ReadLine();
                    word_list[i] = line;
                }
            } while (!f.EndOfStream);


            f.Close();
            for (int i = 0; i < word_list_plus.Length; i++)
            {
                word_list_plus[i] = " ";
            }
        } //+
        public static void sortWordList()
        {
            int i = 1, j, temp;
            string value;
            int arraypeace = word_list.Length;
            while (i < arraypeace)///////////sorting words with their lenghts.
            {
                j = arraypeace - 1;
                while (j >= 1)
                {
                    if (word_list[j - 1].Length > word_list[j].Length)
                    {
                        value = word_list[j];
                        word_list[j] = word_list[j - 1];
                        word_list[j - 1] = value;
                    }
                    else if (word_list[j - 1].Length == word_list[j].Length)
                    {
                        temp = word_list[j].Length;
                        for (int k = 1; k < temp; k++)
                        {
                            if ((Convert.ToInt16(word_list[j - 1][0]) >= Convert.ToInt16(word_list[j][0])) &&
                                Convert.ToInt16(word_list[j - 1][k]) > Convert.ToInt16(word_list[j][k]))
                            {
                                value = word_list[j - 1];
                                word_list[j - 1] = word_list[j];
                                word_list[j] = value;
                                k = temp;
                            }
                        }
                    }
                    j--;
                }
                i++;
            }
            for (int m = 0; m < word_list.Length; m++)
            {
                word_list2[m] = word_list[m];
            }
        }    //+
        public static void printScreen()////printing our console screen
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("WORD = " + word_counter);

            Console.Write(" ");
            int limit = 0;
            for (int i = 0; i < col_num; i++)////puzzle board's border numbers.
            {
                if (limit == 10)
                {
                    limit = 0;
                }
                Console.Write(limit);
                limit++;
            }
            Console.WriteLine();
            limit = 0;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (limit == 10)
                {
                    limit = 0;
                }
                Console.Write(limit);


                for (int k = 0; k < board.GetLength(1); k++)
                {
                    Console.Write(board[i, k]);
                }
                Console.WriteLine();
                limit++;
                if (i == board.GetLength(0) - 1)
                {
                    if (plus_word == null)
                    {
                        Console.WriteLine("Press 'ENTER' to start.");
                    }
                    else
                    {
                        Console.WriteLine("The word '" + plus_word + "' is placed.      ");
                    }

                }
            }


            Console.SetCursorPosition(21, 1);
            Console.Write("+-WORD-LIST---------------+");
            Console.SetCursorPosition(21, 16);
            Console.Write("+-------------------------+");
            for (int i = 0; i < 14; i++)
            {
                Console.SetCursorPosition(21, 2 + i);
                Console.Write("|");
            }
            for (int i = 0; i < 14; i++)
            {
                Console.SetCursorPosition(47, 2 + i);
                Console.Write("|");
            }
            for (int i = 0; i < word_list.Length; i++)
            {
                if (plus_word == word_list[i])///////if the word placed, write '+' near the word.
                {
                    word_list_plus[i] = "+";
                }
                if (i < 14)
                {
                    Console.SetCursorPosition(23, 2 + i);
                    Console.Write("[" + word_list_plus[i] + "]" + word_list[i]);
                }
                if (i >= 14)
                {
                    Console.SetCursorPosition(35, 2 + i - 14);
                    Console.Write("[" + word_list_plus[i] + "]" + word_list[i]);
                }


            }
        }     //+
        public static void scanHorizontal()/////scanning board horizontal
        {

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    int temp = 0;
                    string blankword = "";
                    while (board[i, j] != '█')/////if it is not wall 
                    {
                        blankword += board[i, j];
                        j++;
                    }
                    for (int k = 0; k < blankword.Length; k++)
                    {
                        if (blankword[k] != ' ')
                        {
                            temp = 1;
                            break;
                        }
                    }
                    if (blankword.Length == 1)
                        temp = 0;
                    if (blankword != " " && temp == 1)
                    {
                        temp = 0;
                        string setWord = findWord(blankword);
                        for (int k = 0; k < setWord.Length; k++)
                        {
                            if (setWord[k] == ' ')
                            {
                                temp = 1;
                                break;
                            }
                        }
                        if (setWord != " " && temp == 0)
                        {
                            printWordHorizontal(setWord, i, j);

                        }
                    }
                }

            }
        } //+
        public static void scanVertical()///////Scanning board vertical.
        {

            for (int i = 0; i < board.GetLength(1); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    int temp = 0;
                    string blankword = "";
                    while (board[j, i] != '█')
                    {
                        blankword += board[j, i];
                        j++;
                    }
                    for (int k = 0; k < blankword.Length; k++)
                    {
                        if (blankword[k] != ' ')
                        {
                            temp = 1;
                            break;
                        }
                    }
                    if (blankword.Length == 1)
                        temp = 0;
                    if (blankword != " " && temp == 1)
                    {
                        temp = 0;
                        string setWord = findWord(blankword);
                        for (int k = 0; k < setWord.Length; k++)
                        {
                            if (setWord[k] == ' ')
                            {
                                temp = 1;
                                break;
                            }
                        }
                        if (setWord != " " && temp == 0)
                        {
                            printWordVertical(setWord, j, i);

                        }
                    }
                }

            }
        }  //+
        public static void writeBoard()////////writing final board to a file. 
        {
            StreamWriter f = File.CreateText("solution.txt");
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    f.Write(board[i, j]);
                }

                f.WriteLine();
            }
            f.Close();
        }//+
        public static string findWord(string blankword)/////finding words to place the board.
        {
            int counter3 = 0;
            int counter2 = 0;
            int counter = 0;
            int temp = 0;
            for (int i = 0; i < word_list.Length; i++)/////do not have to scan all of the blanks. so it is jumps the finding word.
            {
                if (blankword == word_list[i])
                {
                    counter = 1;
                    temp = i;
                }

            }
            if (counter == 0)
            {
                for (int k = 0; k < word_list2.Length; k++)
                {
                    if (blankword.Length == word_list2[k].Length)
                    {
                        for (int m = 0; m < word_list2[k].Length; m++)
                        {
                            if (blankword[m] == word_list2[k][m])
                            {
                                counter2++;
                                if (counter2 == 1)
                                {
                                    counter3 = k;
                                }
                                else if (counter2 > 1)
                                {
                                    blankword = word_list2[k];
                                    plus_word = word_list2[k];
                                    word_list2[k] = "";
                                    break;
                                }
                            }
                        }
                        if (word_list2[k] == "")
                        {
                            break;
                        }

                    }

                }
                if (counter2 == 1)
                {
                    blankword = word_list2[counter3];
                    plus_word = word_list2[counter3];
                    word_list2[counter3] = "";
                }
                return blankword;
            }
            else
            {
                blankword = " ";
                return blankword;
            }


        } //+
        public static void printWordHorizontal(string word, int x, int y)////////printing words horizontal.
        {

            int temp = 0;
            for (int i = 0; i < word_list2.Length; i++)
            {
                if (word == word_list2[i])
                {
                    temp = 1;
                    break;
                }
            }
            if (temp == 0)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    board[x, y - word.Length + i] = word[i];
                }
                word_counter++;
                //Console.Clear();
                printScreen();
                Console.ReadLine();
            }
        } //+
        public static void printWordVertical(string word, int x, int y)//////////printing words vertical
        {

            int temp = 0;
            for (int i = 0; i < word_list2.Length; i++)
            {
                if (word == word_list2[i])
                {
                    temp = 1;
                    break;
                }
            }
            if (temp == 0)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    board[x - word.Length + i, y] = word[i];
                }
                word_counter++;
                //Console.Clear();
                printScreen();
                Console.ReadLine();
            }
        } //+
        public static void menu()//////beginning of the console.
        {

            Console.SetCursorPosition(30, 8);
            Console.WriteLine("WELCOME TO THE WORD PUZZLE");
            Console.ReadLine();
            Console.Clear();
            while (true)
            {

                Console.WriteLine("-----------MENU--------");
                Console.WriteLine("1) PLAY");
                Console.WriteLine("2) EXIT");
                int choice;
                choice = Convert.ToInt16(Console.ReadLine());
                Console.Clear();
                if (choice == 1)
                {
                    bool flag = false;
                    readBoard("puzzle.txt");
                    readWordList("dictionary.txt");
                    sortWordList();
                    printScreen();

                    Console.ReadLine();
                    while (flag == false)
                    {
                        flag = true;
                        scanHorizontal();
                        scanVertical();

                        for (int i = 0; i < board.GetLength(0); i++)
                        {
                            for (int j = 0; j < board.GetLength(1); j++)
                            {
                                if (board[i, j] == ' ')
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag == false)
                                break;
                        }
                    }
                    writeBoard();
                    Console.SetCursorPosition(10, 26);
                    Console.WriteLine("END OF THE GAME!");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                else if (choice == 2)
                {
                    Environment.Exit(0);

                }
                else
                {
                    Console.WriteLine("INVALID LETTER!");
                    Console.ReadLine();
                    Console.Clear();

                }

            }


        }
        static void Main(string[] args)
        {
            menu();
        }
    }
}


