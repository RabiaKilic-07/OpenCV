using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using System;
using System.IO;

namespace OpenCV // Projenizin ad alaný (Namespace)
{
    public partial class Form1 : Form
    {
        // === 1. SINIF SEVÝYESÝNDE UZMAN NESNELERÝN TANIMLANMASI ===
        // Uyarýyý gidermek için 'null!' eklendi, böylece kod C# diline daha uygun hale geldi.
        private CascadeClassifier yuzSýnýflandýrýcý = null!;
        private VideoCapture capture = null!;


        public Form1()
        {
            InitializeComponent();

            timer1.Tick += new EventHandler(Timer_Tick);

            try
            {
                // Uygulamanýn çalýþtýðý tam dizini (bin/Debug) alýp dosya adýyla birleþtirir.
                string dosyaYolu = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");

                // OOP: CascadeClassifier nesnesini oluþturma
                yuzSýnýflandýrýcý = new CascadeClassifier(dosyaYolu);

                lblDurum.Text = "Sistem Hazýr (XML Yüklendi).";
            }
            catch (Exception ex)
            {
                lblDurum.Text = "HATA: Sýnýflandýrýcý Yüklenemedi! (Dosya Yolu Kontrol Edildi)";
                MessageBox.Show("Sýnýflandýrýcý Yükleme Hatasý: " + ex.Message);
            }
        }

        // === 2. BUTON METODU (ÝÞLEMÝ BAÞLAT/DURDUR) ===
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (capture == null) // Baþlatma Ýþlemi
            {
                // OOP: VideoCapture nesnesini oluþturma
                capture = new VideoCapture(0);
                timer1.Start();
                lblDurum.Text = "Kamera Baþlatýldý, Yüz Tanýma Aktif.";
                btnBaslat.Text = "Durdur";
            }
            else // Durdurma Ýþlemi
            {
                timer1.Stop();
                capture.Dispose(); // OOP: Kaynaklarý serbest býrakma metodu
                capture = null!;   // Deðiþkeni null olarak iþaretle
                imageBox1.Image = null;
                lblDurum.Text = "Durduruldu.";
                btnBaslat.Text = "Kamerayý Baþlat/Durdur";
            }
        }

        // === 3. ZAMANLAYICI METODU (ANA ÝÞLEM DÖNGÜSÜ) ===
        private void Timer_Tick(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            capture.Retrieve(frame); // capture nesnesinden güncel kareyi çek

            if (frame.IsEmpty) return;

            // OOP: Görüntü nesnesine dönüþtürme
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();

            // OOP: Gri Ton nesnesine dönüþtürme
            Image<Gray, byte> griImage = image.Convert<Gray, byte>();

            // OOP: Uzman nesnenin ana tanýma metodunu çaðýrma
            Rectangle[] bulunanYuzler = yuzSýnýflandýrýcý.DetectMultiScale(
                griImage,
                1.1,
                10,
                Size.Empty
            );

            // Bulunan nesneleri çizme ve etiketleme
            foreach (Rectangle yuz in bulunanYuzler)
            {
                // OOP: Image nesnesinin Draw metodu çaðrýlýr
                image.Draw(yuz, new Bgr(0, 255, 0), 2);

                // OOP: Nesneye ait etiketi yazma
                string etiket = "INSAN";
                CvInvoke.PutText(image, etiket, new Point(yuz.X, yuz.Y - 10),
                                 FontFace.HersheySimplex, 0.8, new Bgr(0, 255, 0).MCvScalar, 2);
            }

            // Sonucu ImageBox'ta gösterme
            imageBox1.Image = image;
        }

    } // FORM SINIFI KAPANIÞI
} // NAMESPACE KAPANIÞI