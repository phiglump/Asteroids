# CLASTEROIDS
An interesting take on the original arcade game.

# Authors 
Philippe Lumpkin and Nicholas Pica

# Description
The name of this project is "CLASTEROIDS". The project is intended to be our take on the original arcade game Asteroids. Our original goal was to create a copy of the original game in a C# wpf. We decided due to time and complications to create something similar game with our own spin on the concept.
# Design Decisions
At first we were going to go with the dafualt theme of space from the original Asteroids game. After doing a first presentation many of the classmates suggested that we do some sort of paper theme. We figured that this was actually a really good idea and went with it. This eventually lead to our new name CLASTEROIDS which is a mash of Class and Asteroids. In terms of the backend of the program itself, we decided to use one main window that hosts pages within it. This way you didn't have to have multiple windows pop up over each other, making a very smooth transition. 
# Bugs
One major bug is that the bullets will eventually start to go really fast compared to the speed that they are going at. Our idea on what is happening is that the timers for the code may be doubling over the same bullets making them go twice, or more, as fast as they should. There is also a bug that only happens on certain desktops. Every so often a exception is thrown on the Bullet[0] that causes the program to not initially load. What makes this bug even worse is that it isn't consistant. One immediate difference we can think that could be a factor is that Phil's PC is ran on an AMD CPU, while Nic's PC is ran on an INTEL CPU just like the lab machines. The INTEL CPU's are the only computers that have exhibited this bug.
