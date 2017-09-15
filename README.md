# Color Combination Demo App
A small application written to demonstrate problem resolution, design and development. This sample project demonstrates implementation, through code of a solution to the following scenario:

## The Challenge
You recently had a state of the art security system installed in your home. The master control panel requires a series of bi‐colored chips to be placed end to end in a specific sequence in order to gain access. The security provider split up the chips and gave a random number to each of your family members. All of you must convene in order to assemble the chips and create the correct color combination. The access panel has a channel for the security chips. On each end of the channel is a colored marker. Chips are placed end to end such that the adjacent colors match and the starting and ending chips are color matched to the corresponding markers.

## Input
The input consists of a single line indicating the beginning and ending marker colors followed by a series of chip definitions. All lines consist of a pair of color indicators; you may use integers, strings, or characters to represent each color. For our example purposes, we will use strings.

## Output
If the combination cannot be achieved by using all of the chips once and only once, then report "Cannot unlock master panel". If the combination can be achieved, then print the chips in the order required to unlock the master control.

----

> # The Solution
> To solve this problem, I've created a small ASP.Net Core MVC Web Application. Although I most frequently work with ASP.Net Framework MVC web applications (not SPA),  I have chosen this route to demonstrate flexibility in frameworks and design.
>
> This application is designed to provide a simple user interface allowing the user to test various combinations of security chips to determine if a correct combination can be assembled per the above requirements. In the interest of brevity, documentation is provided within the comments inside the code of this project. Please see the Main method in Program.cs to get started!
