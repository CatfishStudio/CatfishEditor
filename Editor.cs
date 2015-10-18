/*
 * Сделано в SharpDevelop.
 * Пользователь: Somov Evgeniy
 * Дата: 04.05.2014
 * Время: 9:51
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Catfish
{
	/// <summary>
	/// Description of Editor.
	/// </summary>
	public partial class Editor : Form
	{
		public Editor()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		private bool _listFocus = false;
		private int _findIndex = 0;
		private int _findLast = 0;
		private String _findText = "";
		
		/* Загрузка списка операторов */
		private void loadOperators(string codeType)
		{
			/*
			String pathFile = Config.MyProgramDirectory + "\\resource\\operators.txt";
			if(File.Exists(pathFile)){
				StreamReader sr = File.OpenText(pathFile);
				String str = "";
				while ((str = sr.ReadLine()) != null) 
           		{
					listBox1.Items.Add(str);
                }
			}
			*/
			
			if(codeType == "html")
			{
				listBox2.Sorted = true;
				listBox1.Items.Clear();
				for(int i=0; i < listBox2.Items.Count; i++)
				{
					listBox1.Items.Add(listBox2.Items[i].ToString());
				}
			}
			if(codeType == "css")
			{
				listBox3.Sorted = true;
				listBox1.Items.Clear();
				for(int i=0; i < listBox3.Items.Count; i++)
				{
					listBox1.Items.Add(listBox3.Items[i].ToString());
				}
			}
			if(codeType == "javascript")
			{
				listBox4.Sorted = true;
				listBox1.Items.Clear();
				for(int i=0; i < listBox4.Items.Count; i++)
				{
					listBox1.Items.Add(listBox4.Items[i].ToString());
				}
			}
			if(codeType == "php")
			{
				listBox5.Sorted = true;
				listBox1.Items.Clear();
				for(int i=0; i < listBox5.Items.Count; i++)
				{
					listBox1.Items.Add(listBox5.Items[i].ToString());
				}
			}
		}
		
		/* Загрузка редактора */
		void EditorLoad(object sender, EventArgs e)
		{
			/* Конфигурация: */
			Config.MyProgramDirectory = Environment.CurrentDirectory + "\\"; // путь к программе
			/* Операторы */
			loadOperators("html"); // загрузка списка операторов
		}
		
		/* определение номера активной строки */
		void RichTextBox1TextChanged(object sender, EventArgs e)
		{
			int i = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
			toolStripStatusLabel2.Text = "Строка: " + i.ToString();
		}
		
		void RichTextBox1MouseMove(object sender, MouseEventArgs e)
		{
			int i = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
			toolStripStatusLabel2.Text = "Строка: " + i.ToString();
		}
		
		void RichTextBox1KeyPress(object sender, KeyPressEventArgs e)
		{
			int i = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
			toolStripStatusLabel2.Text = "Строка: " + i.ToString();
			toolStripStatusLabel4.ForeColor = Color.Red;
		}
		
		void RichTextBox1KeyUp(object sender, KeyEventArgs e)
		{
			int i = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1;
			toolStripStatusLabel2.Text = "Строка: " + i.ToString();	
		}
		
		void RichTextBox1KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Control == true && e.KeyValue == 32){
				int _windowH = this.Height;
				int _windowW = this.Width;
				int _smallWinH = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).Y + 150 + panel1.Height;
				int _smallWinW = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).X + panel1.Width;
				if(_windowH < _smallWinH){
					if(_windowW < _smallWinW){ // Право - низ
						textBox1.Left = 0; textBox1.Top = 101;
						listBox1.Left = 0; listBox1.Top = 0;
						panel1.Left = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).X - 200;
						panel1.Top = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).Y - 50;
						textBox1.RightToLeft = RightToLeft.Yes;
						panel1.Visible = true;
						textBox1.Focus();
					}else{ // Лево - Низ
						textBox1.Left = 0; textBox1.Top = 101;
						listBox1.Left = 0; listBox1.Top = 0;
						panel1.Left = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).X;
						panel1.Top = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).Y - 50;
						textBox1.RightToLeft = RightToLeft.No;
						panel1.Visible = true;
						textBox1.Focus();
					}
				}else{
					if(_windowW < _smallWinW){ // Право - Вверх
						textBox1.Left = 0; textBox1.Top = 0;
						listBox1.Left = 0; listBox1.Top = 23;
						panel1.Left = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).X - 200;
						if(toolStrip1.Visible)
							panel1.Top = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).Y + 50;
						else panel1.Top = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).Y + 25;
						textBox1.RightToLeft = RightToLeft.Yes;
						panel1.Visible = true;
						textBox1.Focus();
					}else{ // Лево - Вверх
						textBox1.Left = 0; textBox1.Top = 0;
						listBox1.Left = 0; listBox1.Top = 23;
						panel1.Left = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).X;
						if(toolStrip1.Visible)
							panel1.Top = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).Y + 50;
						else panel1.Top = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart).Y + 25;
						textBox1.RightToLeft = RightToLeft.No;
						panel1.Visible = true;
						textBox1.Focus();
					}
				}
			}
			/* Закрываем буз выбора */
			if(e.KeyData == Keys.Escape){
				panel1.Visible = false;
				listBox1.SelectedIndex = 0;
				textBox1.Clear();
				toolStripStatusLabel3.Text = "...";
				richTextBox1.Focus();
				_listFocus = true;
			}
		}
		
		/* Производим поиск и выбор нужного оператора */		
		void TextBox1KeyDown(object sender, KeyEventArgs e)
		{
			try{
				/* Закрытие окна операторов */
				if(e.KeyData == Keys.Escape){
					panel1.Visible = false;
					textBox1.Clear();
					toolStripStatusLabel3.Text = "...";
					listBox1.SelectedIndex = 0;
					richTextBox1.SelectionStart = richTextBox1.SelectionStart - 1;
					richTextBox1.SelectionLength = 1;
					richTextBox1.Focus();
				}
				/* передача фокуса листу операторов */
				if(e.KeyData == Keys.Down || e.KeyData == Keys.Up || e.KeyData == Keys.PageUp || e.KeyData == Keys.PageDown){
					_listFocus = true;
					listBox1.Focus();
				}
				/* выбор оператора */
				if(e.KeyData == Keys.Enter){
					if(listBox1.SelectedIndex < 0){
						richTextBox1.SelectionStart = richTextBox1.SelectionStart - 1;
						richTextBox1.SelectionLength = 1;
						/* Вставляем выбранный оператор */
						Clipboard.SetDataObject(textBox1.Text);
						richTextBox1.Paste();
						/* Закрываем */
						panel1.Visible = false;
						listBox1.SelectedIndex = 0;
						textBox1.Clear();
						toolStripStatusLabel3.Text = "...";
						richTextBox1.Focus();
					}else{
						richTextBox1.SelectionStart = richTextBox1.SelectionStart - 1;
						richTextBox1.SelectionLength = 1;
						/* Вставляем выбранный оператор */
						Clipboard.SetDataObject(listBox1.Items[listBox1.SelectedIndex].ToString());
						richTextBox1.Paste();
						/* Закрываем */
						panel1.Visible = false;
						listBox1.SelectedIndex = 0;
						textBox1.Clear();
						toolStripStatusLabel3.Text = "...";
						richTextBox1.Focus();
					}
				}
			}catch{
				richTextBox1.SelectionStart = richTextBox1.SelectionStart - 1;
				richTextBox1.SelectionLength = 1;
				panel1.Visible = false;
				listBox1.SelectedIndex = 0;
				textBox1.Clear();
				toolStripStatusLabel3.Text = "...";
				richTextBox1.Focus();		
			}
		}
		
		void TextBox1KeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyCode.ToString() != "16"  && e.KeyCode.ToString() != "17"){
				String _find = "";
				int _countChar = 0;
				String _inputOperator = "";
				
				toolStripStatusLabel3.Text = textBox1.Text;
				
				_countChar = textBox1.TextLength;
				for(int i = 0; i < listBox1.Items.Count; i++){
					_find = listBox1.Items[i].ToString();
					if(_find.Length >= _countChar){
						for(int j = 0; j < _countChar; j++)
							_inputOperator = _inputOperator + _find[j];
					}
					
					this.Update();
					panel1.Update();
					listBox1.Update();
					if(textBox1.Text == _inputOperator){
						listBox1.SelectedIndex = i;
						this.Update();
						panel1.Update();
						listBox1.Update();
						break;
					}else{
						_inputOperator = "";
					}
					
				}
			}
		}
		
		void ListBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			if(_listFocus){
				textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
				toolStripStatusLabel3.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
			}
		}
		
		void ListBox1KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Escape){
				_listFocus = false;
				textBox1.Focus();
			}
			if(e.KeyData == Keys.Enter){
				richTextBox1.SelectionStart = richTextBox1.SelectionStart - 1;
				richTextBox1.SelectionLength = 1;
				/* Вставляем выбранный оператор */
				Clipboard.SetDataObject(listBox1.Items[listBox1.SelectedIndex].ToString());
				richTextBox1.Paste();
				/* Закрываем */
				panel1.Visible = false;
				listBox1.SelectedIndex = 0;
				textBox1.Clear();
				toolStripStatusLabel3.Text = "...";
				_listFocus = false;
				richTextBox1.Focus();
			}
		}
		/*---------------------------------------------*/
		
		/* Открыть файл -------------------------------*/
		void fileOpen()
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				
				BinaryReader instr = new BinaryReader(File.OpenRead(openFileDialog1.FileName));
				byte[] data = instr.ReadBytes((int)instr.BaseStream.Length);
				instr.Close();
				
				// определяем UTF-8 с BOM (EF BB BF)
				if(data.Length > 2 && data[0] == 0xef && data[1] == 0xbb && data[2] == 0xbf){
					StreamReader sr = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.UTF8);
					richTextBox1.Clear();
					richTextBox1.LoadFile(sr.BaseStream, RichTextBoxStreamType.PlainText);
					sr.Close();
					this.Text = openFileDialog1.FileName;
					checkedCoding("Кодировка: UTF-8", false, true, false);
				}else{
					// определяем ASCII или UTF-8 без BOM
					bool typeUTF8Wb = true;
					
					
					if(typeUTF8Wb == false){ // определено как ASCII
						StreamReader sr = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.ASCII);
						richTextBox1.Clear();
						richTextBox1.LoadFile(sr.BaseStream, RichTextBoxStreamType.PlainText);
						sr.Close();
						this.Text = openFileDialog1.FileName;
						checkedCoding("Кодировка: ASCII", true, false, false);
						
					}else{ // определено как UTF-8 без BOM
						UTF8Encoding utf8wb = new UTF8Encoding(false);
						StreamReader sr = new StreamReader(openFileDialog1.FileName, utf8wb);
						richTextBox1.Clear();
						while (sr.Peek() > -1)
							richTextBox1.Text += sr.ReadLine().ToString() + System.Environment.NewLine;
						sr.Close();
						this.Text = openFileDialog1.FileName;
						checkedCoding("Кодировка: UTF-8 without BOM", false, false, true);
					}
				}
			}
		}
		
		void ОткрытьФайлToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileOpen(); // открываем файл в кодировке по умолчанию
		}
		
		/* Открыть файл ASCII */
		void fileOpenASCII()
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				StreamReader sr = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.ASCII);
				richTextBox1.Clear();
				richTextBox1.LoadFile(sr.BaseStream, RichTextBoxStreamType.PlainText);
				sr.Close();
				this.Text = openFileDialog1.FileName;
				checkedCoding("Кодировка: ASCII", true, false, false);
			}
		}
		
		void ASCIIToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileOpenASCII(); // открываем файл как ASCII
		}
		
		/* Открыть файл UTF-8 */
		void fileOpenUTF8()
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				StreamReader sr = new StreamReader(openFileDialog1.FileName, System.Text.Encoding.UTF8);
				richTextBox1.Clear();
				richTextBox1.LoadFile(sr.BaseStream, RichTextBoxStreamType.PlainText);
				sr.Close();
				this.Text = openFileDialog1.FileName;
				checkedCoding("Кодировка: UTF-8", false, true, false);
			}
		}
		
		void UTF8ToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileOpenUTF8();  // открываем файл как UTF-8
		}
		
		/* Открыть файл UTF-8 without BOM*/
		void fileOpenUTF8wB()
		{
			if(openFileDialog1.ShowDialog() == DialogResult.OK){
				UTF8Encoding utf8wb = new UTF8Encoding(false);
				StreamReader sr = new StreamReader(openFileDialog1.FileName, utf8wb);
				richTextBox1.Clear();
				while (sr.Peek() > -1)
					richTextBox1.Text += sr.ReadLine().ToString() + System.Environment.NewLine;
				sr.Close();
				this.Text = openFileDialog1.FileName;
				checkedCoding("Кодировка: UTF-8 without BOM", false, false, true);
			}
		}
		
		void UTF8WithoutBOMToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileOpenUTF8wB(); // открываем файл как UTF-8 without BOM
		}
		
		/* Сохранить файл ------------------------------*/
		void fileSave()
		{
			if(toolStripStatusLabel1.Text == "Кодировка: ASCII") fileSaveASCII(false);
			if(toolStripStatusLabel1.Text == "Кодировка: UTF-8") fileSaveUTF8(false);
			if(toolStripStatusLabel1.Text == "Кодировка: UTF-8 without BOM") fileSaveUTF8wBOM(false);
		}
		
		void СохранитьФайлToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileSave(); // сохраняем файл в кодировке по умолчанию
		}
		
		/* Сохранить файл ASCII */
		void fileSaveASCII(bool _saveAs)
		{
			if(_saveAs == true || this.Text == "Catfish Editor v 1.1"){
				if(saveFileDialog1.ShowDialog() == DialogResult.OK){
					richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
					this.Text = saveFileDialog1.FileName;
					checkedCoding("Кодировка: ASCII", true, false, false);
					toolStripStatusLabel4.ForeColor = Color.Black;
					MessageBox.Show("Файл успешно сохранён!");
				}
			}else{
				richTextBox1.SaveFile(this.Text, RichTextBoxStreamType.PlainText);
				toolStripStatusLabel4.ForeColor = Color.Black;
				MessageBox.Show("Файл успешно сохранён!");
			}
			
		}
		
		void ASCIIToolStripMenuItem1Click(object sender, EventArgs e)
		{
			fileSaveASCII(true); // сохраняем файл как ASCII
		}
		
		/* Сохранить файл UTF-8 */
		void fileSaveUTF8(bool _saveAs)
		{
			if(_saveAs == true || this.Text == "Catfish Editor v 1.1"){
				if(saveFileDialog1.ShowDialog() == DialogResult.OK){
					StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8);
					sw.Write(richTextBox1.Text);
					sw.Close();
					this.Text = saveFileDialog1.FileName;
					checkedCoding("Кодировка: UTF-8", false, true, false);
					toolStripStatusLabel4.ForeColor = Color.Black;
					MessageBox.Show("Файл успешно сохранён!");
				}
			}else{
				StreamWriter sw = new StreamWriter(this.Text, false, Encoding.UTF8);
				sw.Write(richTextBox1.Text);
				sw.Close();
				toolStripStatusLabel4.ForeColor = Color.Black;
				MessageBox.Show("Файл успешно сохранён!");
			}
		}
		
		void UTF8ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			fileSaveUTF8(true); // сохраняем файл как UTF-8
		}
		
		/* Сохранить файл UTF-8 without BOM*/
		void fileSaveUTF8wBOM(bool _saveAs)
		{
			if(_saveAs == true || this.Text == "Catfish Editor v 1.1"){
				if(saveFileDialog1.ShowDialog() == DialogResult.OK){
					UTF8Encoding utf8wb = new UTF8Encoding(false);
					StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, utf8wb);
					sw.Write(richTextBox1.Text);
					sw.Close();
					this.Text = saveFileDialog1.FileName;
					checkedCoding("Кодировка: UTF-8 without BOM", false, false, true);
					toolStripStatusLabel4.ForeColor = Color.Black;
					MessageBox.Show("Файл успешно сохранён!");
				}
			}else{
				UTF8Encoding utf8wb = new UTF8Encoding(false);
				StreamWriter sw = new StreamWriter(this.Text, false, utf8wb);
				sw.Write(richTextBox1.Text);
				sw.Close();
				toolStripStatusLabel4.ForeColor = Color.Black;
				MessageBox.Show("Файл успешно сохранён!");
			}
		}
		
		void UTF8WithoutBOMToolStripMenuItem1Click(object sender, EventArgs e)
		{
			fileSaveUTF8wBOM(true); // сохраняем файл как UTF-8 without BOM
		}
		
		/*-----------------------------------------------------------------*/
		
		/* Смена кодировки ------------------------------------------------*/
		void checkedCoding(String _codingName, bool _asciiChecked, bool _utf8Checked, bool _utf8wbChecked)
		{
			toolStripStatusLabel1.Text = _codingName;
			aSCIIToolStripMenuItem2.Checked = _asciiChecked;
			uTF8ToolStripMenuItem2.Checked = _utf8Checked;
			uTF8WithoutBOMToolStripMenuItem2.Checked = _utf8wbChecked;
		}
		void changeEncoding(String _codingName, String _filePath)
		{
			if(_filePath == "Редактор" || _filePath == ""){
				if(_codingName == "ascii") checkedCoding("Кодировка: ASCII", true, false, false);
				if(_codingName == "utf8") checkedCoding("Кодировка: UTF-8", false, true, false);
				if(_codingName == "utf8 without bom") checkedCoding("Кодировка: UTF-8 without BOM", false, false, true);
			}else{
				/* Переоткрываем файл в кодировке ASCII */
				if(_codingName == "ascii"){
					StreamReader sr = new StreamReader(_filePath, System.Text.Encoding.ASCII);
					richTextBox1.Clear();
					richTextBox1.LoadFile(sr.BaseStream, RichTextBoxStreamType.PlainText);
					sr.Close();
					checkedCoding("Кодировка: ASCII", true, false, false);
				}
				/* Переоткрываем файл в кодировке UTF-8 */
				if(_codingName == "utf8"){
					StreamReader sr = new StreamReader(_filePath, System.Text.Encoding.UTF8);
					richTextBox1.Clear();
					richTextBox1.LoadFile(sr.BaseStream, RichTextBoxStreamType.PlainText);
					sr.Close();
					checkedCoding("Кодировка: UTF-8", false, true, false);
				}
				/* Переоткрываем файл в кодировке UTF-8 without BOM */
				if(_codingName == "utf8 without bom"){
					UTF8Encoding utf8wb = new UTF8Encoding(false);
					StreamReader sr = new StreamReader(_filePath, utf8wb);
					richTextBox1.Clear();
					while (sr.Peek() > -1)
						richTextBox1.Text += sr.ReadLine().ToString() + System.Environment.NewLine;
					sr.Close();
					checkedCoding("Кодировка: UTF-8 without BOM", false, false, true);
				}
			}
		}
		
		/* Смена кодировки на ASCII */
		void ASCIIToolStripMenuItem2Click(object sender, EventArgs e)
		{
			changeEncoding("ascii", this.Text);
		}
		void ASCIIToolStripMenuItem3Click(object sender, EventArgs e)
		{
			changeEncoding("ascii", this.Text);
		}
		
		/* Смена кодировки на UTF-8 */
		void UTF8ToolStripMenuItem2Click(object sender, EventArgs e)
		{
			changeEncoding("utf8", this.Text);
		}
		void UTF8ToolStripMenuItem3Click(object sender, EventArgs e)
		{
			changeEncoding("utf8", this.Text);
		}
		
		/* Смена кодировки на UTF-8 without BOM */
		void UTF8WithoutBOMToolStripMenuItem2Click(object sender, EventArgs e)
		{
			changeEncoding("utf8 without bom", this.Text);
		}
		void UTF8WithoutBOMToolStripMenuItem3Click(object sender, EventArgs e)
		{
			changeEncoding("utf8 without bom", this.Text);
		}
				
		
		
		
		/* Панель инструментов (меню файл) ------------------------------- */
		void ToolStripButton1Click(object sender, EventArgs e)
		{
			fileOpen();
		}
		
		void ОткрытьФайлКакASCIIToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileOpenASCII();
		}
		
		void ОткрытьФайлКакUTF8ToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileOpenUTF8();
		}
		
		void ОткрытьФайлКакUTF8WithoutBOMToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileOpenUTF8wB();
		}
		
		void ToolStripButton2Click(object sender, EventArgs e)
		{
			fileSave();
		}
		
		void СохранитьФайлКакASCIIToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileSaveASCII(true);
		}
		
		void СохранитьФайлКакUTF8ToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileSaveUTF8(true);
		}
		
		void СохранитьФайлКакUTF8WithoutBOMToolStripMenuItemClick(object sender, EventArgs e)
		{
			fileSaveUTF8wBOM(true);
		}
		/*-----------------------------------------*/
		
		/*Закрыть файл*/
		void ЗакрытьФайлToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(MessageBox.Show("Сохранить файл перед закрытием?","Вопрос:", MessageBoxButtons.YesNo) == DialogResult.Yes){
				fileSave();
			}
			/* Очистка */
			this.Text = "Редактор";
			richTextBox1.Clear();
		}
		
		/* Правка: Отмена, Повтор, Вырезать, Копировать, Вставить, Удалить */
		void editUndo() // отмена
		{
			richTextBox1.Undo();
		}
		
		void editRedo() // повтор
		{
			richTextBox1.Redo();
		}
		
		void editCut() // вырезать
		{
			richTextBox1.Cut();
		}
		
		void editCopy() // копировать
		{
			richTextBox1.Copy();
		}
		
		void editPaste() // вставить
		{
			richTextBox1.Paste();
		}
		
		void editDelete() // удалить
		{
			Clipboard.SetDataObject("");
			richTextBox1.Paste();
		}
		
		void ОтменитьToolStripMenuItemClick(object sender, EventArgs e)
		{
			editUndo();
		}
		
		void ToolStripButton3Click(object sender, EventArgs e)
		{
			editUndo();
		}
		
		void ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			editUndo();
		}
		
		void ПовторитьToolStripMenuItemClick(object sender, EventArgs e)
		{
			editRedo();
		}
		
		void ToolStripButton4Click(object sender, EventArgs e)
		{
			editRedo();
		}
		
		void ToolStripMenuItem2Click(object sender, EventArgs e)
		{
			editRedo();
		}
		
		void ВырезатьToolStripMenuItemClick(object sender, EventArgs e)
		{
			editCut();
		}
		
		void ToolStripButton5Click(object sender, EventArgs e)
		{
			editCut();
		}
		
		void ToolStripMenuItem3Click(object sender, EventArgs e)
		{
			editCut();
		}
		
		void КопироватьToolStripMenuItemClick(object sender, EventArgs e)
		{
			editCopy();
		}
		
		void ToolStripButton6Click(object sender, EventArgs e)
		{
			editCopy();
		}
		
		void ToolStripMenuItem4Click(object sender, EventArgs e)
		{
			editCopy();
		}
		
		void ВставитьToolStripMenuItemClick(object sender, EventArgs e)
		{
			editPaste();
		}
		
		void ToolStripButton7Click(object sender, EventArgs e)
		{
			editPaste();
		}
		
		void ToolStripMenuItem5Click(object sender, EventArgs e)
		{
			editPaste();
		}
		
		void УдалитьToolStripMenuItemClick(object sender, EventArgs e)
		{
			editDelete();
		}
		
		void ToolStripMenuItem6Click(object sender, EventArgs e)
		{
			editDelete();			
		}
		/*--------------------------------*/
		
		/* Вид: отображение панели инструментов */
		void ПанельИнструментовToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(панельИнструментовToolStripMenuItem.Checked){
				панельИнструментовToolStripMenuItem.Checked = false;
				toolStrip1.Visible = false;
			}else{
				панельИнструментовToolStripMenuItem.Checked = true;
				toolStrip1.Visible = true;
			}
		}
		
		/* СЕРВИС -----------------------------------------------------------*/
		/* Выполнение файл */
		void runBrowser(String _filePath)
		{
			if(_filePath != "Редактор" && _filePath != ""){
				System.Diagnostics.Process.Start(_filePath);
			}
		}
		
		void ВыполнитьВБраузереToolStripMenuItemClick(object sender, EventArgs e)
		{
			runBrowser(this.Text);
		}
		
		void ToolStripButton8Click(object sender, EventArgs e)
		{
			runBrowser(this.Text);
		}
		
		/*DENWER --------------------------------------------------------*/
		void denwerRun(){
			String denwerFile = Config.DenwerPath + "\\denwer\\Run.exe";
			if(File.Exists(denwerFile)){
				System.Diagnostics.Process.Start(denwerFile);
			}else{
				folderBrowserDialog1.Description = "Выберите папку где расположен Denwer." + System.Environment.NewLine + "по умолчаниэ это C:\\WebServers";
				if(folderBrowserDialog1.ShowDialog() == DialogResult.OK){
					Config.DenwerPath = folderBrowserDialog1.SelectedPath;
					denwerFile = Config.DenwerPath + "\\denwer\\Run.exe";
					System.Diagnostics.Process.Start(denwerFile);
				}
			}
		}
		
		void denwerRestart(){
			String denwerFile = Config.DenwerPath + "\\denwer\\Restart.exe";
			if(File.Exists(denwerFile)){
				System.Diagnostics.Process.Start(denwerFile);
			}else{
				folderBrowserDialog1.Description = "Выберите папку где расположен Denwer." + System.Environment.NewLine + "по умолчаниэ это C:\\WebServers";
				if(folderBrowserDialog1.ShowDialog() == DialogResult.OK){
					Config.DenwerPath = folderBrowserDialog1.SelectedPath;
					denwerFile = Config.DenwerPath + "\\denwer\\Restart.exe";
					System.Diagnostics.Process.Start(denwerFile);
				}
			}
		}
			
		void denwerStop(){
			String denwerFile = Config.DenwerPath + "\\denwer\\Stop.exe";
			if(File.Exists(denwerFile)){
				System.Diagnostics.Process.Start(denwerFile);
			}else{
				folderBrowserDialog1.Description = "Выберите папку где расположен Denwer." + System.Environment.NewLine + "по умолчаниэ это C:\\WebServers";
				if(folderBrowserDialog1.ShowDialog() == DialogResult.OK){
					Config.DenwerPath = folderBrowserDialog1.SelectedPath;
					denwerFile = Config.DenwerPath + "\\denwer\\Stop.exe";
					System.Diagnostics.Process.Start(denwerFile);
				}
			}
		}
		
		void denwerPhpMyAdmin(){
			try{
				System.Diagnostics.Process.Start("http://localhost/Tools/phpMyAdmin/");	
			}catch{
				MessageBox.Show("Ошибка обращения к phpMyAdmin!");
			}
		}
		
		void denwerLocalhost(){
			try{
				System.Diagnostics.Process.Start("http://localhost/");	
			}catch{
				MessageBox.Show("Ошибка обращения к локальному серверу (localhost)");
			}
		}
		
		void DenwerСтартToolStripMenuItemClick(object sender, EventArgs e)
		{
			denwerRun();
		}
		
		void DenwerResetToolStripMenuItemClick(object sender, EventArgs e)
		{
			denwerRestart();
		}
		
		void DenwerStopToolStripMenuItemClick(object sender, EventArgs e)
		{
			denwerStop();
		}
		
		void DenwerPhpMyAdminToolStripMenuItemClick(object sender, EventArgs e)
		{
			denwerPhpMyAdmin();
		}
		
		void HttplocalhostToolStripMenuItemClick(object sender, EventArgs e)
		{
			denwerLocalhost();
		}
		
		void ToolStripButton9Click(object sender, EventArgs e)
		{
			denwerRun();
		}
		
		void ToolStripButton10Click(object sender, EventArgs e)
		{
			denwerRestart();
		}
		
		void ToolStripButton11Click(object sender, EventArgs e)
		{
			denwerStop();
		}
		
		void ToolStripButton12Click(object sender, EventArgs e)
		{
			denwerPhpMyAdmin();
		}
		
		/* Код цвета */
		void codeColor()
		{
			if(colorDialog1.ShowDialog() == DialogResult.OK){
				Color clr = colorDialog1.Color;
				String HexColor;
				HexColor = String.Format("#{0:X2}{1:X2}{2:X2}", clr.R, clr.G, clr.B);
				Clipboard.SetDataObject(HexColor);
				richTextBox1.Paste();
			}
		}
		
		void КодЦветаToolStripMenuItemClick(object sender, EventArgs e)
		{
			codeColor();
		}
		
		void ToolStripButton13Click(object sender, EventArgs e)
		{
			codeColor();
		}
		
		/* Настройки ---------------------------------------------*/
		void ШрифтToolStripMenuItemClick(object sender, EventArgs e)
		{
			fontDialog1.Font = richTextBox1.Font;
			if(fontDialog1.ShowDialog() == DialogResult.OK) richTextBox1.Font = fontDialog1.Font;
		}
		
		void ToolStripButton14Click(object sender, EventArgs e)
		{
			fontDialog1.Font = richTextBox1.Font;
			if(fontDialog1.ShowDialog() == DialogResult.OK) richTextBox1.Font = fontDialog1.Font;			
		}
		
		void ЦветToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(colorDialog1.ShowDialog() == DialogResult.OK){
				richTextBox1.ForeColor = colorDialog1.Color;
				textBox1.ForeColor = colorDialog1.Color;
				listBox1.ForeColor = colorDialog1.Color;
			}
		}
		
		void ToolStripButton15Click(object sender, EventArgs e)
		{
			if(colorDialog1.ShowDialog() == DialogResult.OK){
				richTextBox1.ForeColor = colorDialog1.Color;
				textBox1.ForeColor = colorDialog1.Color;
				listBox1.ForeColor = colorDialog1.Color;
			}
		}
		
		void ЦветФонаToolStripMenuItemClick(object sender, EventArgs e)
		{
			if(colorDialog1.ShowDialog() == DialogResult.OK){
				richTextBox1.BackColor = colorDialog1.Color;
				textBox1.BackColor = colorDialog1.Color;
				listBox1.BackColor = colorDialog1.Color;
			}
		}
		
		void ToolStripButton16Click(object sender, EventArgs e)
		{
			if(colorDialog1.ShowDialog() == DialogResult.OK){
				richTextBox1.BackColor = colorDialog1.Color;
				textBox1.BackColor = colorDialog1.Color;
				listBox1.BackColor = colorDialog1.Color;
			}
		}
		
		/* Поиск по тексту */
		void findText(ToolStripComboBox _cbox)
		{
			try{
				bool resolution = true;
				for(int k = 0; k < _cbox.Items.Count; k++)
					if(_cbox.Items[k].ToString() == _cbox.Text) resolution = false;
				if(resolution) _cbox.Items.Add(_cbox.Text);
				if(_findText != _cbox.Text){
					_findIndex = 0;
					_findLast = 0;
					_findText = _cbox.Text;
				}
				if(richTextBox1.Find(_cbox.Text, _findIndex, richTextBox1.TextLength - 1, RichTextBoxFinds.None) >= 0){
					richTextBox1.Select();
					_findIndex = richTextBox1.SelectionStart + richTextBox1.SelectionLength;
					if(_findLast == richTextBox1.SelectionStart){
						MessageBox.Show("Поиск завершен.", "Сообщение:", MessageBoxButtons.OK);
						_findIndex = 0;
						_findLast = 0;
						_findText = _cbox.Text;
					}else{
						_findLast = richTextBox1.SelectionStart;
					}
				}else{
					MessageBox.Show("Поиск завершен.", "Сообщение:", MessageBoxButtons.OK);
					_findIndex = 0;
					_findLast = 0;
					_findText = _cbox.Text;
				}
					
			}catch{
				MessageBox.Show("Во время поиска произошла ошибка!", "Ошибка!!!", MessageBoxButtons.OK);	
			}
		}
		
		void ToolStripComboBox1KeyPress(object sender, KeyPressEventArgs e)
		{
			if(e.KeyChar.GetHashCode().ToString() == "851981"){
				findText(toolStripComboBox1);
			}
		}
		
		void ToolStripButton17Click(object sender, EventArgs e)
		{
			findText(toolStripComboBox1);
		}
		
		/* О программе */
		void ОПрограммеToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show("Программа: Catfish Editor" + System.Environment.NewLine + "Версия: 1.1" + System.Environment.NewLine + "Автор: Сомов Евгений Павлович" + System.Environment.NewLine + "©  Somov Evgeniy, 2014-2015", "О программе", MessageBoxButtons.OK);
		}
		
		void ToolStripButton19Click(object sender, EventArgs e)
		{
			MessageBox.Show("Программа: Catfish Editor" + System.Environment.NewLine + "Версия: 1.1" + System.Environment.NewLine + "Автор: Сомов Евгений Павлович" + System.Environment.NewLine + "©  Somov Evgeniy, 2014-2015", "О программе", MessageBoxButtons.OK);			
		}
		
		/* Выделить всё */
		void ВыделитьВсёToolStripMenuItemClick(object sender, EventArgs e)
		{
			richTextBox1.SelectAll();
		}
		
		void ВыделитьВсёToolStripMenuItem1Click(object sender, EventArgs e)
		{
			richTextBox1.SelectAll();
		}
		
		/* Создать файл */
		void СоздатьФайлToolStripMenuItemClick(object sender, EventArgs e)
		{
			Editor fEditor = new Editor();
			fEditor.Show();
		}
		
		void ToolStripButton18Click(object sender, EventArgs e)
		{
			Editor fEditor = new Editor();
			fEditor.Show();
		}
		
		void HtmlToolStripMenuItemClick(object sender, EventArgs e)
		{
			htmlToolStripMenuItem.Checked = true;
			cssToolStripMenuItem.Checked = false;
			javascriptToolStripMenuItem.Checked = false;
			phpToolStripMenuItem.Checked = false;
			loadOperators("html");
		}
		
		void CssToolStripMenuItemClick(object sender, EventArgs e)
		{
			htmlToolStripMenuItem.Checked = false;
			cssToolStripMenuItem.Checked = true;
			javascriptToolStripMenuItem.Checked = false;
			phpToolStripMenuItem.Checked = false;
			loadOperators("css");			
		}
		
		void JavascriptToolStripMenuItemClick(object sender, EventArgs e)
		{
			htmlToolStripMenuItem.Checked = false;
			cssToolStripMenuItem.Checked = false;
			javascriptToolStripMenuItem.Checked = true;
			phpToolStripMenuItem.Checked = false;
			loadOperators("javascript");			
		}
		
		void PhpToolStripMenuItemClick(object sender, EventArgs e)
		{
			htmlToolStripMenuItem.Checked = false;
			cssToolStripMenuItem.Checked = false;
			javascriptToolStripMenuItem.Checked = false;
			phpToolStripMenuItem.Checked = true;
			loadOperators("php");			
		}
	}
}
