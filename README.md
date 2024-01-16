# Unity Boid Simulator

## Overview
This repository contains the Unity Boid Simulator, a simulation tool for exploring the behavior of boids, or "bird-oid objects". Boids are a simulation of the flocking behavior of birds, fish, and other animals. They follow simple rules that result in complex, lifelike behavior. The core principles governing boid behavior are separation, alignment, and cohesion.

## How Boids Work
- **Separation**: Avoid crowding neighbors (short-range repulsion).
- **Alignment**: Steer towards the average heading of neighbors.
- **Cohesion**: Steer towards the average position of neighbors (long-range attraction).

Each boid in this simulation follows these rules, resulting in emergent flocking behavior. Additional rules have been added to improve the behaviour. These include limited the neighbour radius, as well as the boid field of view (for which it can see neighbours). In addition wall collisions have been dealt with via ray tracing, and then steering the boid away from the wall.

## Repository Structure
The main scripts for the Boid simulation are located under `Assets > Scripts`:
- `Boids.cs`: Defines the behavior of individual boids, including movement and rule application.
- `BoidManager.cs`: Manages the instantiation and overall control of boids in the simulation.
- `CloseGame.cs`: Provides functionality to close the simulator with a keyboard command.

There are also standard Unity files like Scenes and Sprites, with minimal modifications.

## Running the Simulator
To run the final executable of the Unity Boid Simulator:
1. Download the entire `Builds` folder from this repository.
2. Run the executable within the `Builds` folder. This is essential for the proper functioning of the simulator.

## Exiting the Simulator
To exit the simulator, press `Ctrl+C` at any time.

## Contributions
Feel free to contribute to this project by submitting pull requests or opening issues for any bugs or enhancements you identify.
