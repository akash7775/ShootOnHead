using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootOnHead
{
    public partial class ShootForm : Form
    {
        Logic Obj_Logic = new Logic();
        public ShootForm()
        {
            // Set the positions
            InitializeComponent();
            this.pointAtHead.Enabled = false;
            this.PointAway.Enabled = false;
            this.SpinChamber.Enabled = false;
            this.Fire.Enabled = false;
        }
        Random ran = new Random();
        
        private void pointAtHead_Click(object sender, EventArgs e)
        {
            this.gun.Location = new Point(196, 95);

        }

        private void PointAway_Click(object sender, EventArgs e)
        {
            this.gun.Location = new Point(290, 95);
        }

        private void SpinChamber_Click(object sender, EventArgs e)
        {
            if(Obj_Logic.cham_loc == 6)
            {
                Obj_Logic.cham_loc = 0;
            }
            Obj_Logic.cham_loc++;
            this.ChamberLocation.Text = "Chamber Position=" + Obj_Logic.cham_loc;

            this.LoadBullet.Enabled = false;
            this.pointAtHead.Enabled = false;
            this.PointAway.Enabled = true;
            this.SpinChamber.Enabled = true;
            this.Fire.Enabled = true;
        }

        private void LoadBullet_Click(object sender, EventArgs e)
        {
            //playing the audio
            SoundPlayer audio = new SoundPlayer(ShootOnHead.Properties.Resources.reload);
            audio.Play();
            //creating the bullets location
            Obj_Logic.bullet_loc = ran.Next(1, 7);
            this.LoadBullet.Enabled = false;
            this.pointAtHead.Enabled = true;
            this.PointAway.Enabled = false;
            this.SpinChamber.Enabled = false;
            this.Fire.Enabled = false;
        }

        public void showAnimatedPictureBox()
        {
            person.Image = ShootOnHead.Properties.Resources.dead;
            person.Refresh();
            person.Visible = true;
        }

        private void Fire_Click(object sender, EventArgs e)
        {
         // set the bullet location for shoot and get the points
            if (this.gun.Location.X == 196)
            {
                if (Obj_Logic.bullet_loc == Obj_Logic.cham_loc)
                {
                    showAnimatedPictureBox();
                    SoundPlayer audio = new SoundPlayer(ShootOnHead.Properties.Resources.fire);
                    audio.Play();
                    this.Fire.Visible = false;
                    this.Status.Text = "Died ";
                    this.Status.Visible = true;
                    this.PlayAgain.Visible = true;
                    this.PointAway.Visible = false;
                    this.pointAtHead.Visible = false;
                    this.LoadBullet.Visible = false;
                    this.SpinChamber.Visible = false;
                }
                else
                {
                    Obj_Logic.shots_left--;
                    this.ShotsLeft.Text = "Shots Left=" + Obj_Logic.shots_left;
                }
            }
            else
            {
                Obj_Logic.stayAway--;
                if (Obj_Logic.bullet_loc == Obj_Logic.cham_loc)
                {
                    // play and getting the points
                    SoundPlayer audio = new SoundPlayer(ShootOnHead.Properties.Resources.fire);
                    audio.Play();
                    this.Fire.Visible = false;
                    this.Status.Text = "Survived you got 100 points";
                    this.Status.Visible = true;
                    this.PlayAgain.Visible = true;
                    this.PointAway.Visible = false;
                    this.pointAtHead.Visible = false;
                    this.LoadBullet.Visible = false;
                    this.SpinChamber.Visible = false;
                }
                else
                {
                    Obj_Logic.shots_left--;
                    this.ShotsLeft.Text = "Shots Left=" + Obj_Logic.shots_left;
                }

            }
            if (Obj_Logic.shots_left == 0)
            {
                this.Fire.Visible = false;
                this.Status.Text = "Survived";
                this.Status.Visible = true;
                this.PlayAgain.Visible = true;
                this.PointAway.Visible = false;
                this.pointAtHead.Visible = false;
                this.LoadBullet.Visible = false;
                this.SpinChamber.Visible = false;
            }
            // Spin the chamber and change the position
            if (Obj_Logic.cham_loc == 6)
            {
                Obj_Logic.cham_loc = 0;
            }
            Obj_Logic.cham_loc++;
            this.ChamberLocation.Text = "Chamber Position=" + Obj_Logic.cham_loc;
            if (Obj_Logic.stayAway == 0)
            {
                this.gun.Location = new Point(196, 95);
                this.PointAway.Enabled = false;
            }

        }


        private void PlayAgain_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

    }
}
