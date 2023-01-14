using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Laba6
{
    public partial class Form1 : Form
    {

        char[] consonants = {'й', 'ц', 'к', 'н', 'г', 'ш', 'щ', 'з', 'х',
                              'ф','в','п','р','л','д','ж',
                              'ч','с','м','т','б' }; // согласные буквы

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = richTextBox1.Text;
            string[] words; // массив слов
            richTextBox1.Text = "";

            Regex regex = new Regex(@"/[!?,-.]"); // шаблон удаления знаков препинания
            int count = Convert.ToInt32(textBox1.Text); // количество слов
            string[] sentences = input.Split(new string[] { ". " }, StringSplitOptions.RemoveEmptyEntries); // разбить текст на предложения
            foreach (var item in sentences) // перебор предложений
            {
                words = regex.Replace(item, "").Split(' '); // удалить знаки препинания и разбить предложение на слова
                if (words.Length == count) // если количество слов совпадает с введённым
                    richTextBox1.Text += item + ". "; // вывести предложение с точкой
            }

        }

        bool ContainsEveryChar(string inp, char[] arr) // метод проверки строки на наличие каждого символа из массива
        {
            string str = inp.ToLower(); // всё приводим к нижнему регистру
            foreach (var item in arr) // перебор символов массива
            {
                if (!str.Contains(item)) // если строка не содержит символов
                {
                    return false;
                }
            }
            return true;
        }

        bool ContainsAnyChar(string inp, char[] arr) // метод проверки строки на наличие любого символа из массива
        {
            string str = inp.ToLower(); // всё приводим к нижнему регистру
            foreach (var item in arr) // перебор симолов массива
            {
                if (str.Contains(item)) // если строка содержит символа
                {
                    return true;
                }
            }
            return false;
        }

        bool EqualAnyChar(char inp, char[] arr) // метод проверки равенства символа на любой символ в массиве символов
        {
            foreach (var ch in arr)
            {
                if (inp == ch)
                    return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string input = richTextBox2.Text;
            richTextBox2.Text = "";
            string[] words = input.Split(' '); // разбить предложение на слова
            char[] letters = words[0].ToLower().ToCharArray(); // преобразовать первое слово в массив симолов

            for (int i = 1; i < words.Length; i++) // перебор слов, не включая первого
            {
                if (ContainsEveryChar(words[i], letters)) // если слово содержит символы массива letters
                    richTextBox2.Text += words[i] + " "; // вывести слово и пробел
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string word; 
            string input = richTextBox3.Text;
            richTextBox3.Text = "";

            Regex regex = new Regex(@"\w+", RegexOptions.IgnoreCase); // шаблон поиска слов
            foreach (var item in regex.Matches(input)) // перебор найденных строк
            {
                word = item.ToString().ToLower(); // преобразуем каждое слово в нижний регистр
                if (EqualAnyChar(word[0], consonants)) // если первая буква слова равна любому из символов в массиве consonants
                    richTextBox3.Text += item + " "; // вывести слово вместе с пробелом
            }

        }   

        private void button4_Click(object sender, EventArgs e)
        {
            string input = richTextBox4.Text;
            richTextBox4.Text = "";

            Regex spacesRegex = new Regex(@"\s+"); // шаблон поиска множества пробелов
            Regex tabulationRegex = new Regex(@"(?:[.?!])(\s*)"); // шаблон поиска всех знаков окончания предложения, не включая их самих в выборку
                                                                  // и любого количества пробелов

            input = spacesRegex.Replace(input, " "); // замена множества пробелов на один
            input = tabulationRegex.Replace(input, m => m.Groups[0].Value[0].ToString() + "  ");  // заменить все вхождения на конкатенацию первого симовола первой найденной группы и двух пробелов

            richTextBox4.Text = input; // вывести результат
        }
    }
}
