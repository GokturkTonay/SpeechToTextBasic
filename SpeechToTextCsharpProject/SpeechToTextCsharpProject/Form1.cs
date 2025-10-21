using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace SpeechToTextCsharpProject
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

        public Form1()
        {
            InitializeComponent();

            try
            {
                recognizer.SetInputToDefaultAudioDevice();

                recognizer.LoadGrammar(new DictationGrammar());

                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                btnDinleDur.Enabled = false;
            }
            catch (Exception ex)
            {
                // Eğer Windows'ta Konuşma Tanıma dil paketi yüklü değilse burada hata alınır.
                MessageBox.Show("Ses tanıma motoru başlatılamadı. \n\nLütfen Windows ayarlarınızdan Türkçe 'Konuşma Tanıma' paketinin yüklü olduğundan emin olun.\n\nHata Detayı: " + ex.Message);
                btnDinleBasla.Enabled = false;
                btnDinleDur.Enabled = false;
            }
        }

        private void btnDinleBasla_Click(object sender, EventArgs e)
        {
            recognizer.RecognizeAsync(RecognizeMode.Multiple);

            // Butonların kullanılabilirlik durumunu ayarla.
            btnDinleBasla.Enabled = false;
            btnDinleDur.Enabled = true;
        }

        private void btnDinleDur_Click(object sender, EventArgs e)
        {
            recognizer.RecognizeAsyncStop();

            // Butonların kullanılabilirlik durumunu eski haline getir.
            btnDinleBasla.Enabled = true;
            btnDinleDur.Enabled = false;
        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Arayüzdeki bir kontrolü (TextBox) güvenli bir şekilde güncellemek için 'Invoke' kullanmak zorunludur.
            this.Invoke(new Action(() =>
            {
                txtMetin.Text += e.Result.Text + " ";
            }));
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
