﻿using System;
using System.Threading;
using System.Windows.Media.Imaging;

namespace Tanki_3._0
{
     partial class MainWindow
    {
        class Enemy : Character
        {
            public Enemy(int x, int y) : base(x, y)
            {
                Lifes = 1;
                image.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/Images/enemy.png"));
            }

            public void Behavior(Field field, BulletCollection bullets)
            {
                Random rnd = new Random((Y * 100 + X) * DateTime.Now.Millisecond);
                int doActions = rnd.Next(0, 2);
                if (doActions == 0)
                {
                    int doStep = rnd.Next(0, 2);
                    if (doStep == 0)
                    {
                        int stepDirection = rnd.Next(0, 4);
                        int dx = 0, dy = 0;
                        switch (stepDirection)
                        {
                            case 0:
                                dx--;
                                break;
                            case 1:
                                dy--;
                                break;
                            case 2:
                                dx++;
                                break;
                            case 3:
                                dy++;
                                break;
                        }
                        Rotate(dx, dy);
                        if(field.CanMakeStep(X + dx, Y + dy))
                            MakeStep(dx, dy);
                    }

                    int doShot = rnd.Next(0, 3);
                    if (doShot == 0)
                    {
                        bullets.Add(new Bullet(X, Y, direction, true));
                    }
                }
            }
        }
    }
}