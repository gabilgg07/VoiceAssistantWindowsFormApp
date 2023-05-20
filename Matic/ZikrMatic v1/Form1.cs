using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;

namespace ZikrMatic_v1
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();
        System.Media.SoundPlayer music = new System.Media.SoundPlayer();
        public Form1()
        {
            InitializeComponent();

            Choices choices = new Choices();
            string[] text = File.ReadAllLines(Environment.CurrentDirectory + "//grammar.txt");
            choices.Add(text);
            Grammar grammar = new Grammar(new GrammarBuilder(choices));
            recEngine.LoadGrammar(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recEngne_SpechRecognized);

            speech.SelectVoiceByHints(VoiceGender.Male);
        }
        public int count = 0;
        private void recEngne_SpechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string result = e.Result.Text;
            resultLbl.Text += result + ", ";

            if (result == "Allah" || result == "Hello Kabil" || result == "What time is it"|| result == "Open google"|| result == "Open my gmail"||
                result == "Close Google Chrome"|| result == "Open my folder"|| result == "Close My Folder"|| result == "Play My Music"||
                result == "Stop"|| result == "Shut down")
            {
                if (result == "Hello Kabil")
                {
                    result = "Hi, how can I help you?";
                }

                if (result == "What time is it")
                {
                    result = "It is currently" + DateTime.Now.ToLongTimeString();
                }

                if (result == "Open google")
                {
                    System.Diagnostics.Process.Start("https://www.google.com/");
                    result = "Google Opening";
                }

                if (result == "Open my gmail")
                {
                    System.Diagnostics.Process.Start("https://mail.google.com/mail/u/0/#inbox");
                    result = "Gmail Opening";
                }

                if (result == "Close Google Chrome")
                {
                    System.Diagnostics.Process[] close = System.Diagnostics.Process.GetProcessesByName("chrome");
                    foreach (System.Diagnostics.Process p in close)
                    {
                        p.Kill();
                    }
                    result = "Chrome closing";
                }

                if (result == "Open my folder")
                {
                    System.Diagnostics.Process.Start(@"D:\Codes\gitProjects");
                    result = "Folder Opening";
                }

                if (result == "Close My Folder")
                {
                    System.Diagnostics.Process[] close = System.Diagnostics.Process.GetProcessesByName("explorer");
                    foreach (System.Diagnostics.Process p in close)
                    {
                        p.Kill();
                    }
                    result = "Folder closing";
                }

                if (result == "Play My Music")
                {
                    music.SoundLocation = @"C:\Users\user\Desktop\[MP3DOWNLOAD.TO] [OFFICIAL VIDEO] Hallelujah - Pentatonix-HQ.wav";
                    music.Play();
                    result = "";
                }

                if (result == "Stop")
                {
                    speech.SpeakAsyncCancelAll();
                    music.Stop();
                    result = "";
                }

                if (result == "Shut up")
                {
                    speech.SpeakAsyncCancelAll();
                }

                if (result == "Shut down")
                {
                    Application.Exit();
                }

                speech.SpeakAsync(result);
                label2.Text = result;
            }
            else if (result == "ALLAH")
            {
                count++;
                result = count.ToString();
                label2.Text = result;
            }
            else
            {
                speech.SpeakAsyncCancelAll();
            }
        }
    }
}
