
        private float timeSinceLastHit = 0f;

        public KeyboardState currKeyboardState;
        public KeyboardState prevKeyboardState;
        private GamePadState _currentGamepadState;
        private GamePadState _prevGamepadState;
                Position = new Vector2(50, 800);
                attacking = true;
                IsPlayerHit = true;

            if (currKeyboardState.IsKeyDown(Keys.E) && prevKeyboardState.IsKeyUp(Keys.E))


            if (IsPlayerHit)
            {
                playerHealth -= 10;
            }

            foreach (var b in bullets)
                b.Update(gameTime);
            
            if (currKeyboardState.IsKeyDown(Keys.Space) && !prevKeyboardState.IsKeyDown(Keys.Space) && !attacking && !IsDead)
            {
                attacking = true;
            }

           
          
            if (currKeyboardState.IsKeyDown(Keys.E) && prevKeyboardState.IsKeyUp(Keys.E) && !IsDead)
                var bullet = new Bullet(this.Game);
                bullet.Velocity = new Vector2(direction, 0) * 8;
                    if (_currentGamepadState.ThumbSticks.Left.X > 0.5f)
                    {
                        Position += new Vector2(5.0f, 0);
                        Collide(BeeSoulsGame.tileEngine.CheckCollision(PlayerHitBox), "width");
                        direction = 1;
                    }
                    if (_currentGamepadState.ThumbSticks.Left.Y < -0.5f)
                    {
                        Position += new Vector2(0,5.0f);
                        Collide(BeeSoulsGame.tileEngine.CheckCollision(PlayerHitBox), "height");

                    }

                    if (_currentGamepadState.ThumbSticks.Left.Y > 0.5f)
                    {
                        Position += new Vector2(0, -5.0f);
                        Collide(BeeSoulsGame.tileEngine.CheckCollision(PlayerHitBox), "height");

                    }
                }

                if (c.GamePadType == GamePadType.GamePad)
                {
                    if (_currentGamepadState.IsButtonDown(Buttons.Back))
                    {
                       BeeSoulsGame g = this.Game as BeeSoulsGame;
                        g.Exit();
                    }

                    if (_currentGamepadState.IsButtonDown(Buttons.A) && _prevGamepadState.IsButtonUp(Buttons.A))
                    {
                        var bullet = new Bullet(this.Game);
                        bullet.Velocity = new Vector2(direction, 0) * 8;
                        bullet.Position = Position;
                        bullets.Add(bullet);


                    }



                }
            }

            

            if (playerHealth <= 0)
            {
                IsDead = true;
            }
            else
            {
                IsDead = false;
            }

