Part 2 README

Explanation:

I have given 5 states of animation that character can be in. Idle, WalkForward, WalkBackward, Run, and Jump.
The states Idle, WalkForward, WalkBackard, and Run utilize blend trees to allow the player to turn left and right.

I designed the FSM using an animation controller and used the animation controller within the animator component. 

All blend trees use the float blending parameter Turn, which is taken as input from the left and right arrow keys.
Holding the left arrow key makes the character go left and sets turn to -1, no input sets float to 0, and
holding the left arrow key makes the character go right and sets Turn to 1.

The transition to walk and backwards uses the float vertical_speed as a parameter. Holding the up arrow or w
results in vertical_speed being set to 1 and the character transitioning from idle to walking forward. Holding 
the down arrow or s results in vertical_speed being set to 1 and the character transitioning from idle to 
walking backward. 

To transition from walking to running you have to hold the shift key down which sets the sprint value to 1.

To transition from running to jumping you must hit the spacebar which sets the Jump value to 1.

In any state other than idle, not giving any input at all will transfer you back to idle. If you are in the jumping state and give no input you will transition from jumping to running, then to walking, and finally to idle.


User Guide:

1. MOVEMENT
	a. W,A,S,D to control the player's movements
	b. Hold SHIFT to sprint
	c. Press SPACE to jump (player must currently be sprinting)
