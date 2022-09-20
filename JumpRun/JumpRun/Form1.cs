namespace JumpRun
{
    public partial class frm_game : Form
    {
        public frm_game()
        {
            InitializeComponent();
            g = game.CreateGraphics();
        }
        private void frm_game_Load(object sender, EventArgs e)
        {

        }

        #region Variables   
        static bool left = false;
        static bool right = false;
        static bool up = false;
        static int playerSpeed = 5;

        static int force = -15;
        static int gravity = 0;
        static bool inAir = true;
        static bool headroom = true;

        static int boxHeight = 50;
        static int boxWidth = 50;

        static List<Rectangle> liBlocks = new List<Rectangle>();

        //Player Collision Detection
        static Rectangle colBot;
        static Rectangle colTop;

        Graphics g;
        #endregion


        #region Worlds
        static int[,] world = new int[,] {
            { 1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,0 },
            { 1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,0,0,0,0,0 },
            { 1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,2,1,0,0,0,0,0,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }};
        #endregion


        #region Movement
        private void tmr_game_Tick(object sender, EventArgs e)
        {
            Movement();
            Collision();
            DebugVariables();
            ShowPlayerCollisionDetection();
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

            if (up && !inAir && headroom)
            {
                gravity = force;
                inAir = true;
            }

            if (inAir)
            {
                gravity += 1;
            }
            else
            {
                gravity = 0;
            }

            player.Top += gravity;
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
            colBot = new Rectangle(player.Left, player.Bottom, player.Width, 1); //Bottom Collision
            colTop = new Rectangle(player.Left, player.Top - 1, player.Width, 1); //Top Collision

            inAir = true;
            headroom = true;
            foreach(Rectangle b in liBlocks)
            {
                if (colBot.IntersectsWith(b))
                {
                    inAir = false;
                    player.Top = b.Top - player.Height;
                }
                if (colTop.IntersectsWith(b))
                {
                    headroom = false;
                    gravity = 0;
                    player.Top = b.Bottom;
                }
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
                        Rectangle block = new Rectangle(j * boxWidth, i * boxHeight, boxWidth, boxHeight);
                        g.FillRectangle(Brushes.LightBlue, block);
                        liBlocks.Add(block);
                    }
                    else if(world[i, j] == 2) 
                    {
                        Point[] points = new Point[3];
                        points[0] = new Point(j * boxWidth, (i+1) * boxHeight);
                        points[1] = new Point((j +1) * boxWidth, i * boxHeight);
                        points[2] = new Point((j +1) * boxWidth, (i + 1) * boxHeight);
                        g.FillPolygon(Brushes.CornflowerBlue, points);
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
            lbl_up.Text = "up: " + up.ToString();
            lbl_inAir.Text = "inAir: " + inAir.ToString();
            lbl_headroom.Text = "headroom: " + headroom.ToString();
        }

        public void ShowPlayerCollisionDetection()
        {
            g.FillRectangle(Brushes.Red, colBot);
            g.FillRectangle(Brushes.Red, colTop);
        }
        #endregion

        private void game_Paint(object sender, PaintEventArgs e)
        {
            WorldCreation();
        }
    }
}