/* David Laughton
 * April 23rd 2019
 * Hangman game pulls words from textfiles.
 */

using System;
using System.Linq;
using System.Windows;

namespace U3_David_Summative
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string outputwordrandom;
        int lives = 9;
        //strings of output for both of the levels of words
        string[] output8 = new string[] { "_", " ", "_", " ", "_", " ", "_", " ", "_", " ", "_", " ", "_", " ", "_" };
        string[] output4 = new string[] { "_", " ", "_", " ", "_", " ", "_" };

        public MainWindow()
        {
            InitializeComponent();
        }
        //button to intailize
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //make them enter a character
            if (ltrInput.Text == "")
            {
                MessageBox.Show("Please type a character");
            }
            else if ((bool)btnEasy.IsChecked)
            {
                OutputLettersOnUnderline.Text = "";
                //Input character
                char ltrEntered = ltrInput.Text[0];
                guessedLtrs.Text += ltrEntered;
                if (outputwordrandom.Contains(ltrEntered))
                {
                    int sp = 0;
                    int where = 0;
                    int[] Index = new int[0];
                    while (sp < outputwordrandom.Length && outputwordrandom.IndexOf(ltrEntered, sp) >= 0)
                    {
                        Array.Resize(ref Index, Index.Length + 1);
                        if (outputwordrandom.IndexOf(ltrEntered, sp) >= 0)
                        {
                            where = outputwordrandom.IndexOf(ltrEntered, sp);
                            Index[sp] = where;
                            output4[Index[sp] * 2] = ltrEntered.ToString();
                        }
                        sp++;
                    }
                }
                else
                {
                    //Method for lost lives
                    LivesLost();
                }
                for (int i = 0; i < output4.Length; i++)
                {
                    OutputLettersOnUnderline.Text += output4[i];
                }
            }
            else if ((bool)btnHard.IsChecked)
            {
                OutputLettersOnUnderline.Text = "";
                //Input character
                char ltrEntered = ltrInput.Text[0];
                guessedLtrs.Text += ltrEntered;
                if (outputwordrandom.Contains(ltrEntered))
                {
                    int sp = 0;
                    int where = 0;
                    int[] Index = new int[0];
                    while (sp < outputwordrandom.Length && outputwordrandom.IndexOf(ltrEntered, sp) >= 0)
                    {
                        Array.Resize(ref Index, Index.Length + 1);
                        if (outputwordrandom.IndexOf(ltrEntered, sp) >= 0)
                        {
                            where = outputwordrandom.IndexOf(ltrEntered, sp);
                            Index[sp] = where;
                            output8[Index[sp] * 2 ] = ltrEntered.ToString();
                        }
                        sp++;
                    }                    
                }
                else
                {
                    //Method for lost lives
                    LivesLost();
                }
                for (int i = 0; i < output8.Length; i++)
                {
                    OutputLettersOnUnderline.Text += output8[i];
                }
            }            
            else
            {
                MessageBox.Show("Please select a difficulty");
            }
            //Deletes text
            ltrInput.Text = "";
        }

        private void LivesLost()
        {
            //Lives of hearts 
            string hearts = lives.ToString();
            string heartDisplay = numOfLives.Text;
            numOfLives.Text = numOfLives.Text.Remove(heartDisplay.Length - 1, 1);
            //live number changing
            lives--;            
            //if the lives equal zero show them the word
            if (lives == 0)
            {
                MessageBox.Show("You ran out of lives, " + "word was: " + outputwordrandom);
            }
        }

        public void BtnHard_Click(object sender, RoutedEventArgs e)
        {
            //start output
            OutputLettersOnUnderline.Text = "_ _ _ _ _ _ _ _";
            int randomnumber = randomNumberMethod();
            try
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader("8letterwords.txt"))
                {
                    outputwordrandom = "";
                    //random word
                    string[] words = sr.ReadToEnd().Split('\n');
                    outputwordrandom = words[randomnumber];
                    //Change in the names[] for 0 for first names and 1 for last name
                    sr.Close();
                    // test MessageBox.Show(words[randomnumber]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void BtnEasy_Click(object sender, RoutedEventArgs e)
        {
            //start output
            OutputLettersOnUnderline.Text = "_ _ _ _";
            int randomnumber = randomNumberMethod();
            try
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader("4letterwords.txt"))
                {
                    outputwordrandom = "";
                    //random word
                    string[] words = sr.ReadToEnd().Split('\n');
                    outputwordrandom = words[randomnumber];
                    //Change in the names[] for 0 for first names and 1 for last name
                    sr.Close();
                    // test MessageBox.Show(words[randomnumber]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private static int randomNumberMethod()
        {
            //word generation for easy + hard
            int randomnumber;
            //generates random number within 0-20
            Random r = new Random(1922479367);
            randomnumber = r.Next(20);
            return randomnumber;
        }
        //hint button
        public void btnHint_Click(object sender, RoutedEventArgs e)
        {
            string output = OutputLettersOnUnderline.Text;
            if (output[0].ToString() == "_")
            {
                MessageBox.Show("hint! " + outputwordrandom[0] + " is the first letter.");
            }
            else
            {
                MessageBox.Show("You're close enough! You dont need a hint.");
            }
        }
    }
}