using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDP_AnimatedImage
{
    public partial class Form1 : Form
    {
        private Image panda; //holds an image of the panda
        private List<string> pandaMovements = new List<string>();//stores the filenames of panda frame images
        private int steps = 0; //identifies index of pandaMovements list
        private int slowDownFrameRate = 0; //used to slow down the animation

        public Form1()
        {
            InitializeComponent(); 
            this.DoubleBuffered = true;  //needed for smooth drawing of images
           // panda = Image.FromFile($"images/wavingPanda.gif");


            pandaMovements = Directory.GetFiles($"images/pandaframes", "*.gif").ToList();//load image file names into a list
            panda = Image.FromFile(pandaMovements[0]);// set the initial player image to the first item in the list
        }

        private void refresh(object sender, EventArgs e)
        {//timer event
            this.Invalidate();
            animateImage(0, pandaMovements.Count()-1);
        }
        
        private void FormPaint(object sender, PaintEventArgs e)
        {//Form paint event
            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(panda, 174, 65, 480, 480);
        }
         
        private void animateImage(int start, int end)
        {//procedure to change the image file, incrementing the image location index each time it is called

            slowDownFrameRate += 1;
            if (slowDownFrameRate == 20) //wait until the procedure has been called a number of times before changing the image
                                        //this slows down the rate at which the image changes
            {
                steps++; //go to next image
                slowDownFrameRate = 0; //frame rate countdown again
            }
            if (steps > end || steps < start) //identify when to loop back to first image
            {
                steps = start;
            }
            panda = Image.FromFile(pandaMovements[steps]);//change the image

        }
    }
}
