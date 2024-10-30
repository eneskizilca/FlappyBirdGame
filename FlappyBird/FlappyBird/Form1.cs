using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyBird
{
    public partial class Form1 : Form
    {
        //gerekli degiskenler
        Random random = new Random();
        int pipeSpeed = 8;
        int gravity = 7;
        int score = 0;
        int firstPipeBottomLocation = 348;
        int firstPipeTopLocation = -133;
        bool gameOver = false;
        public Form1()
        {
            InitializeComponent();
        }

        //timer nesnesi event fonksiyonu, surekli oyun durumu kontrolu yapar
        private void gameTimerEvent(object sender, EventArgs e)
        {
            //bir ve pipe ları hareket ettir
            bird.Top += gravity;
            pipeBottom.Left -= pipeSpeed;
            pipeTop.Left -= pipeSpeed;
            lblScore.Text = score.ToString();  
            
            int randomLocationValue = random.Next(10, 111); //pipe lara değişeceği yer degeri için sayı uret

            if(pipeBottom.Right < 0 || (pipeTop.Right < 0)) // pipelar sol kenarın bitişine gelirse yeniden konumla
            {
                pipeBottom.Left = 600;
                pipeBottom.Top = firstPipeBottomLocation + randomLocationValue;
                pipeTop.Left = 600;
                pipeTop.Top = firstPipeTopLocation + randomLocationValue;
                
            }

            if (bird.Bounds.IntersectsWith(pipeBottom.Bounds) || //pipelara veya zemine degmesini kontrol et
                bird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                 bird.Bounds.IntersectsWith(ground.Bounds))
            {
                GameOver();
            }

            if(bird.Right == pipeTop.Location.X) //bird pipe a degmeden ayni konuma gelirse scoru u artir
            {
                score++;
            }
            
        }

        void GameOver() //oyun sonlaninca calisacak seyler
        {
            gameTimer.Stop();
            lblScore.Text = score + " -> Game Over. R'ye bas!";
            gameOver = true;

        }

        void RestartGame() // yeniden baslatma fonksiyonu, degerleri default a getir
        {
            gameOver = false;
            bird.Location = new Point(90, 232);
            pipeBottom.Left = 650;
            pipeTop.Left = 650;
            score = 0;
            pipeSpeed = 8;     
            lblScore.Text = score.ToString();
            gameTimer.Start();
        }

        private void gameKeyIsDown(object sender, KeyEventArgs e) // space e basılı tutuldugunda calisir
        {
            if(e.KeyCode == Keys.Space)
            {
                gravity = -15;
            }
        }

        private void gameKeyIsUp(object sender, KeyEventArgs e) // space basılı olmadığında calisir
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 6;
            }
            if (e.KeyCode == Keys.R && gameOver) // yandiktan sonra R'ye  basinca oyunu yeniden baslat
            {
                RestartGame();
            }
        }
    }
}
