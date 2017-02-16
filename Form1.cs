using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordFinder
{
    public partial class Form1 : Form
    {
        hashtab head = new hashtab();
        Dictionary<char, int> alpha = new Dictionary<char, int>();
        Dictionary<int, char> num = new Dictionary<int, char>();

        public Form1()
        {
            InitializeComponent();
            int counter = 0;
            string line;

            alpha.Add('a', 0);
            alpha.Add('b', 1);
            alpha.Add('c', 2);
            alpha.Add('d', 3);
            alpha.Add('e', 4);
            alpha.Add('f', 5);
            alpha.Add('g', 6);
            alpha.Add('h', 7);
            alpha.Add('i', 8);
            alpha.Add('j', 9);
            alpha.Add('k', 10);
            alpha.Add('l', 11);
            alpha.Add('m', 12);
            alpha.Add('n', 13);
            alpha.Add('o', 14);
            alpha.Add('p', 15);
            alpha.Add('q', 16);
            alpha.Add('r', 17);
            alpha.Add('s', 18);
            alpha.Add('t', 19);
            alpha.Add('u', 20);
            alpha.Add('v', 21);
            alpha.Add('w', 22);
            alpha.Add('x', 23);
            alpha.Add('y', 24);
            alpha.Add('z', 25);
            alpha.Add('-', 26);

            num.Add(0, 'a');
            num.Add(1, 'b');
            num.Add(2, 'c');
            num.Add(3, 'd');
            num.Add(4, 'e');
            num.Add(5, 'f');
            num.Add(6, 'g');
            num.Add(7, 'h');
            num.Add(8, 'i');
            num.Add(9, 'j');
            num.Add(10, 'k');
            num.Add(11, 'l');
            num.Add(12, 'm');
            num.Add(13, 'n');
            num.Add(14, 'o');
            num.Add(15, 'p');
            num.Add(16, 'q');
            num.Add(17, 'r');
            num.Add(18, 's');
            num.Add(19, 't');
            num.Add(20, 'u');
            num.Add(21, 'v');
            num.Add(22, 'w');
            num.Add(23, 'x');
            num.Add(24, 'y');
            num.Add(25, 'z');
            num.Add(26, '-');

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\vyomc\\OneDrive\\Documents\\Visual Studio 2015\\Projects\\WordFinder\\words.txt");
            while ((line = file.ReadLine()) != null)
            {
                add_word(line,head);
                counter++;
            }

            //MessageBox.Show(counter.ToString());
            file.Close();
        }

        private void add_word(string word, hashtab head)
        {
            for (int i = 0; i < word.Length; i++)
            {
                if (alpha.Keys.Contains(word[i]))
                {
                    if (head.point.Count == 0)
                    {
                        for (int x = 0; x < globals.total_count; x++)
                        {
                            hashtab h1 = new hashtab();
                            head.point.Add(h1);
                        }
                    }
                    head.cont[alpha[word[i]]] = 1;
                    if (i == word.Length - 1)
                    {
                        head.ext[alpha[word[i]]] = 1;
                    }
                    head = head.point[alpha[word[i]]];
                }
                else
                {
                    MessageBox.Show(word);
                }
            }
        }

        private void check_word(string word, hashtab head)
        {
            richTextBox1.Clear();
            for (int i = 0; i < word.Length; i++)
            {
                if (alpha.Keys.Contains(word[i]))
                {
                    if (head.ext[alpha[word[i]]] == 1)
                    {
                        richTextBox1.Text = "exists";
                    }
                    else
                    {
                        richTextBox1.Text = "not exists";
                    }
                    if (head.cont[alpha[word[i]]] == 1)
                    {
                        head = head.point[alpha[word[i]]];
                    }
                    if (i == word.Length - 1)
                    {
                        richTextBox2.Clear();
                        complete_word(word, head);
                    }
                }
            }
        }

        private void complete_word(string word, hashtab head)
        {
            for (int i = 0; i < globals.total_count; i++)
            {
                if (head.ext[i] == 1)
                {
                    string word2 = word;
                    word2 += num[i].ToString();
                    richTextBox2.Text += word2 + "\n";
                }
                if (head.cont[i] == 1)
                {
                    string word2 = word;
                    word2 += num[i].ToString();
                    hashtab head2 = head.point[i];
                    complete_word(word2, head2);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength>0)
            {
                check_word(textBox1.Text, head);
            }
        }
    }

    public static class globals
    {
        public static int total_count = 27;
    }

    public class hashtab
    {
        public int[] cont = new int[globals.total_count];
        public int[] ext = new int[globals.total_count];
        public List<hashtab> point = new List<hashtab>();

        public hashtab()
        {
            for (int x = 0; x < globals.total_count; x++)
            {
                cont[x] = 0;
                ext[x] = 0;
            }
        }
    }

}
