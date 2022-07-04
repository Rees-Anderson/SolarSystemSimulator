# A Simple Solar System Simulator
Current WebGL Build: <https://rees-anderson.github.io/SolarSystemSimulator/SolarSystemSimulator_WebGL>

This link will take you to the hosted page for the WebGL build of the game - it will run in your browser without a download! WebGL is a browser based graphics API, and it's supported natively by common browsers like Safari, Chrome, Edge, and Firefox.

This basic simulation of the Solar System was created over two weeks to serve as my submission for the final project of the CSS451 3D Computer Graphics course at the University of Washington. The focus of this course was learning how 3D graphics systems work including how graphics engines render objects, keep track of object location and size relative to each other, map textures, implement shaders, and more. The goal of the final project was to show everything we'd learned over the course by having us modify and overwrite the Unity Engine to make use of our own implementations of the location hierarchy, texture mapping, rendering pipeline, and shaders - and then create an interactive application that shows off each of these features. I chose a simulation of our Solar System since it naturally lends itself well to everything on that list.

My simulator contains the Sun, all of the planets, and all of the planets' major moons. All of these celestial objects have accurate rotations and perfected orbits (orbits are modeled as perfect circles with the average distance to the sun as its radius) which are tied to an internal clock the user can control. All of the celestial objects' size and relative distances to each other are modeled to-scale. Due to engine and time constraints however, the size and distance scales are different from each other in this simulation. The program allows the user to explore the simulator with a menu of the celestial objects, complete with information about each of them. Users can also launch and freely fly a "Terraforming Rocket" about the simulation, crashing this rocket into a planet or moon will "terraform" it, try it out!

Credits: Rees Anderson

Created for CSS451 at the UW - Autumn Quarter 2021