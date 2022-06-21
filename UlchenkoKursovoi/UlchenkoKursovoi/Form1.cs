using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UlchenkoKursovoi
{
    public partial class Form1 : Form
    {
        static string dataBasePath = "C:\\Users\\Public\\Documents\\database.json";
        List<Uchenik> uchen = JsonConvert.DeserializeObject<List<Uchenik>>(File.ReadAllText(open_file()));
        List<Uchenik> uchenCopy = new List<Uchenik>();
        public Form1()
        {
            InitializeComponent();
            reDrawTable(uchen);

            textBox1.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
            textBox2.KeyPress += new KeyPressEventHandler(textBox2_KeyPress);
            textBox4.KeyPress += new KeyPressEventHandler(textBox4_KeyPress);
        }

        /// <summary>
        /// Проверяет существует ли файл, если нет создает
        /// </summary>
        /// <returns>адрес файла</returns>
        private static string open_file()
        {
            if (!File.Exists(dataBasePath))
            {
                File.Create(dataBasePath);
            }
            return dataBasePath;
        }

        /// <summary>
        /// Добавление ученика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox4.Text == ""))
            {
                MessageBox.Show("Не заполнены важные поля", "Ошибка");
                return;
            }

            var n = textBox1.Text;

            if (uchen.Where(u => u.Name == textBox1.Text).Count() != 0)
            {
                MessageBox.Show("Такой ученик уже есть", "Внимание");
                Uchenik nay = uchen.Where(u => u.Name.ToLower() == textBox1.Text.ToLower()).First();
                textBox1.Text = nay.Name;
                textBox2.Text = nay.Posesh.ToString();
                textBox4.Text = nay.KolOshibok.ToString();
                comboBox3.SelectedItem = nay.Kateg;
                comboBox1.SelectedItem = nay.ResPredEx;
                comboBox2.SelectedItem = nay.ResEx;
            }
            else
            {
                var k = comboBox3.Text;
                var p = int.Parse(textBox2.Text);
                var ko = int.Parse(textBox4.Text);
                var rp = comboBox1.Text;
                var re = comboBox2.Text;

                var uch = new Uchenik(
                        name: n,
                        kateg: k,
                        posesh: p,
                        kolOshibok: ko,
                        resPredEx: rp,
                        resEx: re
                    );

                uchen.Add(uch);

                var item1 = new ListViewItem(new[] {
                   n.ToString(),
                   p.ToString(),
                   k.ToString(),
                   ko.ToString(),
                   rp.ToString(),
                   re.ToString()
                });
                listView2.Items.Add(item1);
            }
        }

        /// <summary>
        /// Удаление ученика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                uchen.Remove(uchen.Where(u => u.Name.ToLower() == textBox1.Text.ToLower()).First());
                //reDrawTable();
                reDrawTable(uchen);
            }
            catch
            {
                MessageBox.Show("Такого ученика нет", "Ошибка");
            }
        }

        /// <summary>
        /// Изменение данных об ученике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (uchen.Where(u => u.Name == textBox1.Text).Count() == 0)
            {
                MessageBox.Show("Такого ученика нет", "Ошибка");
                return;
            }
            try
            {
                uchen.Select(u => u).Where(u => u.Name.ToLower() == textBox1.Text.ToLower()).First().Posesh = int.Parse(textBox2.Text);
                uchen.Select(u => u).Where(u => u.Name.ToLower() == textBox1.Text.ToLower()).First().Kateg = comboBox3.Text;
                uchen.Select(u => u).Where(u => u.Name.ToLower() == textBox1.Text.ToLower()).First().KolOshibok = int.Parse(textBox4.Text);
                uchen.Select(u => u).Where(u => u.Name.ToLower() == textBox1.Text.ToLower()).First().ResPredEx = comboBox1.Text;
                uchen.Select(u => u).Where(u => u.Name.ToLower() == textBox1.Text.ToLower()).First().ResEx = comboBox2.Text;
                reDrawTable(uchen);
            }
            catch
            {
                MessageBox.Show("Одно из полей не заполнено", "Ошибка");
            }
        }

        /// <summary>
        /// Запись в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            // File.WriteAllText("database.json", JsonConvert.SerializeObject(uchen));
            File.WriteAllText(dataBasePath, JsonConvert.SerializeObject(uchen));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(1069, 500);
        }

        /// <summary>
        /// Метод отоброжения актуальных данных в таблице
        /// </summary>
        private void reDrawTable(List<Uchenik> ucheniks)
        {
            listView2.Items.Clear();

            foreach (var uc in ucheniks)
            {
                var item1 = new ListViewItem(new[] {
                    uc.Name,
                    uc.Posesh.ToString(),
                    uc.Kateg,
                    uc.KolOshibok.ToString(),
                    uc.ResPredEx,
                    uc.ResEx
                });
                listView2.Items.Add(item1);
            }
        }

        /// <summary>
        /// Запрет на ввод цифр в поле задания имени ученика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Запрет на ввод букв в поле посещаемости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Запрет на ввод букв в поле задания колличества ошибок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Фильтрация по категории обучения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (uchenCopy.Count != 0)
            {
                uchenCopy.Clear();
            }

            var uchenCopy1 = uchen.Select(u => u).Where(u => u.Kateg == comboBox3.Text);
            foreach (var uc in uchenCopy1)
            {
                uchenCopy.Add(uc);
            }
            reDrawTable(uchenCopy);
        }

        /// <summary>
        /// Сброс фильтрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            reDrawTable(uchen);
        }
    }
}
