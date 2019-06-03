using System;
using System.Runtime.Remoting.Activation;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tanki_3._0
{
     partial class  MainWindow 
    {
        class Level
        {
            private Field gameField;
            private Player player;
            private CharacterCollection enemies;
            private BulletCollection bullets;

            static public Canvas canvas;

            public PressedKey key;

            public enum PressedKey
            {
                Left, Right, Up, Down, Fire, None
            }


            public bool Game;


            public Level(int level, Canvas canvas)
            {
                Game = true;
                bullets = new BulletCollection();
                key = PressedKey.None;
                new LevelLoader(level).LoadField(out gameField, out player, out enemies);
                Level.canvas = canvas;
            }

            public void PrintLevel()
            {
                canvas.Children.Clear();

                gameField.Print();
                bullets.Print();
                LivesMessage();

            }

            public void UpdateField()
            {
                PlayerAction();
                EnemiesActions();
                ClearDestroyed();
                BulletsMove();
            }

            private void PlayerAction()
            {
                int dx = 0, dy = 0;
                switch (key)
                {
                    case PressedKey.Up:
                        dy = -1;
                        break;
                    case PressedKey.Down:
                        dy = 1;
                        break;
                    case PressedKey.Left:
                        dx = -1;
                        break;
                    case PressedKey.Right:
                        dx = 1;
                        break;
                    case PressedKey.Fire:
                        bullets.Add(new Bullet(player.X, player.Y, player.CurrentDirection));
                        key = PressedKey.None;
                        break;
                }

                if (gameField.CanMakeStep(player.X + dx, player.Y + dy))
                {
                    player.MakeStep(dx, dy);
                    key = PressedKey.None;
                }
                player.Rotate(dx, dy);
                PutCharacterOnField(player);
            }

            private void PutCharacterOnField(Character character)
            {
                int x = character.X;
                int y = character.Y;
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx + x >= 0 && dx + x < gameField.Width &&
                            dy + y >= 0 && dy + y < gameField.Height &&
                            gameField[dx + x, dy + y] == character)
                        {
                            gameField[dx + x, dy + y] = new Empty(dx + x, dy + y);
                            break;
                        }
                    }
                }

                gameField[x, y] = character;
            }

            private void ClearDestroyed()
            {
                for (int y = 0; y < gameField.Height; y++)
                {
                    for (int x = 0; x < gameField.Width; x++)
                    {
                        if (gameField[x, y] is DestroyedObject)
                            gameField.MakeEmpty(x, y);
                    }
                }
                
            }

            private void EnemiesActions()
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if(enemies[i] is Enemy)
                        (enemies[i] as Enemy).Behavior(gameField, bullets);
                    PutCharacterOnField(enemies[i]);
                }
            }

            private void BulletsMove()
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    int dx, dy;
                    bullets[i].ChooseDirection(out dx, out dy);
                    if (gameField.NotOutOfField(bullets[i].X + dx, bullets[i].Y + dy))
                        bullets[i].MakeStep();
                    else
                    {
                        bullets.Delete(i--);
                        continue;
                    }
                    int x = bullets[i].X;
                    int y = bullets[i].Y;

                    if (BulletShot(x, y, bullets[i].EnemySource)) {
                        bullets.Delete(i--);
                    }
                   
                }
            }

            private bool BulletShot(int x, int y, bool isEnemy) {
                if (gameField[x, y] is Wall)
                {
                    ShootWall(x, y);
                }
                else if (gameField[x, y] is Player)
                {
                    ShootPlayer(x, y);
                }
                else if (gameField[x, y] is Enemy && !isEnemy)
                {
                    ShootEnemy(x, y);
                }
                else
                {
                    return false;
                }
                return true;
            }

            private void ShootWall(int x, int y) {
                gameField.MakeDestroyed(x, y);
            }

            private void ShootPlayer(int x, int y) {
                player.Lifes--;
                if (player.Lifes == 0)
                {
                    Lose();
                }
            }

            private void ShootEnemy(int x, int y) {
                int enemyIndex = enemies.GetIndexByPosition(x, y);
                enemies[enemyIndex].Lifes--;
                if (enemies[enemyIndex].Lifes == 0) {
                    enemies.Delete(enemyIndex);
                    gameField.MakeDestroyed(x, y);
                }
                if (enemies.Count == 0)
                {
                    Win();
                }
            }

            private void Win()
            {
                Game = false;
                PrintLevel();
                ShowGameOverMessage("Congratulations! You win!");
            }

            private void Lose()
            {
                Game = false;
                PrintLevel();
                ShowGameOverMessage("You lose!! See you next time");
            }

           

            private void LivesMessage()
            {
                lives.Text = $"Lives: {player.Lifes}";
            }
            
            public void ChangeKeyState(Key pressedKey)
            {
                switch(pressedKey)
                {
                    case Key.W:
                    case Key.Up:
                        key = PressedKey.Up;
                        break;
                    case Key.S:
                    case Key.Down:
                        key = PressedKey.Down;
                        break;
                    case Key.A:
                    case Key.Left:
                        key = PressedKey.Left;
                        break;
                    case Key.D:
                    case Key.Right:
                        key = PressedKey.Right;
                        break;
                    case Key.F:
                    case Key.Space:
                        key = PressedKey.Fire;
                        break;
                    default:
                        key = PressedKey.None;
                        break;
                }
            }
        }
    }
}