Robogeddon  --- Started March 8th  2016
---------------------------------------
---------------------------------------

Summary
--------
Robogeddon is a simple survival game where the player attempts to survive against the waves of robot zombies
for as long as they can. Currently there is only one small level made out of primitive objects and 
basic color materials. The player uses two machine gun's as weapons. 

Controls
--------
-Moving the character can be done through either the arrow keys or WASD.

-Rotating the character is achieved through the mouse.

-Making the character jump is achieved through the space bar.


Types of Enemies
----------------
-Currently there are three types of enemies:
	-BallBot, which is a robot made out of spheres, is the fastest of the three.
	 This bot has the smallest health pool and also does the least amount of damage.
	
	-FatBot, which is a robot made out of primitive shapes, is the slowest of the three.
	 This bot has the largest health pool and does the most damage.

	-TallBot, which is a robot made out of primitive shapes, is the "middle" robot.
	 This bot has a health pool and damage output in between the other two robots.


Enemy Spawn Points
-----------------
-There are currently 2 enemy spawn points that spawn enemies.
-A robot is spawned every 3 seconds, and is spawned at one of the two spawners, which is chosen "randomly".


Item Pickups
------------
-Currently there is only one item pickup, which is a health pickup
	-The health pickup gives the player maximum health, which in this game is 100.
	-The health pickup CANNOT be picked up when the player has maximum health.
		-This is done to avoid accidental pickups and to allow for the ability
		 to run through the health, even if you do not want to pick it up. 


Some Improvements/Next Steps Needed
------------------------
-Weapon Rotation/rotation clamping can behave better. Sometimes effects weapon rotation.
-Collisions between enemies and bullets still not working perfectly. Seems some bullets do not collide
 with the enemy even if they should.
-Moving platform needs features that limit/make the platform use make sense.


