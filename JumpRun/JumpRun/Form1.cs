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
            DrawPlayer();
        }

        #region Variables 
        PictureBox player = new PictureBox();
        int playerWidth = 30;
        int playerHeight = 30;

        static bool left = false;
        static bool right = false;
        static bool up = false;
        static int playerSpeed = 4;

        public int jumpTicks = 0;
        public int minJumpTicks = 5;
        static int force = -17;
        static int gravity = 0;
        static bool inAir = true;
        static bool headroom = true;

        static int blockHeight = 50;
        static int blockWidth = 50;

        static List<Rectangle> liBlocks = new List<Rectangle>();

        //Player Collision Detection
        static Rectangle colBot;
        static Rectangle colTop;
        static Rectangle colLeftTop;
        static Rectangle colRightTop;
        static Rectangle colLeftBot;
        static Rectangle colRightBot;

        static bool canMoveToTheLeft;
        static bool canMoveToTheRight;

        Graphics g;
        #endregion


        #region Worlds
        static int[,] world = new int[,] {
            { 1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,0 },
            { 1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,0,0,0,0,0 },
            { 1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,0 },
            { 0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0 },
            { 0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,2 },
            { 0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,3,4,1 },
            { 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }};
        #endregion


        #region Movement
        private void tmr_game_Tick(object sender, EventArgs e)
        {
            Movement();
            Collision();
            DebugVariables();
            //ShowPlayerCollisionDetection();
        }

        public void Movement()
        {
            //basic left & right movement
            if (left && canMoveToTheLeft)
            {
                player.Left -= playerSpeed;
            }
            else if (right && canMoveToTheRight)
            {
                player.Left += playerSpeed;
            }

            //allow player to jump
            if (up && !inAir)
            {
                inAir = true;
                gravity = force;
            }

            //check if player touches the floor
            if (inAir)
            {
                gravity += 1;
            }
            else if (!inAir)
            {
                gravity = 0;
            }

            //track jumping duration
            if (gravity < 0)
            {
                jumpTicks++;
            }
            else if (gravity >= 0) //if player starts falling down again - reset jumpTick tracker
            {
                jumpTicks = 0;
            }

            //Prevent player from going higher
            if (!up && gravity < 0 && jumpTicks >= minJumpTicks)
            {
                //-3 because of smoother jumping curve - not an aprupt stop in velocity
                gravity = -3;
                jumpTicks = 0;
            }

            //move player according to gravity's value
            player.Top += gravity;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Basic Key Pressed Event
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
            //Basic Key Released Event
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
            colBot = new Rectangle(player.Left, player.Bottom, playerWidth, 1); //Bottom Collision
            colTop = new Rectangle(player.Left, player.Top - 1, playerWidth, 1); //Top Collision
            colLeftTop = new Rectangle(player.Left - 1, player.Top, 1, playerHeight); // Left Top Collision
            colRightTop = new Rectangle(player.Right, player.Top, 1, playerHeight); // Left Top Collision

            //Check if player intersects with any Blocks/Enemies/...
            inAir = true;
            headroom = true;
            canMoveToTheLeft = true;
            canMoveToTheRight = true;
            foreach(Rectangle b in liBlocks)
            {
                if (colBot.IntersectsWith(b))
                {
                    inAir = false;
                    player.Top = b.Top - playerHeight;
                    break;
                }
                if (colTop.IntersectsWith(b))
                {
                    headroom = false;
                    gravity = 0;
                    player.Top = b.Bottom;
                    break;
                }
                if (colLeftTop.IntersectsWith(b))
                {
                    canMoveToTheLeft = false;
                    player.Left = b.Right;
                }
                if (colRightTop.IntersectsWith(b))
                {
                    canMoveToTheRight = false;
                    player.Left = b.Left - playerWidth;
                }
                
                
            }
        }
        #endregion

        #region World + Player Creation
        public void WorldCreation()
        {
            for (int y = 0; y < world.GetLength(0); y++)
            {
                for (int x = 0; x < world.GetLength(1); x++)
                {
                    if (world[y, x] == 1) // normal block
                    {
                        Rectangle block = new Rectangle(x * blockWidth, y * blockHeight, blockWidth, blockHeight);
                        g.FillRectangle(Brushes.LightBlue, block);
                        liBlocks.Add(block);
                    }
                    else if (world[y, x] == 2) // steep slope right
                    {
                        Point[] points = new Point[3];
                        points[0] = new Point(x * blockWidth, (y + 1) * blockHeight);
                        points[1] = new Point((x + 1) * blockWidth, y * blockHeight);
                        points[2] = new Point((x + 1) * blockWidth, (y + 1) * blockHeight);
                        g.FillPolygon(Brushes.CornflowerBlue, points);
                    }
                    else if (world[y, x] == 3) // gentle slope right
                    {
                        Point[] points = new Point[3];
                        points[0] = new Point(x * blockWidth, (y + 1) * blockHeight);
                        points[1] = new Point((x + 1) * blockWidth, y * blockHeight + blockHeight / 2);
                        points[2] = new Point((x + 1) * blockWidth, (y + 1) * blockHeight);
                        g.FillPolygon(Brushes.CornflowerBlue, points);
                    }
                    else if (world[y, x] == 4) // gentle slope right upper part
                    {
                        Point[] points = new Point[3];
                        points[0] = new Point(x * blockWidth, (y + 1) * blockHeight - blockHeight / 2);
                        points[1] = new Point((x + 1) * blockWidth, y * blockHeight);
                        points[2] = new Point((x + 1) * blockWidth, (y + 1) * blockHeight - blockHeight / 2);
                        g.FillPolygon(Brushes.CornflowerBlue, points);

                        Rectangle block = new Rectangle(x * blockWidth, y * blockHeight + blockHeight / 2, blockWidth, blockHeight / 2);
                        g.FillRectangle(Brushes.LightBlue, block);
                    }
                }
            }
        }
 
        public void DrawPlayer()
        {
            //Setup the Player PictureBox
            player.Width = playerWidth;
            player.Height = playerHeight;
            player.BackColor = Color.Blue;
            player.Top = 300;
            player.Left = 100;
            game.Controls.Add(player);

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
            lbl_x.Text = "x: " + player.Left.ToString();
            lbl_y.Text = "y: " + player.Top.ToString();
            lbl_gravity.Text = "gravity: " + gravity.ToString();
            lbl_jumpTicks.Text = "jumpTicks: " + jumpTicks.ToString();
        }

        public void ShowPlayerCollisionDetection()
        {
            g.FillRectangle(Brushes.Red, colBot);
            g.FillRectangle(Brushes.Red, colTop);
            g.FillRectangle(Brushes.Red, colLeftTop);
            g.FillRectangle(Brushes.Red, colRightTop);
        }
        #endregion

        private void game_Paint(object sender, PaintEventArgs e)
        {
            //Draw World Once
            WorldCreation();
        }
    }
}