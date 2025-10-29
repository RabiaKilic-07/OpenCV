using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using System;
using System.IO;

namespace OpenCV // Projenizin ad alan� (Namespace)
{
    public partial class Form1 : Form
    {
        // === 1. SINIF SEV�YES�NDE UZMAN NESNELER�N TANIMLANMASI ===
        // Uyar�y� gidermek i�in 'null!' eklendi, b�ylece kod C# diline daha uygun hale geldi.
        private CascadeClassifier yuzS�n�fland�r�c� = null!;
        private VideoCapture capture = null!;


        public Form1()
        {
            InitializeComponent();

            timer1.Tick += new EventHandler(Timer_Tick);

            try
            {
                // Uygulaman�n �al��t��� tam dizini (bin/Debug) al�p dosya ad�yla birle�tirir.
                string dosyaYolu = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");

                // OOP: CascadeClassifier nesnesini olu�turma
                yuzS�n�fland�r�c� = new CascadeClassifier(dosyaYolu);

                lblDurum.Text = "Sistem Haz�r (XML Y�klendi).";
            }
            catch (Exception ex)
            {
                lblDurum.Text = "HATA: S�n�fland�r�c� Y�klenemedi! (Dosya Yolu Kontrol Edildi)";
                MessageBox.Show("S�n�fland�r�c� Y�kleme Hatas�: " + ex.Message);
            }
        }

        // === 2. BUTON METODU (��LEM� BA�LAT/DURDUR) ===
        private void btnBaslat_Click(object sender, EventArgs e)
        {
            if (capture == null) // Ba�latma ��lemi
            {
                // OOP: VideoCapture nesnesini olu�turma
                capture = new VideoCapture(0);
                timer1.Start();
                lblDurum.Text = "Kamera Ba�lat�ld�, Y�z Tan�ma Aktif.";
                btnBaslat.Text = "Durdur";
            }
            else // Durdurma ��lemi
            {
                timer1.Stop();
                capture.Dispose(); // OOP: Kaynaklar� serbest b�rakma metodu
                capture = null!;   // De�i�keni null olarak i�aretle
                imageBox1.Image = null;
                lblDurum.Text = "Durduruldu.";
                btnBaslat.Text = "Kameray� Ba�lat/Durdur";
            }
        }

        // === 3. ZAMANLAYICI METODU (ANA ��LEM D�NG�S�) ===
        private void Timer_Tick(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            capture.Retrieve(frame); // capture nesnesinden g�ncel kareyi �ek

            if (frame.IsEmpty) return;

            // OOP: G�r�nt� nesnesine d�n��t�rme
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();

            // OOP: Gri Ton nesnesine d�n��t�rme
            Image<Gray, byte> griImage = image.Convert<Gray, byte>();

            // OOP: Uzman nesnenin ana tan�ma metodunu �a��rma
            Rectangle[] bulunanYuzler = yuzS�n�fland�r�c�.DetectMultiScale(
                griImage,
                1.1,
                10,
                Size.Empty
            );

            // Bulunan nesneleri �izme ve etiketleme
            foreach (Rectangle yuz in bulunanYuzler)
            {
                // OOP: Image nesnesinin Draw metodu �a�r�l�r
                image.Draw(yuz, new Bgr(0, 255, 0), 2);

                // OOP: Nesneye ait etiketi yazma
                string etiket = "INSAN";
                CvInvoke.PutText(image, etiket, new Point(yuz.X, yuz.Y - 10),
                                 FontFace.HersheySimplex, 0.8, new Bgr(0, 255, 0).MCvScalar, 2);
            }

            // Sonucu ImageBox'ta g�sterme
            imageBox1.Image = image;
        }

    } // FORM SINIFI KAPANI�I
} // NAMESPACE KAPANI�I