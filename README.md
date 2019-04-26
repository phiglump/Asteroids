# CLASTEROIDS
An interesting take on the original arcade game.

# Authors 
Philippe Lumpkin and Nicholas Pica

# How to run?
Download the entire zip package. This zip will include everything you need to get started other than an install of Visual Studio 2017 or 2019. So long as you have that installed you can run the program. Extract the zip of the project to a location that is accesible for you. Open the first folder and double click on the Asteroids.sln. If the program asks what to run it in, make sure to select Visual Studio. It should then load the program in visual studio and at this point all that has to be done is to click the green start button near the top of the screen. This will begin CLASTEROIDS.  

# Description
The name of this project is "CLASTEROIDS". The project is intended to be our take on the original arcade game Asteroids. Our original goal was to create a copy of the original game in a C# wpf. We decided due to time and complications to create something similar game with our own spin on the concept.

# Design Decisions
At first we were going to go with the dafualt theme of space from the original Asteroids game. After doing a first presentation many of the classmates suggested that we do some sort of paper theme. We figured that this was actually a really good idea and went with it. This eventually lead to our new name CLASTEROIDS which is a mash of Class and Asteroids. In terms of the backend of the program itself, we decided to use one main window that hosts pages within it. This way you didn't have to have multiple windows pop up over each other, making a very smooth transition. 

# Bugs
One major bug is that the bullets will eventually start to go really fast compared to the speed that they are going at. Our idea on what is happening is that the timers for the code may be doubling over the same bullets making them go twice, or more, as fast as they should. There is also a bug that only happens on certain desktops. Every so often a exception is thrown on the Bullet[0] that causes the program to not initially load. What makes this bug even worse is that it isn't consistant. One immediate difference we can think that could be a factor is that Phil's PC is ran on an AMD CPU, while Nic's PC is ran on an INTEL CPU just like the lab machines. The INTEL CPU's are the only computers that have exhibited this bug.

# Challenges
There were plenty of issues that created challenges for us throughout the entire project. To begin with, we had just started at the beginning of the semester learning how to code within the C# language as well as using a WPF to create projects. Though this wasn't extremely easy we were able to apply relative knowledge gained from having learned C++. Another challenge is that we have nevered coded a video game before. Essentially this program was mostly new territory for us. Another issue was that there weren't a lot of great understandable examples to follow. There were some other great creations of the asteroids game, but with our limited knowledge on the C# language it was difficult to really understand what those programmers did in there codes. Another challenge was time management. We did have other classes going on at the same time, which meant that we really had to balance our time well. 

# Future Development
We would love to complete this game and remove all of the bugs creating a well rounded complete game. It also wouldn't be a bad idea to make the program more dynamic in that you can have more less asteroids, different types of firing styles, bosses, and much more. There are plenty of things that this code can be improved on. 
