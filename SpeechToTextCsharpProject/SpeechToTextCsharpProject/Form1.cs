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
using Vosk;
using NAudio.Wave;
using System.Threading;

namespace SpeechToTextCsharpProject
{
    public partial class Form1 : Form
    {
        private const string ModelPath = "model";

        private VoskRecognizer recognizer;
        private WaveInEvent waveIn; // NAudio'dan mikrofon dinleyicisi

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDinleBasla_Click(object sender, EventArgs e)
        {
            if (waveIn == null) return;

            try
            {
                // Mikrofon kaydını BAŞLAT
                waveIn.StartRecording();
                UpdateButtons(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Mikrofon başlatılamadı: {ex.Message}");
            }
        }

        private void btnDinleDur_Click(object sender, EventArgs e)
        {
            if (waveIn == null) return;

            try
            {
                // Mikrofon kaydını DURDUR
                waveIn.StopRecording();
                UpdateButtons(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Durdurma hatası: {ex.Message}");
            }
        }

        private void UpdateTextBox(string text)
        {
            if (txtMetin.InvokeRequired)
            {
                txtMetin.Invoke(new Action(() => UpdateTextBox(text)));
            }
            else
            {
                txtMetin.Text += text;
            }
        }

        private void UpdateButtons(bool dinlemeBaslaAktif)
        {
            if (btnDinleBasla.InvokeRequired)
            {
                btnDinleBasla.Invoke(new Action(() => UpdateButtons(dinlemeBaslaAktif)));
            }
            else
            {
                btnDinleBasla.Enabled = dinlemeBaslaAktif;
                btnDinleDur.Enabled = !dinlemeBaslaAktif;
            }
        }

        private string ParseVoskResult(string json, string key = "text")
        {
            try
            {
                // Basit string ayıklama (JSON kütüphanesi eklememek için)
                string keyPattern = $"\"{key}\" : \"";
                int startIndex = json.IndexOf(keyPattern);
                if (startIndex == -1) return ""; // Anahtar bulunamadı

                startIndex += keyPattern.Length;
                int endIndex = json.IndexOf("\"", startIndex);
                if (endIndex == -1) return ""; // Kapanış tırnağı bulunamadı

                return json.Substring(startIndex, endIndex - startIndex);
            }
            catch
            {
                return ""; // Hata durumunda boş dön
            }
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // Gelen ham ses verisini (byte array) Vosk'a besle
            if (recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
            {
                // AcceptWaveform true dönerse, bu TAM bir sonuçtur (konuşma durdu)
                string fullResult = recognizer.FinalResult();
                // Vosk sonucu JSON formatında verir, ör: { "text" : "hello world" }
                // Basit bir şekilde metni almak için "text" kısmını arayabiliriz (daha sağlamı JSON parse etmektir)
                string textResult = ParseVoskResult(fullResult);
                UpdateTextBox(textResult + " ");
            }
            else
            {
                // AcceptWaveform false dönerse, bu KISMİ bir sonuçtur (konuşma devam ediyor)
                string partialResult = recognizer.PartialResult();
                string partialText = ParseVoskResult(partialResult, "partial");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Vosk Modelini yükle
                // Model() sınıfı, "model" klasörünün yolunu ister.
                Model model = new Model(ModelPath);

                // 2. Tanıyıcıyı (Recognizer) bu modelle oluştur.
                // 16000 Hz örneklem hızını belirttiğimize dikkat et (NAudio ile aynı olmalı)
                recognizer = new VoskRecognizer(model, 16000.0f);

                // 3. NAudio Mikrofonunu Ayarla
                waveIn = new WaveInEvent();
                waveIn.WaveFormat = new WaveFormat(16000, 1); // 16kHz, Mono

                // 4. Mikrofondan ses geldikçe (DataAvailable) ne yapılacağını ata
                waveIn.DataAvailable += WaveIn_DataAvailable;

                // 5. Başlangıç durumu
                btnDinleDur.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Model yüklenemedi. 'model' klasörünü kontrol edin. \nHata: {ex.Message}", "Vosk Hatası");
                btnDinleBasla.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
            if (recognizer != null)
            {
                recognizer.Dispose();
                recognizer = null;
            }
        }
    }
}
