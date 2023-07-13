using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Traffic_Racer_2D
{
    public partial class TR2 : Form
    {
        public TR2()
        {
            InitializeComponent();
        }



        private void label23_Click(object sender, EventArgs e)
        {

        }

        int SeritSayisi = 1, Road1 = 0, Speed1 = 70;
        int SeritSayisi2 = 4, Road2 = 0, Speed2 = 70;
        Random R1 = new Random();  //araba resmi getirmek için random 
        Random R2 = new Random(); 
       
        class Random_Car1   //1.oyuncu random araç sınıfı
        {
            public bool SahteAraba1 = false;
            public PictureBox Arabalar1;
            public bool vakit = false;
        }

        class Random_Car2 //2.oyuncu
        {
            public bool SahteAraba2 = false;
            public PictureBox Arabalar2;
            public bool vakit2 = false;
        }

        Random_Car1[] rndCar1 = new Random_Car1[2];  //sınıfı çağırdık ve dizi oluşturduk
        Random_Car2[] rndCar2 = new Random_Car2[2];

        //1.OYUNCU RANDOM ARABA RESMİ SEÇME FONKSİYONU
        void RandomCarGetir1 (PictureBox pb) 

        {
            int rnd1 = R1.Next(1, 5);//1 - 5 olmasının nedeni car1 car2...

            switch (rnd1) 
            {
                case 1:
                    pb.Image = Properties.Resources.car1;
                    break;
                case 2:
                    pb.Image = Properties.Resources.car2;
                    break;
                case 3:
                    pb.Image = Properties.Resources.car3;
                    break;
                case 4:
                    pb.Image = Properties.Resources.car4;
                    break;
            }

            pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        //2.OYUNCU RANDOM ARABA RESMİ SEÇME FONKSYİONU
        void RandomCarGetir2(PictureBox pb)
        {
            int rnd2 = R2.Next(1, 5);//1 - 5 car1 car2... 

            switch (rnd2) //2. oyuncu için switch case yapısı
            {
                case 1:
                    pb.Image = Properties.Resources.car2;
                    break;
                case 2:
                    pb.Image = Properties.Resources.car3;
                    break;
                case 3:
                    pb.Image = Properties.Resources.car1;
                    break;
                case 4:
                    pb.Image = Properties.Resources.car4;
                    break;
            }

            pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void AracYerine1()
        {  //2.oyuncu arabasının konum değiştirme fonksiyonu
            if (SeritSayisi == 1)
            {
                araba2.Location = new Point(1220, 458);
            }
            else if (SeritSayisi == 0)
            {
                araba2.Location = new Point(950, 458);
            }
            else if (SeritSayisi == 2)
            {
                araba2.Location = new Point(1460, 458);
            }
        }
        private void AracYerine2()
        {  //1.oyuncu arabasının konum değiştirme fonksiyonu
            if (SeritSayisi2 == 3)
            {
                araba1.Location = new Point(663, 458);
            }
            else if (SeritSayisi2 == 4)
            {
                araba1.Location = new Point(432, 458);
            }
            else if (SeritSayisi2 == 5)
            {
                araba1.Location = new Point(202, 458);
            }
        }

        private void TR2_KeyDown(object sender, KeyEventArgs e)
        {
            //1.OYUNCU SAĞ SOL YÖN TUŞLARININ ATAMASI
            if (e.KeyCode == Keys.Right) 
            {
                if (SeritSayisi < 2)
                    SeritSayisi++;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (SeritSayisi > 0) 
                    SeritSayisi--; 
            }                      
            AracYerine1();
            // 2.OYUNCU A VE D TUŞLARINI ATAMA
            if (e.KeyCode == Keys.A) 
            {
                if (SeritSayisi2 < 5)
                    SeritSayisi2++;
            }
            else if (e.KeyCode == Keys.D)
            {
                if (SeritSayisi2 > 3) 
                    SeritSayisi2--; 
            }
            AracYerine2();

        }


        private void MusicEkle()
        {
            axWindowsMediaPlayer1.URL = @"Sounds/menu_music.mp3";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void TR2_Load(object sender, EventArgs e)
        {
            durdurma1 = false;   //oyun başladığında durmuş halde olacak start butonuna basmak gerekiyor
            Pause1.Image = Properties.Resources.play;
            pause2.Image = Properties.Resources.play;

            timerRandomCar1.Enabled = false;
            timerSerit1.Enabled = false;
            timerRandomCar2.Enabled = false;
            timerSerit2.Enabled = false;


            for (var i = 0; i < rndCar1.Length; i++)
            {
                rndCar1[i] = new Random_Car1();
            }
            rndCar1[0].vakit = true;

            for (var i = 0; i < rndCar2.Length; i++)
            {
                rndCar2[i] = new Random_Car2();
            }
            rndCar2[0].vakit2 = true;

            AracYerine1();
            AracYerine2();
            MusicEkle();

            yskor1.Text = Settings1.Default.YuksekSkor1.ToString();
            yskor2.Text = Settings1.Default.YuksekSkor2.ToString();
        }
        
        bool seskontrol1 = true; //ses kontrolü için bool 

        private void timerRandomCar1_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < rndCar1.Length; i++)
            {
                if (!rndCar1[i].SahteAraba1 && rndCar1[i].vakit) //rndCar1'ın bool elemanları true veya false olursa...
                {
                    rndCar1[i].Arabalar1 = new PictureBox();
                    RandomCarGetir1(rndCar1[i].Arabalar1); //picturebox switch case 
                    rndCar1[i].Arabalar1.Size = new Size(140, 233);
                    rndCar1[i].Arabalar1.Top = -(rndCar1[i].Arabalar1.Height+170); //bir araç boyu mesafe çıkardık ki ardarda araba gelmesin

                    int SeriteYerlestir = R1.Next(0, 3); //random gelecek araçların konumlarını atama

                    if (SeriteYerlestir == 0) //random değer sıfır olarak geldi ise sola hizalaması 55
                    {
                        rndCar1[i].Arabalar1.Left = 670;
                    }
                    else if (SeriteYerlestir == 1)
                    {
                        rndCar1[i].Arabalar1.Left = 200;
                    }
                    else if (SeriteYerlestir == 2)
                    {
                        rndCar1[i].Arabalar1.Left = 450;
                    }
                    this.Controls.Add(rndCar1[i].Arabalar1);//dizinin içine ekleyecez
                    rndCar1[i].SahteAraba1 = true;
                }

                else
                {
                    if (rndCar1[i].vakit)
                    {
                        rndCar1[i].Arabalar1.Top += 20;
                        if (rndCar1[i].Arabalar1.Top >= 154)
                        {
                            for (int j = 0; j < rndCar1.Length; j++)
                            {
                                if (!rndCar1[j].vakit)
                                {
                                    rndCar1[j].vakit = true;
                                    break;
                                }
                            }
                        }
                        if (rndCar1[i].Arabalar1.Top >= this.Height - 20) //araç sonsuza dek aşağı kadar gitmesin diye
                        {
                            rndCar1[i].Arabalar1.Dispose();
                            rndCar1[i].SahteAraba1 = false;
                            rndCar1[i].vakit = false;
                        }
                    }
                    //***********KAZA----DURUMU*****************************
                }
                 if (rndCar1[i].vakit) //vakit true olduğunda çalışır halde oluyor arabamız
                    {
                        float MutlakX = Math.Abs((araba1.Left + (araba1.Width / 2)) - (rndCar1[i].Arabalar1.Left + (rndCar1[i].Arabalar1.Width / 2)));
                        float MutlakY = Math.Abs((araba1.Top + (araba1.Height / 2)) - (rndCar1[i].Arabalar1.Top + (rndCar1[i].Arabalar1.Height / 2)));
                        float FarkGenislik = (araba1.Width / 2) + (rndCar1[i].Arabalar1.Width / 2);
                        float FarkYukseklik = (araba1.Height / 2) + (rndCar1[i].Arabalar1.Height / 2);

                        if ((FarkGenislik > MutlakX) && (FarkYukseklik > MutlakY))
                        {
                            timerRandomCar1.Enabled = false;
                            timerSerit1.Enabled = false;

                            axWindowsMediaPlayer2.URL = @"sounds/car_crash.mp3";
                            axWindowsMediaPlayer2.Ctlcontrols.play();

                            kazapb1.Visible = true;
                            tekrarpb1.Visible = true;

                            if (Road1 > Settings1.Default.YuksekSkor1)
                            {
                            Settings1.Default.YuksekSkor1 = Road1;
                            Settings1.Default.Save();
                            }

                    }
                    }
                   
            }

        }

        private void timerRandomCar2_Tick(object sender, EventArgs e) //2.OYUNCU RANDOM ARABA HAREKETLERİ VE KAZA DURUMU 
        {
            for (int i = 0; i < rndCar2.Length; i++)
            {
                if (!rndCar2[i].SahteAraba2 && rndCar2[i].vakit2)
                {
                    rndCar2[i].Arabalar2 = new PictureBox();
                    RandomCarGetir2(rndCar2[i].Arabalar2);
                    rndCar2[i].Arabalar2.Size = new Size(140, 233);
                    rndCar2[i].Arabalar2.Top = -(rndCar2[i].Arabalar2.Height+170); //bir araç boyu mesafe çıkardık ki ardarda araba gelmesin

                    int SeriteYerlestir = R2.Next(0, 3); //random gelecek araçların konumlarını atama

                    if (SeriteYerlestir == 0) //random değer sıfır olarak geldi ise sola hizalaması 55
                    {
                        rndCar2[i].Arabalar2.Left = 980;
                    }
                    else if (SeriteYerlestir == 1)
                    {
                        rndCar2[i].Arabalar2.Left = 1240;
                    }
                    else if (SeriteYerlestir == 2)
                    {
                        rndCar2[i].Arabalar2.Left = 1470;
                    }


                    this.Controls.Add(rndCar2[i].Arabalar2);
                    rndCar2[i].SahteAraba2 = true;
                }
                else
                {
                    if (rndCar2[i].vakit2)
                    {
                        rndCar2[i].Arabalar2.Top += 20;
                        if (rndCar2[i].Arabalar2.Top >= 154)
                        {
                            for (int j = 0; j < rndCar2.Length; j++)
                            {
                                if (!rndCar2[j].vakit2)
                                {
                                    rndCar2[j].vakit2 = true;
                                    break;
                                }
                            }
                        }
                        if (rndCar2[i].Arabalar2.Top >= this.Height - 20) //araç sonsuza dek aşağı kadar gitmesin diye
                        {
                            rndCar2[i].Arabalar2.Dispose();
                            rndCar2[i].SahteAraba2 = false;
                            rndCar2[i].vakit2 = false;
                        }
                    }
                    // ****************KAZA--DURUMU*********************
                }
                  if (rndCar2[i].vakit2) //vakit true olduğunda çalışır halde oluyor arabamız
                  {                           //araba2 2.oyuncunun beyaz arabası
                    float MutlakX = Math.Abs((araba2.Left + (araba2.Width / 2)) - (rndCar2[i].Arabalar2.Left + (rndCar2[i].Arabalar2.Width / 2)));
                    float MutlakY = Math.Abs((araba2.Top + (araba2.Height / 2)) - (rndCar2[i].Arabalar2.Top + (rndCar2[i].Arabalar2.Height / 2)));
                    float FarkGenislik = (araba2.Width / 2) + (rndCar2[i].Arabalar2.Width / 2);
                    float FarkYukseklik = (araba2.Height / 2) + (rndCar2[i].Arabalar2.Height / 2);

                    if ((FarkGenislik > MutlakX) && (FarkYukseklik > MutlakY))
                    {
                        timerRandomCar2.Enabled = false;
                        timerSerit2.Enabled = false;

                        
                        axWindowsMediaPlayer2.URL = @"sounds/car_crash.mp3";
                        axWindowsMediaPlayer2.Ctlcontrols.play();

                        kaza2pb.Visible = true;
                        tekrarpb2.Visible = true;

                        if (Road2 > Settings1.Default.YuksekSkor2)
                        {
                            Settings1.Default.YuksekSkor2 = Road2;
                            Settings1.Default.Save();
                        }
                    }
                    
                  }
            }

        }
        void HizSeviye2()
        {   //seviye 2
            if (Road2 > 10 && Road2 < 30)
            {
                Speed2 = 70;
                timerSerit2.Interval = 170;
                timerRandomCar2.Interval = 150;
            }
            //seviye 3
            else if (Road2 >= 20 && Road2 < 35)
            {
                Speed2 = 90;
                timerSerit2.Interval = 160;
                timerRandomCar2.Interval = 140;
            }
            //seviye 4
            else if (Road2 >= 35 && Road2 < 50)
            {
                Speed2 = 100;
                timerSerit2.Interval = 150;
                timerRandomCar2.Interval = 130;
            }
            //seviye 5
            else if (Road2 >= 50 && Road2 < 65)
            {
                Speed2 = 120;
                timerSerit2.Interval = 140;
                timerRandomCar2.Interval = 130;
            }
            //seviye 6
            else if (Road2 >= 65 && Road2 <85)
            {
                Speed2 = 150;
                timerSerit2.Interval = 110;
                timerRandomCar2.Interval = 90;
            }
            //seviye 7
            else if (Road2 >= 85 && Road2 < 100)
            {
                Speed2 = 180;
                timerSerit2.Interval = 85;
                timerRandomCar2.Interval = 65;
            }
            //seviye 8
            else if (Road2 >= 100 && Road1 < 130)
            {
                Speed2 = 200;
                timerSerit2.Interval = 65;
                timerRandomCar2.Interval = 45;
            }
            //seviye 9
            else if (Road2 >= 130)
            {
                Speed2 = 250;
                timerSerit2.Interval = 45;
                timerRandomCar2.Interval = 30;
            }
        }
        private void timerSerit2_Tick(object sender, EventArgs e)
        {
            Road2 += 1;

            HizSeviye2();

            if (SeritHareket2 == false)
            {
                for (int i = 1; i < 5; i++)   //şerit sayısı kadar döndürüyoruz
                {    //i=1 çünkü labellar ssol1 olarak başlıyor her biri sıra sıra yukarı kayacak
                    this.Controls.Find("rsol" + i.ToString(), true)[0].Top -= 25; //2.oyuncu
                    this.Controls.Find("rsag" + i.ToString(), true)[0].Top -= 25;
                    SeritHareket2 = true;
                }
            }
            else
            {
                for (int i = 1; i < 5; i++)   //şerit sayısı kadar döndürüyoruz
                {    //i=1 çünkü labellar ssol1 olarak başlıyor her biri sıra sıra yukarı kayacak
                    this.Controls.Find("rsol" + i.ToString(), true)[0].Top += 25; //2.oyuncu
                    this.Controls.Find("rsag" + i.ToString(), true)[0].Top += 25;
                    SeritHareket2 = false;
                }
            }
            skor2.Text = Road2.ToString();
            surat2.Text = Speed2.ToString() + "km/h";

        }

        private void sound1_Click(object sender, EventArgs e)
        {
            if(seskontrol1 == true)   //birinci ses kısma image'i için geçerli olan kod satırı
            {
                seskontrol1 = false;
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                sound1.Image = Properties.Resources.sound_off;
                sound2.Image = Properties.Resources.sound_off;
            }
            else if (seskontrol1 == false)
            {
                seskontrol1 = true;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                sound1.Image = Properties.Resources.sound_on;
                sound2.Image = Properties.Resources.sound_on;
            }
        }

        private void kaza2pb_Click(object sender, EventArgs e)
        {

        }

        private void tekrarpb1_Click(object sender, EventArgs e) //tekrar oynama picturebox
        {
            timerRandomCar1.Enabled = true;
            timerSerit1.Enabled = true;
            tekrarpb1.Visible = false;
            kazapb1.Visible = false;
            
            for (int j = 0; j < rndCar1.Length; j++)
            {
                rndCar1[j].Arabalar1.Dispose();
                rndCar1[j].SahteAraba1 = false;
                rndCar1[j].vakit = false;
            }

            Road1 = 0;
            Speed1 = 70;
            rndCar1[0].vakit = true;
            timerRandomCar1.Enabled = true;
            timerRandomCar1.Interval = 200;

            timerSerit1.Enabled = true;
            timerSerit1.Interval = 200;

            yskor1.Text = Settings1.Default.YuksekSkor1.ToString();

            axWindowsMediaPlayer1.Ctlcontrols.play();

        }

        private void tekrarpb2_Click(object sender, EventArgs e) //tekrar oynama picturebox
        {
            timerRandomCar2.Enabled = true;
            timerSerit2.Enabled = true;
            tekrarpb2.Visible = false;
            kaza2pb.Visible = false;
            
            for (int j = 0; j < rndCar2.Length; j++)
            {
                rndCar2[j].Arabalar2.Dispose();
                rndCar2[j].SahteAraba2 = false;
                rndCar2[j].vakit2 = false;
            }

            Road2 = 0;
            Speed2 = 70;
            rndCar2[0].vakit2 = true;
            timerRandomCar2.Enabled = true;
            timerRandomCar2.Interval = 200;

            timerSerit2.Enabled = true;
            timerSerit2.Interval = 200;

            yskor2.Text = Settings1.Default.YuksekSkor2.ToString();
            axWindowsMediaPlayer1.Ctlcontrols.play();

        }

        private void sound2_Click(object sender, EventArgs e)
        {
            if (seskontrol1 == true)    //2.ses kapatma image'i için kod satırı
            {
                seskontrol1 = false;
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                sound1.Image = Properties.Resources.sound_off;
                sound2.Image = Properties.Resources.sound_off;
            }
            else if (seskontrol1 == false)
            {
                seskontrol1 = true;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                sound1.Image = Properties.Resources.sound_on;
                sound2.Image = Properties.Resources.sound_on;
            }
        }
        
        bool durdurma1 = true;

        private void Pause1_Click(object sender, EventArgs e)
        {
            if ( durdurma1 == true)
            {
                durdurma1 = false;
                Pause1.Image = Properties.Resources.play;
                pause2.Image = Properties.Resources.play;

                timerRandomCar1.Enabled = false;
                timerSerit1.Enabled = false;
                timerRandomCar2.Enabled = false;
                timerSerit2.Enabled = false;

                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            else if (durdurma1 == false)
            {
                durdurma1 = true;
                Pause1.Image = Properties.Resources.pause;
                pause2.Image = Properties.Resources.pause;

                timerRandomCar1.Enabled = true;
                timerSerit1.Enabled = true;
                timerRandomCar2.Enabled = true;
                timerSerit2.Enabled = true;

                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void pause2_Click(object sender, EventArgs e)
        {
            if (durdurma1 == true)
            {
                durdurma1 = false;
                Pause1.Image = Properties.Resources.play;
                pause2.Image = Properties.Resources.play;

                timerRandomCar1.Enabled = false;
                timerSerit1.Enabled = false;
                timerRandomCar2.Enabled = false;
                timerSerit2.Enabled = false;

                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            else if (durdurma1 == false)
            {
                durdurma1 = true;
                Pause1.Image = Properties.Resources.pause;
                pause2.Image = Properties.Resources.pause;

                timerRandomCar1.Enabled = true;
                timerSerit1.Enabled = true;
                timerRandomCar2.Enabled = true;
                timerSerit2.Enabled = true;

                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void baslat1_Click(object sender, EventArgs e)
        {
            
            durdurma1 = true;
            Pause1.Image = Properties.Resources.pause;
            pause2.Image = Properties.Resources.pause;

            timerRandomCar1.Enabled = true;
            timerSerit1.Enabled = true;
            timerRandomCar2.Enabled = true;
            timerSerit2.Enabled = true;
            baslat1.Visible = false;
            baslat2.Visible = false;
        }

        private void baslat2_Click(object sender, EventArgs e)
        {
            durdurma1 = true;
            Pause1.Image = Properties.Resources.pause;
            pause2.Image = Properties.Resources.pause;

            timerRandomCar1.Enabled = true;
            timerSerit1.Enabled = true;
            timerRandomCar2.Enabled = true;
            timerSerit2.Enabled = true;
            baslat1.Visible = false;
            baslat2.Visible = false;
        }

        bool SeritHareket2 = false;
        bool SeritHareket = false; //sonsuz döngü için bir bool değişkeni


        void HizSeviye1()
        {
            //seviye 2
            if (Road1 > 100 && Road1 < 200)
            {
                Speed1 = 80;
                timerSerit1.Interval = 170;
                timerRandomCar1.Interval = 150;
            }
            //seviye 3
            else if (Road1 >= 200 && Road1 < 350)
            {
                Speed1 = 90;
                timerSerit1.Interval = 160;
                timerRandomCar1.Interval = 140;
            }
            //seviye 4
            else if (Road1 >= 350 && Road1 < 500)
            {
                Speed1 = 100;
                timerSerit1.Interval = 150;
                timerRandomCar1.Interval = 130;
            }
            //seviye 5
            else if (Road1 >= 500 && Road1 < 650)
            {
                Speed1 = 120;
                timerSerit1.Interval = 140;
                timerRandomCar1.Interval = 130;
            }
            //seviye 6
            else if (Road1 >= 650 && Road1 < 850)
            {
                Speed1 = 150;
                timerSerit1.Interval = 110;
                timerRandomCar1.Interval = 90;
            }
            //seviye 7
            else if (Road1 >= 850 && Road1 < 1000)
            {
                Speed1 = 180;
                timerSerit1.Interval = 85;
                timerRandomCar1.Interval = 65;
            }
            //seviye 8
            else if (Road1 >= 1000 && Road1 < 1300)
            {
                Speed2 = 200;
                timerSerit2.Interval = 65;
                timerRandomCar2.Interval = 45;
            }
            //seviye 9
            else if (Road1 >= 1300)
            {
                Speed1 = 250;
                timerSerit1.Interval = 45;
                timerRandomCar1.Interval = 30;
            }
        }

        private void timerSerit1_Tick(object sender, EventArgs e)
        {
            Road1 += 1;

            HizSeviye1();

            if (SeritHareket == false)  
            {
                for (int i = 1; i < 5; i++)   //şerit sayısı kadar döndürüyoruz
                {    //i=1 çünkü labellar ssol1 olarak başlıyor her biri sıra sıra yukarı kayacak
                    this.Controls.Find("ssol" + i.ToString(), true)[0].Top -= 25;
                    this.Controls.Find("ssag" + i.ToString(), true)[0].Top -= 25;
                    SeritHareket = true;
                }
            }
            else
            {
                for (int i = 1; i < 5; i++)   //şerit sayısı kadar döndürüyoruz
                {    //i=1 çünkü labellar ssol1 olarak başlıyor her biri sıra sıra yukarı kayacak
                    this.Controls.Find("ssol" + i.ToString(), true)[0].Top += 25;
                    this.Controls.Find("ssag" + i.ToString(), true)[0].Top += 25;
                    SeritHareket = false;
                }
            }

            skor1.Text = Road1.ToString();
            surat1.Text = Speed1.ToString() + "km/h";
        
        }
    }
}
