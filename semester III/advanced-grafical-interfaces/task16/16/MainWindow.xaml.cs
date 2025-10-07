using System;
using System.IO;
using System.Speech.Synthesis;
using System.Windows;
using Microsoft.Win32;

namespace SpeechSynthesizerApp
{
    public partial class MainWindow : Window
    {
        private SpeechSynthesizer synthesizer;

        public MainWindow()
        {
            InitializeComponent();
            synthesizer = new SpeechSynthesizer();
            synthesizer.SelectVoice("Microsoft Paulina Desktop");
        }

        private void BtnReadText_Click(object sender, RoutedEventArgs e)
        {
            string text = txtInput.Text;
            if (!string.IsNullOrEmpty(text))
            {
                synthesizer.SpeakAsync(text);
            }
        }

        private void BtnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Pliki tekstowe (*.txt)|*.txt"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string text = File.ReadAllText(openFileDialog.FileName);
                txtInput.Text = text;
                synthesizer.SpeakAsync(text);
            }
        }
    }
}
