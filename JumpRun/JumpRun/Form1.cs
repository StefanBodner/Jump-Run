namespace JumpRun
{
    public partial class frm_game : Form
    {
        public frm_game()
        {
            InitializeComponent();
        }
        private void frm_game_Load(object sender, EventArgs e)
        {
            WorldCreation();
        }

        #region Variables   
        static bool left = false;
        static bool right = false;
        static bool up = false;
        static int playerSpeed = 5;

        static int gravity = -20;

        static int boxHeight = 50;
        static int boxWidth = 50;
        #endregion


        #region Worlds
        static int[,] world = new int[,] {
            { 1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,0 },
            { 1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,0,0,0,0,0 },
            { 1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }};
        #endregion


        #region Movement
        private void tmr_game_Tick(object sender, EventArgs e)
        {
            Movement();
            Collision();
            DebugVariables();
        }

        public void Movement()
        {
            if (left)
            {
                player.Left -= playerSpeed;
            }
            else if (right)
            {
                player.Left += playerSpeed;
            }

            //if (up)
            //{
            //    player.Top += force
            //}

            //if ()
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.A))
            {
                left = true;
            }
            else if (e.KeyCode.Equals(Keys.D))
            {
                right = true;
            }
            if (e.KeyCode.Equals(Keys.W))
            {
                up = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.A))
            {
                left = false;
            }
            else if (e.KeyCode.Equals(Keys.D))
            {
                right = false;
            }
            if (e.KeyCode.Equals(Keys.W))
            {
                up = false;
            }
        }

        private void Collision()
        {
            //Bottom Collision
            Rectangle colBot = new Rectangle(player.Left, player.Bottom, player.Width, 1);

            this.CreateGraphics().FillRectangle(Brushes.Red, colBot);


            if (colBot.IntersectsWith())
            {

            }
        }
        #endregion

        #region World Creation
        public void WorldCreation()
        {
            for (int i = 0; i < world.GetLength(0); i++)
            {
                for (int j = 0; j < world.GetLength(1); j++)
                {
                    if (world[i, j] == 1)
                    {
                        PictureBox pb = new PictureBox();
                        pb.Top = i * boxHeight;
                        pb.Left = j * boxWidth;
                        pb.Height = boxHeight;
                        pb.Width = boxWidth;
                        pb.BackColor = Color.Blue;
                        this.Controls.Add(pb);
                    }
                }
            }
        }
        #endregion

        #region Debug
        public void DebugVariables()
        {
            lbl_left.Text = "left: " + left.ToString();
            lbl_right.Text = "right: " + right.ToString();
            //lbl_left.Text = "up: " + up.ToString();
        }
        #endregion

        private void frm_game_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}