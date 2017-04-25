using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private String currentFile = "";
        private Boolean savedChanges = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadConfig();
        }
        private void loadConfig()
        {
            try
            {
                XmlDocument config = new XmlDocument();
                config.Load("config/config.xml");

                //Load Font
                XmlNode fontName = config.SelectSingleNode("/data/font-data/font-name/text()");
                XmlNode fontType = config.SelectSingleNode("/data/font-data/font-type/text()");
                XmlNode fontSize = config.SelectSingleNode("/data/font-data/font-size/text()");

                if (fontName == null || fontName.Value == null)
                {
                    throw new Exception("The font-name parameter in config.xml was not set properly");
                }

                if (fontSize == null || fontSize.Value == null)
                {
                    throw new Exception("The font-size parameter in config.xml was not set properly");
                }
                if (fontType != null && fontType.Value != "italic" && fontType.Value != "bold")
                {
                    throw new Exception("The font-type parameter in config.xml was not set properly");
                }

                String font = "";
                if (fontType == null)
                {
                    font = String.Format("{0},{1}", fontName.Value, fontSize.Value);
                }
                else
                {
                    font = String.Format("{0},{1},style={2}", fontName.Value, fontSize.Value, fontType.Value);
                }
                FontConverter fontcvtr = new FontConverter();
                textBox1.Font = fontcvtr.ConvertFromString(font) as Font;
            }
            catch (Exception ex)
            {
                var errorBox = MessageBox.Show(ex.Message, "config.xml Error",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
            }
            return;
        }
       

        /// <summary>
        /// 
        /// </summary>
        private void saveToCurrentFile()
        {
            File.WriteAllText(currentFile, textBox1.Text);
            savedChanges = true;
            setTitle();
            return;
        }
        //Save Dialogs
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentFile.Equals("") || currentFile == null)
            {
                saveFileDialog.ShowDialog();
            }
            else
            {
                saveToCurrentFile();
            }
            return;
            
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (currentFile.Equals("") || currentFile == null)
            {
                saveFileDialog.ShowDialog();
            }
            else
            {
                saveToCurrentFile();
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            SaveFileDialog sfDialog = (SaveFileDialog)sender;
            setCurrentFile(sfDialog.FileName);
            saveToCurrentFile();
            return;
        }

 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofDialog = (OpenFileDialog)sender;
            setCurrentFile(ofDialog.FileName);
            textBox1.Text = File.ReadAllText(ofDialog.FileName);
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.SelectedText = "";
            return;
        }

        private void CutButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.SelectedText = "";
            return;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
        }
        /// <summary>
        /// When key is pressed, add * to title to notify the user that they have unsaved changes
        /// </summary>
        private void Key_Typed(object sender, EventArgs e)
        {
            savedChanges = false;
            setTitle();
        }

        public String getCurrentFile()
        {
            return currentFile;
        }
        public void setCurrentFile(String filename)
        {
            currentFile = filename;
            setTitle();
            return;
        }

        private void setTitle()
        {
            this.Text = String.Format("Text Editor - {0}{1}", currentFile, (savedChanges ? "" : "*"));
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pasteButton_Click(object sender, EventArgs e)
        {

        }
    }
}
