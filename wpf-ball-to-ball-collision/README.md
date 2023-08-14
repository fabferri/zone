<properties
pageTitle= 'Elastic collisions of balls in WFP'
description= "Elastic collisions of balls in WFP"
documentationcenter: na
services=""
documentationCenter="na"
authors="fabferri"
editor=""/>

<tags
   ms.service="configuration-Example-WFP"
   ms.devlang="na"
   ms.topic="article"
   ms.tgt_pltfrm="na"
   ms.workload="na"
   ms.date="02/01/2015"
   ms.author="fabferri" />

# Elastic collisions of balls in WFP

Elastic collisions of multiple balls are not a trivial effect. Apparently, this subject seems only related to physics, but collision between objects is simulated in gaming. To run collision of balls we need to understand physics laws.
Consider two spherical rigid objects, denoted by subscripts 1 and 2, one of mass **m<sub>1</sub>** and the other of mass **m<sub>2</sub>**. <br> 
Let's set things up so these two balls are approaching each other along the line joining their centers, a recipe for a head on collision. Let these balls not be rotating or vibrating; the motion is purely translation. Under these conditions, we may choose a reference frame, which is one dimensional, with the objects on the x-axis.
We consider an isolated system, in other words a system where external forces are absent (nothing affects spheres in the stage). A perfectly elastic collision is defined as one in which there is no loss of kinetic energy in the collision; the conservation laws say in an elastic collision total kinetic energy is the same before and after the collision and total momentum remains constant throughout the collision.
Applying the conservation of linear momentum to this situation, we have:

m<sub>1</sub>v<sub>1</sub> + m<sub>2</sub>v<sub>2</sub> = m<sub>1</sub>u<sub>1</sub> + m<sub>2</sub>u<sub>2</sub>

where **v** is the initial velocity of each object before the collision, **u** is the final velocity of each object after the collision and m is the mass of the object. The conservation of kinetic energy gives us the equation:

m<sub>1</sub>v<sub>1</sub><sup>2</sup> / 2 + m<sub>2</sub>v<sub>2</sub><sup>2</sup> / 2 = m<sub>1</sub>u<sub>1</sub><sup>2</sup> / 2 + m<sub>2</sub>u<sub>2</sub><sup>2</sup> / 2   <br>

These two equations can rewrite as:
- momentum equation:      m<sub>1</sub> (v<sub>1</sub> - u<sub>1</sub>) = m<sub>2</sub> (u<sub>2</sub> - v<sub>2</sub>)   
- conservation of kinetic energy: m<sub>1</sub> (v<sub>1</sub><sup>2</sup> - u<sub>1</sub><sup>2</sup>) = m2 (u<sub>2</sub><sup>2</sup> - v<sub>2</sub><sup>2</sup>)

if the difference between final and initial velocities is not zero for either object (meaning a collision actually happens), we may divide the second equation by the first one, which yields:

v<sub>1</sub> + u<sub>1</sub> = u<sub>2</sub> + v<sub>2</sub>  <br>
or <br>
v<sub>1</sub> - v<sub>2</sub> = u<sub>2</sub> - u<sub>1</sub>

In other words, in a one-dimensional elastic collision the relative velocity of approach before the collision equals the relative velocity of separation after collision. To get the final velocities in terms of the initial velocities and the masses, we would solve the last equation above for u2, plug that into the momentum equation, and solve to get:

u<sub>1</sub> = v<sub>1</sub> (m<sub>1</sub> - m<sub>2</sub>) / (m<sub>1</sub> + m<sub>2</sub>) + v<sub>2</sub> (2 m<sub>2</sub>) / (m<sub>1</sub> + m<sub>2</sub>) <br>

u<sub>2</sub> = v<sub>1</sub> (2 m<sub>1</sub>) / (m<sub>1</sub> + m<sub>2</sub>) + v<sub>2</sub> (m<sub>2</sub> - m<sub>1</sub>) / (m<sub>2</sub> + m<sub>1</sub>)

Let start out with two spheres moving in the plane to have a collision: 


[![1]][1]

<p align="center">sketch 1: angles in collision</p>

Also in bi-dimensional collision, we ignore every energy losses due to friction and rotation. The physical laws (the conservation of momentum and conservation of kinetic energy) are invariant for changing of Cartesian coordinate system, so we can apply a transformation and consider a new standard x'-y' coordinate system (sketch 2), where the x-axis lies along the collision line, and the y-axis is perpendicular to that.  In this new Cartesian coordinate system Ref' is easier to understand what happens.


[![2]][2]

<p align="center">sketch 2: new Cartesian coordinate system Ref'</p>

The sketch 3 shows the new vector components of velocities, in the new Cartesian coordinate system Ref' where the x-axis lies along the collision line, and the y-axis is perpendicular to that.

[![3]][3]

<p align="center">sketch 3: velocity components in the new coordinate system</p>

In the new Cartesian coordinate system Ref' the velocities components of spheres along x<sup>'</sup>, y<sup>'</sup>-axis are:

v'<sub>1</sub>x = v<sub>1</sub> * cos($\theta$<sub>1</sub> - $\Phi$) <br>
v'<sub>1</sub>y = v<sub>1</sub> * sin($\theta$<sub>1</sub> - $\Phi$) <br>
v'<sub>2</sub>x = v<sub>2</sub> * cos($\theta$<sub>2</sub> - $\Phi$) <br>
v'<sub>2</sub>y = v<sub>2</sub> * sin($\theta$<sub>2</sub> - $\Phi$) <br>

In the case of the two spheres, the velocity components involved are the components resolved along the line of centers during the contact. Consequently, the components of velocity perpendicular to the line of centers will be unchanged during the impact.
The new x velocities in our new coordinate system follows the same outcome of collision 1D, and the y-components do not change. 

u'<sub>1x</sub> = ((m<sub>1</sub> - m<sub>2</sub>) * v'<sub>1x</sub> + (m<sub>1</sub> + m<sub>2</sub>) * v'<sub>2x</sub>) / (m<sub>1</sub> + m<sub>2</sub>) <br>
u'<sub>2x</sub> = ((m<sub>1</sub> + m<sub>1</sub>) * v'1x + (m<sub>2</sub>-m<sub>1</sub>) * v'<sub>2x</sub>) / (m<sub>1</sub> + m<sub>2</sub>) <br>
u'<sub>1y</sub> = v'<sub>1y</sub>   &nbsp; &nbsp; &nbsp;  *(the y velocity of sphere 1 won't change)* <br>
u'<sub>2y</sub> = v'<sub>2y</sub>   &nbsp; &nbsp; &nbsp;   *(the y velocity of sphere 2 won't change)* <br>

The sketch 4 shows the x-velocities inversion, after the collision:

[![4]][4]

<p align="center">sketch 4: velocities components in new Cartesian coordinate system Ref'</p>

We have determined the 'after collision' velocities, but we have to transform the components back to the initial x-y reference frame Ref. <br>

[![5]][5]

<p align="center">sketch 5: changing back to new initial Coordinate system Ref</p>

Translation from Cartesian coordinate system Ref' to Ref can be execute by counterclockwise rotation matrix M($\Phi$):

```math
\begin{pmatrix}
x \\
y
\end{pmatrix}
= M(\Phi) 
\begin{pmatrix}
  x' \\
 y'  
\end{pmatrix}
```


```math
\begin{pmatrix}
x \\
y
\end{pmatrix}
= 
\begin{pmatrix}
  cos(\Phi)  & -sin(\Phi)\\
  sin(\Phi)  & cos(\Phi)\\  
\end{pmatrix}
\begin{pmatrix}
  x' \\
 y'  
\end{pmatrix}
```

Executing matrix multiplication produce the result:
```math
\begin{pmatrix}
x \\
y
\end{pmatrix}
= 
\begin{pmatrix}
  x' * cos(\Phi) - y' * sin(\Phi)\\
  x' * sin(\Phi) + y' * cos(\Phi)\\  
\end{pmatrix}
```

The same formula for our speeds:
```math
\begin{pmatrix}
u_{1x} \\
u_{1y}
\end{pmatrix}
= 
\begin{pmatrix}
  u'_{1x} *cos(\Phi)- u'_{1y} * sin(\Phi)\\
  u'_{1x} *sin(\Phi)+ u'_{1y} * cos(\Phi)\\  
\end{pmatrix}
```

```math
\begin{pmatrix}
u_{2x} \\
u_{2y}
\end{pmatrix}
= 
\begin{pmatrix}
  u'_{2x} * cos(\Phi)- u'_{2y} * sin(\Phi)\\
  u'_{2x} * sin(\Phi)+ u'_{2y} * cos(\Phi)\\  
\end{pmatrix}
```


All together: <br>
u<sub>1x</sub> = u'<sub>1x</sub> * cos($\Phi$) - u'<sub>1y</sub> * sin($\Phi$) <br>
u<sub>1y</sub> = u'<sub>1</sub>x * sin($\Phi$) + u'<sub>1y</sub> * cos($\Phi$) <br>
u<sub>2x</sub> = u'<sub>2x</sub> * cos($\Phi$) - u'<sub>2y</sub> * sin($\Phi$) <br>
u<sub>2y</sub> = u'<sub>2x</sub> * sin($\Phi$) + u'<sub>2y</sub> * cos($\Phi$) <br>

In other words, in a perfectly elastic collision between the balls we only need to consider the component of the velocity that is in the direction of the collision. The other component (tangent to the collision) will stay the same for both balls. We can get the collision components by creating a unit vector pointing in the direction from one ball to the other, then taking the dot product with the velocity vectors of the balls. We can then plug these components into a 1D perfectly elastic collision equation.

In our project we introduce a class named **Ball** containing all elementary information needed to identify the ellipse in canvas control and physical parameters (position, mass, velocity) for describing motion:

```java
public class Ball
{
    public Ellipse ellip = new Ellipse(); // Ellipse object
    public double r;                      // circle radius
    public double m;                      // mass of the ball
    public Vector p;                      // position vector
    public Vector v;                      // velocity vector
}
```
By the passing of the time, ball motion is tracked on canvas by the vector position p and the vector velocity v. 
Starting animation is triggered from on-click button named **Start**:

```java
private void StartButton_Click(object sender, RoutedEventArgs e)
{
     // remove EventHandler from Rendering event, if exists
     CompositionTarget.Rendering -= new EventHandler(RenderFrame);
     // change the label of button from "Start" to "Stop"
     StopContinueButton.Content = "Stop";
     // clean all Ellipse object from canvas, if it exists
     CleanBalls();
     // Assign the correct number of circles to be controlled
     TotNumCircles = tmpTotalNumCircles;
     // Resizing of correct number of array of Ball class
     // Number of array elements is defined from variable TotNumCircles
     Array.Resize(ref balls, TotNumCircles);
     // it draws all ellipses in canvas, with circle distribution
     StartDraw();
     // set an EventHandler on rendering event, with RenderFrame as callback function
     CompositionTarget.Rendering += new EventHandler(RenderFrame);
       StopContinueButton.IsEnabled = true;
       flagStopContinue = true;
}
```
In the picture below is reported a flow diagram for click-on on Start button:

[![6]][6]
**<p align="center">sketch 6: flow executed by click-on Start button event</p>**

Let's briefly explain how works the logic of project.

At beginning of running a method StartDraw() draws circles in the canvas;  StartDraw() uses a basic Mathematical function to dispose all circles around the center of canvas, avoiding overlaps:

```C#
double R;                         // R: radius to position the balls in circles from center of canvas control
double R0 = 80d;                  // R0: initial value of radius to position circles from center of canvas to the edges
double theta = 0;                 // theta: angle to distribute the balls in canvas, avoiding overlap
double theta_old = -1 * Math.PI;  // initial value of angle of radius R
double m = 0d;

R = R0;                         

theta = (k == 0) ? Math.Atan2(3 * balls[k].r, R) : Math.Atan2(2 * balls[k].r + 2 * balls[k - 1].r, R);
theta = theta + theta_old;
m = Convert.ToInt32(theta / (2 * Math.PI));
R = R0 + (R0 / 2) * m;

theta_old = theta;
balls[k].v = rnd1.Next(minSpeed, maxSpeed) * new Vector(Math.Cos(theta), Math.Sin(theta));
balls[k].p = startVector + new Vector(R * Math.Cos(theta), R * Math.Sin(theta));
balls[k].ellip.SetValue(Canvas.LeftProperty, balls[k].p.X - balls[k].r);
balls[k].ellip.SetValue(Canvas.TopProperty, balls[k].p.Y - balls[k].r);
canv.Children.Add(balls[k].ellip);
```

On completion of **StartDraw()** all balls[k] (k=0,1,2,…. TotNumCircles) the circles are drawn in the canvas in fix position:

[![7]][7]
<p align="center">sketch 7: initial position of all ellipses in the canvas</p>

The **System.Windows.Media.CompositionTarget** static class is the engine of balls animation and it represents the display surface on which our application is drawn.
Every time the **CompositionTarget** class raises the **Rendering** event, notifies any event handlers that a frame has been rendered. To create the animation we attach to the  Rendering event an event handler: <br>
```csharp
CompositionTarget.Rendering += new EventHandler(RenderFrame);
```
The callback function **RenderFrame()** is called every time the **Rendering** event is fired from **CompositionTarget**; in  the diagram below is shown the flow for **RenderFrame()** method. 


[![8]][8]
<p align="center">sketch 8: main program flow</p>

**RenderFrame()** has a main loop for scanning all elements of array of **Ball** class for two actions:
- increase the position of the ellipse a step forward with magnitude specified in vector velocity (v), to make an effect of animation:
```csharp
balls[j].p = balls[j].p + balls[j].v;
```

- verify that the ellipse does not intersect the edges of canvas. If the ellipse oversteps the boundary of canvas, it is applied an inversion of the velocity component orthogonal to the edge, see the below:

[![9]][9]
<p align="center">sketch 9: inversion of velocity component orthogonal at the edge of canvas</p>

Inside the main loop of the **RenderFrame()** method exists a further loop which makes two cascade  checks:
- First check runs through the **colliding()** method, to establish if the specific ball doesn’t intercept with any others.  
**colliding()** method accepts as input parameters two Balls class and return a boolean equal true in case of interception between two balls (i.e. **balls[i]** and  **balls[j]**).
To detect collision between two balls, is enough check that the distance between their centers is less than the sum of their radii. Given two circles (x0, y0, R0) and (x1,y1,R1), condition of interception follows the formula:
```csharp
     ABS(R0-R1) <= SQRT((x0 - x1)^2+(y0 - y1)^2) <= (R0 + R1)
```
- Second check runs through **resolveCollision()**; this last methods runs only if the **colliding()** methods detect an interception between balls. 
**resolveCollision()** method contains physics laws to control velocities of balls after collision and one algorithm based on Minimum Translation Distance (mtd) technique, to push balls apart in case of balls intersecting. Checking for collisions between balls happens through internal loop, created with start value that skips redundant checks:
-	if you have to check if ball 1 collides with ball 2 then you don't need to check if ball 2 collides with ball 1.
-	it skips checking for collisions with itself

Below the colliding() routine.
```csharp
public bool colliding(Ball _ball1, Ball _ball2)
{
    double r1 = _ball1.r;
    double r2 = _ball2.r;

    double dist = (_ball1.p - _ball2.p).Length;
    if (dist > (r1 + r2))
      {
          // No solutions, the circles are too far apart.  
          return false;
      }
    else if (dist <= r1 + r2)
      {
          // One circle contains the other.
          return true;
      }
    else if ((dist == 0) && (r1 == r2))
      {
          // the circles coincide.
          return true;
      }
    else return true;
}

public void resolveCollision(Ball _ball1, Ball _ball2)
{
    double collisionision_angle = Math.Atan2((_ball2.p.Y - _ball1.p.Y), (_ball2.p.X - _ball1.p.X));
            
    double speed1 = _ball1.v.Length;
    double speed2 = _ball2.v.Length;

    double direction_1 = Math.Atan2(_ball1.v.Y, _ball1.v.X);
    double direction_2 = Math.Atan2(_ball2.v.Y, _ball2.v.X);
    double new_xspeed_1 = speed1 * Math.Cos(direction_1 - collisionision_angle);
    double new_yspeed_1 = speed1 * Math.Sin(direction_1 - collisionision_angle);
    double new_xspeed_2 = speed2 * Math.Cos(direction_2 - collisionision_angle);
    double new_yspeed_2 = speed2 * Math.Sin(direction_2 - collisionision_angle);


    double final_xspeed_1 = ((_ball1.m - _ball2.m) * new_xspeed_1 + (_ball2.m + _ball2.m) * new_xspeed_2) / (_ball1.m + _ball2.m);
    double final_xspeed_2 = ((_ball1.m + _ball1.m) * new_xspeed_1 + (_ball2.m - _ball1.m) * new_xspeed_2) / (_ball1.m + _ball2.m);
    double final_yspeed_1 = new_yspeed_1;
    double final_yspeed_2 = new_yspeed_2;


    double cosAngle = Math.Cos(collisionision_angle);
    double sinAngle = Math.Sin(collisionision_angle);
    _ball1.v.X = cosAngle * final_xspeed_1 - sinAngle * final_yspeed_1;
    _ball1.v.Y = sinAngle * final_xspeed_1 + cosAngle * final_yspeed_1;
    _ball2.v.X = cosAngle * final_xspeed_2 - sinAngle * final_yspeed_2;
    _ball2.v.Y = sinAngle * final_xspeed_2 + cosAngle * final_yspeed_2;

    Vector pos1 = new Vector(_ball1.p.X, _ball1.p.Y);
    Vector pos2 = new Vector(_ball2.p.X, _ball2.p.Y);

    // get the mtd
    Vector posDiff = pos1 - pos2;
    double d = posDiff.Length;

    // minimum translation distance to push balls apart after intersecting
    Vector mtd = posDiff * (((_ball1.r + _ball2.r) - d) / d);

    // resolve intersection --
    // computing inverse mass quantities
    double im1 = 1 / _ball1.m;
    double im2 = 1 / _ball2.m;

    // push-pull them apart based off their mass
    pos1 = pos1 + mtd * (im1 / (im1 + im2));
    pos2 = pos2 - mtd * (im2 / (im1 + im2));
    _ball1.p = pos1;
    _ball2.p = pos2;

    if (((_ball1.p.X + _ball1.r) >= canv.Width) | ((_ball1.p.X - _ball1.r) <= 0))
    _ball1.v.X = -1 * _ball1.v.X;

    if (((_ball1.p.Y + _ball1.r) >= canv.Height) | ((_ball1.p.Y - _ball1.r) <= 0))
    _ball1.v.Y = -1 * _ball1.v.Y;

    if (((_ball2.p.X + _ball2.r) >= canv.Width) | ((_ball2.p.X - _ball2.r) <= 0))
    _ball2.v.X = -1 * _ball2.v.X;

    if (((_ball2.p.Y + _ball2.r) >= canv.Height) | ((_ball2.p.Y - _ball2.r) <= 0))
    _ball2.v.Y = -1 * _ball2.v.Y;
}
```

The code for **RenderFrameFrame()** method:
```csharp
public void RenderFrame(object sender, System.EventArgs e)
{
    for (int j = 0; j < TotNumCircles; j++)
    {
        //  balls[j].p.X = balls[j].p.X + balls[j].v.X;
        //  balls[j].p.Y = balls[j].p.Y + +balls[j].v.Y;
        balls[j].p = balls[j].p + balls[j].v;

        if ((balls[j].p.X + balls[j].r) >= canv.Width)
        {
              balls[j].v.X = -1 * balls[j].v.X;
              balls[j].p.X = canv.Width - balls[j].r;
        }
        if ((balls[j].p.X - balls[j].r) <= 0)
        {
              balls[j].v.X = -1 * balls[j].v.X;
              balls[j].p.X = balls[j].r;
        }

        if ((balls[j].p.Y + balls[j].r) >= canv.Height)
        {
              balls[j].v.Y = -1 * balls[j].v.Y;
              balls[j].p.Y = canv.Height - balls[j].r;
        }
        if ((balls[j].p.Y - balls[j].r) <= 0)
        {
              balls[j].v.Y = -1 * balls[j].v.Y;
              balls[j].p.Y = balls[j].r;
        }

        Canvas.SetLeft(balls[j].ellip, balls[j].p.X - balls[j].r);
        Canvas.SetTop(balls[j].ellip, balls[j].p.Y - balls[j].r);

        for (int k = j + 1; k < TotNumCircles; k++)
        {
            if (colliding(balls[j], balls[k]))
            {
                TextBoxCollision.Text = "Collision pair: " + j.ToString() + "-" + k.ToString();
                 resolveCollision(balls[k], balls[j]);
            }
        }
    }
}
```

<br>

Screenshot of the running application:

[![10]][10]
<p align="center">sketch 10: running application</p>

### <a name="Reference"></a> Reference

[Ball to Ball Collision - Detection and Handling](http://stackoverflow.com/questions/345838/ball-to-ball-collision-detection-and-handling) <br>
[In WPF: Children.Remove or Children.Clear doesn't free objects](http://stackoverflow.com/questions/2541684/in-wpf-children-remove-or-children-clear-doesnt-free-objects) <br>
[How to remove a wpf element on a canvas by it's tag name?](http://stackoverflow.com/questions/1053555/how-to-remove-a-wpf-element-on-a-canvas-by-its-tag-name) <br>
[Delete all images added to canvas](http://stackoverflow.com/questions/10835755/delete-all-images-added-to-canvas) <br>

`Tags: WFP` <br>
`date: 02/01/2015` <br>
`date: 14/08/2023` <br>


**Note: Project runs with Visual Studio 2022.**

<!--Image References-->

[1]: ./media/picture01.png "sphere in collision and coordinate system"
[2]: ./media/picture02.png "two Cartesian coordinate systems"
[3]: ./media/picture03.png "collision "
[4]: ./media/picture04.png "velocities direction inversion after collision"
[5]: ./media/picture05.png "velocities after collision in the reference system Ref"
[6]: ./media/click-on-start-flow.png "click-on Start flow"
[7]: ./media/balls-start-position.png "balls stat position"
[8]: ./media/main-program-flow.png "main program flow"
[9]: ./media/inversion-velocities.png "inversion of velocity component orthogonal at the edge of canvas"
[10]: ./media/running-application.png "screenshot of running application"

<!--Link References-->

