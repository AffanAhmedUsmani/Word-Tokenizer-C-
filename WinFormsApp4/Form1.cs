using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp4
{
    public partial class Form1 : Form
    {  
        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            System.IO.StringReader OpenFile = new StringReader(openFileDialog1.FileName);
            textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
            tokenise(sender, e);
            tokeniseSentence(sender, e);
            OpenFile.Close();


        }

     


    private void tokenise(object sender ,EventArgs e)
        {

            //starting tokeniser
        string tokenisearray = textBox1.Text.ToString();
            int len1 = tokenisearray.Length;
            // it takes the length of the document to be the maximum number of words dynamic initialisation
            // maximum index number of string array 
            string[] arr = new string[len1];

            //string array initialized for storing tokenised words
            int len2 = 0;
            // index number of string array start
            for (int i = 0; i < tokenisearray.Length; i++)

            {//loop through every charecter of the string in which txt file is imported
                
                
                    if (tokenisearray[i] == ' ' || tokenisearray[i] == ',' )
                    {
                    //check for word end to start new word either there is a space b/w two words or a comma 
                    len2++;
                    
                    }

                
                else
                {
                    //here I have creted an append type thing in which I am seprating each word 
                    arr[len2] = arr[len2] + tokenisearray[i];
                   
                }
            }
            // this distinct().to array() will remove all the repeating words 
            string[] dist = arr.Distinct().ToArray();

            string separator = ", ";
            // I am loading my stop words dictionary from the destination of my project
            if (checkBox1.Checked== true)
            { //for with stop words 
                string text = File.ReadAllText("stopwordsdictionary.txt");

                string[] dictionary = new string[127];
                int j = 0;
                for (int i = 0; i < text.Length; i++)

                {


                    if (text[i] == ' ' || text[i] == ',')
                    {

                        j++;

                    }


                    else
                    {
                        dictionary[j] = dictionary[j] + text[i];

                    }
                }


                var final = dist.Except(dictionary).ToArray();
                textBox2.Text = final.Length.ToString();
                textBox4.Text = string.Join(separator, final);

            }
            else if(checkBox1.Checked== false)
            {
                //without filtering stop words
                textBox2.Text = dist.Length.ToString();
                textBox4.Text = string.Join(separator, dist);

            }

        }

        private void tokeniseSentence(object sender, EventArgs e)
        {

            //senetnce tokeinsation
            string tokenisearray = textBox1.Text.ToString();
            int len1 = tokenisearray.Length;
            len1 = len1 / 4;
            string[] arr = new string[len1];
            int len2 = 0;
            for (int i = 0; i < tokenisearray.Length; i++)

            {


                if (tokenisearray[i] == '.' )
                {
                    len2++;
                }


                else
                {
                    arr[len2] = arr[len2] + tokenisearray[i];

                }
            }
            string separator = ", ";
            len2++;
            textBox5.Text = len2.ToString();
            textBox3.Text = string.Join(separator, arr);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new();
            //to save the tokenised word file

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                string name1 = @saveFileDialog1.FileName;
                FileStream stream = null;
                try
                {
                    // Create a FileStream with mode CreateNew  
                    stream = new FileStream(name1, FileMode.OpenOrCreate);
                    // Create a StreamWriter from FileStream  
                    using (StreamWriter writer = new(stream, Encoding.UTF8))
                    {
                        writer.WriteLine(textBox4.Text);
                    }
                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }

            }


        }

        private void button3_Click(object sender, EventArgs e)
        { //to save the tokenised sentence file 
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                string name1 = @saveFileDialog1.FileName;
                FileStream stream = null;
                try
                {
                    // Create a FileStream with mode CreateNew  
                    stream = new FileStream(name1, FileMode.OpenOrCreate);
                    // Create a StreamWriter from FileStream  
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        writer.WriteLine(textBox3.Text);
                    }
                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
